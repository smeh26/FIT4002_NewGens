using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using MyNursingFuture.BL.Entities;
using MyNursingFuture.DL;
using MyNursingFuture.Util;

namespace MyNursingFuture.BL.Managers
{
    public interface IDomainsManager : IManager<DomainEntity>
    {
        Result UpdatePosition(int questionId, int position);
    }
    public class DomainsManager: IDomainsManager
    {
        public Result Get()
        {
            var con = new DapperConnectionManager();
            var query = new QueryEntity
            {
                Entity = new { },
                Query = @"SELECT * FROM Domains ORDER BY Name ASC"
            };
            return con.ExecuteQuery<DomainEntity>(query);
        }

        public Result Get(int id)
        {
            var con = new DapperConnectionManager();
            var query = new QueryEntity();

            query.Query = @"SELECT * FROM Domains
                            where DomainId = @DomainId";
            query.Entity = new { DomainId = id };

            var result = con.ExecuteQuery<DomainEntity>(query);

            if (!result.Success)
            {
                result.Message = "Domain not found";
                return result;
            }

            var r = (IEnumerable<DomainEntity>)result.Entity;

            var domain = r.FirstOrDefault();

            if (result.Entity == null)
                result.Success = false;

            var queryActions = new QueryEntity()
            {
                Query = @"SELECT A.ActionId, A.Title, C.Position FROM DomainsActions as C
                        JOIN Actions as A ON C.ActionId = A.ActionId
                        where C.DomainId = @DomainId ORDER BY C.Position ASC",
                Entity = new { DomainId = id }
            };
            var resultActions = con.ExecuteQuery<ActionEntity>(queryActions);
            if (!resultActions.Success)
            {
                result.Message = "An error occurred";
                return result;
            }
            var actions = (IEnumerable<ActionEntity>)resultActions.Entity;
            domain.ActionsList = actions.ToList();
            result.Entity = domain;
            return result;
        }

        public Result Update(DomainEntity entity)
        {
            var con = new DapperConnectionManager();
            var query = new QueryEntity();
            Result result = new Result();
            ImageManager imageManager = new ImageManager();
            if (entity.IconFile != null)
            {
                var resImgCheck = imageManager.CheckFileImageAndSave(entity.ImagePath, entity.IconFile, 30, 30, 3000, 3000);
                if (!resImgCheck.Success)
                {
                    return resImgCheck;
                }
                entity.Icon = resImgCheck.Entity.ToString();
            }

            if (entity.ImageFile != null)
            {
                var resImgCheck = imageManager.CheckFileImageAndSave(entity.ImagePath, entity.ImageFile, 30, 30, 3000, 3000);
                if (!resImgCheck.Success)
                {
                    return resImgCheck;
                }
                entity.Image = resImgCheck.Entity.ToString();
            }

            using (var scope = new TransactionScope())
            {
                query.Entity = entity;
                query.Query = @"Update Domains set Name = @Name, Title = @Title, Text = @Text, Image = @Image, Icon = @Icon, Framework = @Framework WHERE DomainId = @DomainId";
                result = con.ExecuteQueryUnScoped(query);
                result.Entity = entity.DomainId;
                var queryDeleteActions = new QueryEntity
                {
                    Query = @"DELETE from DomainsActions where DomainId = @DomainId",
                    Entity = new { DomainId = entity.DomainId }
                };
                var resultDeleteActions = con.ExecuteQueryUnScoped(queryDeleteActions);

                if (!resultDeleteActions.Success)
                {
                    result.Message = "An error occurred";
                    return result;
                }
                foreach (var item in entity.ActionsList)
                {
                    item.DomainId = entity.DomainId;
                    var queryActionCreate = new QueryEntity
                    {
                        Query = @"INSERT into DomainsActions (DomainId, ActionId, Position) VALUES( @DomainId, @ActionId, @Position)",
                        Entity = item
                    };
                    var resultAction = con.ExecuteQueryUnScoped(queryActionCreate);

                    if (resultAction.Success)
                        continue;

                    result.Message = "An error occurred";
                    return result;
                }
                scope.Complete();
                result.Message = result.Success ? "The domain has been updated" : "An error occurred";
            }
            
            return result;
        }

