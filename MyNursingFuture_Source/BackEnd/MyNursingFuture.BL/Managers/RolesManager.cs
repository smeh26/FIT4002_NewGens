using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using MyNursingFuture.BL.Entities;
using MyNursingFuture.DL;
using MyNursingFuture.Util;

namespace MyNursingFuture.BL.Managers
{
    public interface IRolesManager : IManager<RoleEntity>
    {
        
    }
    public class RolesManager: IRolesManager
    {
        public Result Get()
        {
            var con = new DapperConnectionManager();
            var query = new QueryEntity
            {
                Entity = new { },
                Query = @"SELECT * FROM Roles WHERE Active = 1 ORDER BY Name ASC"
            };
            return con.ExecuteQuery<RoleEntity>(query);
        }

        public Result Get(int id)
        {
            var con = new DapperConnectionManager();
            var query = new QueryEntity();

            query.Query = @"SELECT * FROM Roles
                            where RoleId = @RoleId";
            query.Entity = new { RoleId = id };

            var result = con.ExecuteQuery<RoleEntity>(query);

            if (!result.Success)
            {
                result.Message = "Role not found";
                return result;
            }

            var r = (IEnumerable<RoleEntity>)result.Entity;

            result.Entity = r.FirstOrDefault();

            if (result.Entity == null)
                result.Success = false;

            return result;
        }

        public Result Update(RoleEntity entity)
        {
            var con = new DapperConnectionManager();
            
            var query = new QueryEntity();
            query.Entity = entity;
            query.Query = @"UPDATE Roles set 
                                      Title=@Title,
                                      WhatIs = @WhatIs,
                                      WhatIsTheirRole = @WhatIsTheirRole,
                                      Accountabilities = @Accountabilities,
                                      Examples = @Examples,
                                      FurtherInformation = @FurtherInformation,
                                      LinkName = @LinkName,
                                      Pathways = @Pathways
                                      WHERE RoleId = @RoleId";
            var result = con.ExecuteQuery(query);
            result.Message = result.Success ? "The role has been updated" : "An error occurred";
            result.Entity = entity.RoleId;
            return result;
        }

        public Result Insert(RoleEntity entity)
        {
            var con = new DapperConnectionManager();
            var query = new QueryEntity();
            Result result;
            using (var scope = new TransactionScope())
            {
                var linkEntity = new LinkEntity
                {
                    Href = " ",
                    Name = entity.Title,
                    Type = "ROLE",
                    Active = false
                };

                query.Entity = linkEntity;
                query.Query = @"INSERT INTO Links (Name, Type, Href, Active) VALUES(@Name, @Type, @Href, @Active)";


                result = con.InsertQueryUnScoped(query);
                if (!result.Success)
                {
                    return result;
                }
                
                var linkId = (int)result.Entity;

                entity.LinkId = linkId;
                query.Entity = entity;
                query.Query = @"INSERT INTO Roles (Name, LinkId, WhatIs, WhatIsTheirRole, Accountabilities, Examples, FurtherInformation, Title, LinkName, Pathways) 
                                               VALUES(@Name, @LinkId, @WhatIs, @WhatIsTheirRole, @Accountabilities, @Examples, @FurtherInformation, @Title, @LinkName, @Pathways)";
                result = con.InsertQueryUnScoped(query);

                if (!result.Success)
                {
                    result.Message = "An error occurred";
                    return result;
                }

                //LINK HREF UPDATE
                var queryUpdateLink = new QueryEntity()
                {
                    Entity = new { Href = "/roles/" + (int)result.Entity, LinkId = linkId },
                    Query = "UPDATE Links set Href = @Href where LinkId = @LinkId"
                };
                var resultUpdateLink = con.ExecuteQueryUnScoped(queryUpdateLink);
                if (!resultUpdateLink.Success)
                {
                    result.Message = "An error occurred";
                    result.Success = false;
                    return result;
                }

                scope.Complete();
            }
            result.Message = "The role has been created";
            return result;
        }

        public Result Delete(int id)
        {
            var con = new DapperConnectionManager();
            var query = new QueryEntity()
            {
                Query = @"DECLARE @linkId int;
                          SELECT @linkId = Roles.LinkId
                          FROM Roles WHERE RoleId = @RoleId;
                          Update Roles set Active = 0 WHERE RoleId = @RoleId;
                          Update Links set Active = 0 WHERE LinkId = @linkId;",
                Entity = new { RoleId = id }
            };
            var result = con.ExecuteQuery(query);
            result.Message = result.Success ? "The role has been deleted" : "An error occurred";
            return result;
        }

        /// <summary>
        /// By default set the role and the link published/active, could set the value passed as the second parameter as false for unpublishing
        /// </summary>
        /// <param name="id"></param>
        /// <param name="published">Optional, set published or unpublished. True by default</param>
        /// <returns></returns>
        public Result SetPublished(int id, bool published = true)
        {
            var con = new DapperConnectionManager();
            var query = new QueryEntity()
            {
                Query = @"DECLARE @linkId int;
                          SELECT @linkId = Roles.LinkId
                          FROM Roles WHERE RoleId = @RoleId;
                          UPDATE Roles set Published = @Published where RoleId = @RoleId;
                          UPDATE Links set Active = @Published where LinkId = @LinkId",
                Entity = new { RoleId = id, Published = published }
            };
            var result = con.ExecuteQuery(query);
            result.Message = result.Success ? "The role has been updated" : "An error occurred";
            return result;
        }
    }
}