        public Result Insert(DomainEntity entity)
        {
            var con = new DapperConnectionManager();
            var query = new QueryEntity();
            Result result = null;
            ImageManager imageManager = new ImageManager();
            if (entity.IconFile != null)
            {
                var resImgCheck = imageManager.CheckFileImageAndSave(entity.ImagePath, entity.IconFile, 100, 100, 3000, 3000);
                if (!resImgCheck.Success)
                {
                    return resImgCheck;
                }
                entity.Icon = resImgCheck.Entity.ToString();
            }

            if (entity.ImageFile != null)
            {
                var resImgCheck = imageManager.CheckFileImageAndSave(entity.ImagePath, entity.ImageFile, 100, 100, 3000, 3000);
                if (!resImgCheck.Success)
                {
                    return resImgCheck;
                }
                entity.Image = resImgCheck.Entity.ToString();
            }
            using (var scope = new TransactionScope())
            {
                var linkEntity = new LinkEntity
                {
                    Href = " ",
                    Name = entity.Title,
                    Type = LinksTypes.DOMAIN.ToString(),
                    Active = false
                };

                query.Entity = linkEntity;
                query.Query = @"INSERT INTO Links (Name, Type, Href, Active) VALUES(@Name, @Type, @Href, @Active)";


                result = con.InsertQueryUnScoped(query);
                if (!result.Success)
                {
                    result.Message = "An error occurred";
                    return result;
                }
                var linkId = (int)result.Entity;

                entity.LinkId = linkId;
                query.Entity = entity;
                query.Query = @"INSERT INTO Domains (Name, Title, Text, LinkId, Image, Icon, Framework) VALUES(@Name, @Title, @Text, @LinkId, @Image, @Icon, @Framework)";
                result = con.InsertQueryUnScoped(query);

                if (!result.Success)
                {
                    result.Message = "An error occurred";
                    return result;
                }

                var domainId = (int)result.Entity;
                
                var queryDeleteActions = new QueryEntity
                {
                    Query = @"DELETE from DomainsActions where DomainId = @DomainId",
                    Entity = new {DomainId = domainId}
                };
                var resultDeleteActions = con.ExecuteQueryUnScoped(queryDeleteActions);

                if (!resultDeleteActions.Success)
                {
                    result.Message = "An error occurred";
                    return result;
                }
                foreach (var item in entity.ActionsList)
                {
                    item.DomainId = domainId;
                    var queryActionCreate = new QueryEntity
                    {
                        Query = @"INSERT into DomainsActions (DomainId, ActionId, Position) VALUES( @DomainId, @ActionId, @Position)",
                        Entity = item
                    };
                    var resultAction = con.ExecuteQueryUnScoped(queryActionCreate);

                    if (resultAction.Success)
                        continue;

                    result.Message = "An error occurred";
                    result.Success = false;
                    return result;
                }


                //update link with href

                var queryUpdateLink = new QueryEntity()
                {
                    Entity = new { Href = string.Concat("/domains/" , domainId), LinkId = linkId },
                    Query = "UPDATE Links set Href = @Href where LinkId = @LinkId"
                };
                var resultUpdateLink = con.ExecuteQueryUnScoped(queryUpdateLink);
                if (!resultUpdateLink.Success)
                {
                    result.Message = "An error occurred";
                    return result;
                }

                scope.Complete();
                result.Message = "The domain has been created";
            }
            return result;
        }

        public Result Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Result SetPublished(int id, bool published = true)
        {
            var con = new DapperConnectionManager();
            var query = new QueryEntity()
            {
                Query = @"DECLARE @linkId int;
                          SELECT @linkId = Domains.LinkId
                          FROM Domains WHERE DomainId = @DomainId;
                          UPDATE Domains set Published = @Published where DomainId = @DomainId;
                          UPDATE Links set Active = @Published where LinkId = @LinkId",
                Entity = new { SectorId = id, Published = published }
            };
            return con.ExecuteQuery(query);
        }

        public Result UpdatePosition(int domainId, int position)
        {
            var con = new DapperConnectionManager();
            var query = new QueryEntity();
            query.Entity = new { DomainId = domainId, Position = position };
            query.Query = @"UPDATE Domains set Position = @Position where DomainId = @DomainId";
            var result = con.ExecuteQuery(query);
            result.Message = result.Success ? "The Position has been updated" : "An error occurred";
            return result;
        }
    }
}
