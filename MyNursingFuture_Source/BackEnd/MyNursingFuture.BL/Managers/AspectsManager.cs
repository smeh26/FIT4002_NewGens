using System;
using System.Collections;
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
    public interface IAspectsManager : IManager<AspectEntity>
    {
        Result GetByDomain(int domainId);
        Result UpdatePosition(int questionId, int position);
    }
    public class AspectsManager:IAspectsManager
    {
        public Result Get()
        {
            var con = new DapperConnectionManager();
            var query = new QueryEntity
            {
                Entity = new { },
                Query = @"SELECT A.AspectId, A.Title, A.Published, D.Name as DomainName, D.Framework as DomainFramework, A.Position
                          FROM Aspects as A
                          JOIN Domains as D on A.DomainId =  D.DomainId
                          where A.Active = 1 ORDER BY Title ASC"
            };
            return con.ExecuteQuery<AspectEntity>(query);
        }

        public Result GetByDomain(int domainId)
        {
            var con = new DapperConnectionManager();
            var query = new QueryEntity
            {
                Entity = new { DomainId = domainId },
                Query = @"SELECT A.AspectId, A.Title, A.Published, D.Name as DomainName, D.Framework as DomainFramework, A.Position
                          FROM Aspects as A
                          JOIN Domains as D on A.DomainId =  D.DomainId
                          where A.Active = 1 and D.DomainId = @DomainId ORDER BY Title ASC"
            };
            return con.ExecuteQuery<AspectEntity>(query);
        }

        public Result Get(int id)
        {
            var con = new DapperConnectionManager();
            var query = new QueryEntity
            {
                Entity = new { AspectId = id },
                Query = @"SELECT * FROM Aspects WHERE AspectId = @AspectId"
            };
            var result = con.ExecuteQuery<AspectEntity>(query);

            if (!result.Success)
            {
                result.Message = "Aspect not found";
                return result;
            }

            var r = (IEnumerable<AspectEntity>)result.Entity;

            var aspect= r.FirstOrDefault();

            if (result.Entity == null)
            {
                result.Success = false;
                return result;
            }

            var queryActions = new QueryEntity()
            {
                Query = @"SELECT A.ActionId,  A.Title, C.LevelAction, C.Position, A.Type FROM AspectsActions as C
                        JOIN Actions as A ON C.ActionId = A.ActionId
                        where C.AspectId = @AspectId",
                Entity = new { AspectId  = id}
            };
            var resultActions = con.ExecuteQuery<ActionEntity>(queryActions);
            if (!resultActions.Success)
            {
                result.Message = "An error occurred";
                return result;
            }
            var actions = (IEnumerable<ActionEntity>) resultActions.Entity;
            aspect.ActionsList = actions.ToList();
            result.Entity = aspect;
            return result;
        }

        public Result Update(AspectEntity entity)
        {
            var con = new DapperConnectionManager();
            var query = new QueryEntity();
            Result result = null;
            using (var scope = new TransactionScope())
            {
                query.Entity = entity;
                query.Query =
                    @"Update  Aspects set Title=@Title, Text=@Text, Examples=@Examples, OnlineResources=@OnlineResources, FurtherEducation =@FurtherEducation, PeopleContact = @PeopleContact, Levels = @Levels, Position = @Position
                                where AspectId = @AspectId";

                result = con.ExecuteQueryUnScoped(query);

                if (!result.Success)
                {
                    result.Message = "An error occurred";
                    return result;
                }
                var aspectId = entity.AspectId;

                

                var listActionsToRemove = entity.ActionsList.Where(x => x.Removed);
                var listActionsToCreate = entity.ActionsList.Where(x => x.Created);
                var listActionsToCopy = entity.ActionsList.Where(x => x.Added).ToList();

                foreach (var item in listActionsToRemove)
                {
                    var queryActionCreate = new QueryEntity
                    {
                        Query = @"DELETE from AspectsActions where ActionId = @ActionId",
                        Entity = item
                    };
                    var resultAction = con.ExecuteQuery(queryActionCreate);
                    if (resultAction.Success)
                        continue;

                    result.Message = "An error occurred";
                    return result;
                }
                

                foreach (var item in listActionsToCreate)
                {
                    var queryActionCreate = new QueryEntity
                    {
                        Query = @"INSERT into Actions (Text, Title, Type) VALUES(@Text, @Title, @Type)",
                        Entity = item
                    };
                    var resultAction = con.InsertQueryUnScoped(queryActionCreate);
                    
                    item.ActionId = (int)resultAction.Entity;
                    listActionsToCopy.Add(item);
                }


                foreach (var item in listActionsToCopy)
                {
                    item.AspectId = aspectId;
                    var queryActionCreate = new QueryEntity
                    {
                        Query = @"INSERT into AspectsActions (AspectId, ActionId, LevelAction, Position) VALUES( @AspectId, @ActionId, @LevelAction, @Position)",
                        Entity = item
                    };
                    var resultAction = con.ExecuteQueryUnScoped(queryActionCreate);

                    if (resultAction.Success)
                        continue;

                    result.Message = "An error occurred";
                    return result;
                }
                scope.Complete();
                result.Entity = aspectId;
                result.Message = "The aspect has been created";
            }
            return result;
        }

        public Result Insert(AspectEntity entity)
        {
            var con = new DapperConnectionManager();
            var query = new QueryEntity();
            Result result = null;
            using (var scope = new TransactionScope())
            {
                var linkEntity = new LinkEntity
                {
                    Href = " ",
                    Name = entity.Title,
                    Type = LinksTypes.ASPECT.ToString(),
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
                query.Query = @"INSERT INTO Aspects (DomainId, LinkId, Title, Text, Examples, OnlineResources, FurtherEducation, PeopleContact, Levels) 
                                                    VALUES(@DomainId, @LinkId, @Title, @Text,@Examples, @OnlineResources, @FurtherEducation, @PeopleContact, @Levels )";
               
                result = con.InsertQueryUnScoped(query);

                if (!result.Success)
                {
                    result.Message = "An error occurred";
                    return result;
                }
                var aspectId = (int)result.Entity;

                var listActionsToCreate = entity.ActionsList.Where(x => x.Created);
                var listActionsToCopy = entity.ActionsList.Where(x => !x.Created).ToList();


                foreach (var item in listActionsToCreate)
                {
                    var queryActionCreate = new QueryEntity
                    {
                        Query = @"INSERT into Actions (Text, Title, Type) VALUES(@Text, @Title, @Type)",
                        Entity = item
                    };
                    var resultAction = con.InsertQueryUnScoped(queryActionCreate);
                    if (!resultAction.Success)
                    {
                        result.Message = "An error occurred";
                        return result;
                    }
                    item.ActionId = (int)resultAction.Entity;
                    listActionsToCopy.Add(item);
                }


                foreach (var item in listActionsToCopy)
                {
                    item.AspectId = aspectId;
                    var queryActionCreate = new QueryEntity
                    {
                        Query = @"INSERT into AspectsActions (AspectId, ActionId, LevelAction, Position) VALUES( @AspectId, @ActionId, @LevelAction, @Position)",
                        Entity = item
                    };
                    var resultAction = con.ExecuteQueryUnScoped(queryActionCreate);
                    if (!resultAction.Success)
                    {
                        result.Message = "An error occurred";
                        result.Success = false;
                        return result;
                    }
                }

                //update link with href

                var queryUpdateLink = new QueryEntity()
                {
                    Entity = new {Href = string.Concat("/aspects/" , aspectId), LinkId = linkId },
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
                result.Entity = aspectId;
                result.Message = "The aspect has been created";
            }
            return result;
        }


        public Result InsertTest(int domainId, int copyId)
        {
            var con = new DapperConnectionManager();
            var query = new QueryEntity();
            Result result = null;


            query.Entity = new { DomainId = domainId};
            query.Query = @"SELECT * FROM Aspects where DomainId = @DomainId";

            var domainActions = con.ExecuteQuery<AspectEntity>(query);

            var aspectsList = (IEnumerable<AspectEntity>)domainActions.Entity;

            foreach (var entity in aspectsList)
            {
                using (var scope = new TransactionScope())
                {
                    entity.DomainId = copyId;

                    var linkEntity = new LinkEntity
                    {
                        Href = " ",
                        Name = entity.Title,
                        Type = LinksTypes.ASPECT.ToString(),
                        Active = true
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
                    query.Query = @"INSERT INTO Aspects (DomainId, LinkId, Title, Text, Examples, OnlineResources, FurtherEducation, PeopleContact, Levels) 
                                                    VALUES(@DomainId, @LinkId, @Title, @Text,@Examples, @OnlineResources, @FurtherEducation, @PeopleContact, @Levels )";

                    result = con.InsertQueryUnScoped(query);

                    if (!result.Success)
                    {
                        result.Message = "An error occurred";
                        return result;
                    }
                    var aspectId = (int)result.Entity;


                    var queryUpdateLink = new QueryEntity()
                    {
                        Entity = new { Href = string.Concat("/aspects/", aspectId), LinkId = linkId },
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
                    result.Entity = aspectId;
                    result.Message = "The aspect has been created";
                } 
            }


            
            return result;
        }

        public Result Delete(int id)
        {
            var con = new DapperConnectionManager();

            var result = new Result();

            //checking relations
            var queryCheckDomains = new QueryEntity()
            {
                Query = @"SELECT * from Questions where AspectId = @AspectId",
                Entity = new {AspectId = id}
            };
            var resultDomain = con.ExecuteQuery(queryCheckDomains);

            var questionsActions = resultDomain.Entity as IEnumerable<dynamic>;
            if (questionsActions.Any())
            {
                result.Success = false;
                result.Message = "This aspects is used for questions in the self assessment quiz. Remove the question first.";
                return result;
            }
            
            var query = new QueryEntity()
            {
                Query = @"DECLARE @linkId int;
                          SELECT @linkId = Aspects.LinkId
                          FROM Aspects WHERE AspectId = @AspectId;
                          UPDATE Aspects set Published = @Published where AspectId = @AspectId;
                          UPDATE Links set Active = @Active where LinkId = @LinkId",
                Entity = new { AspectId = id, Active = false }
            };
            result = con.ExecuteQuery(query);
            result.Message = result.Success?"The aspect has been deleted":"An error occurred";
            return result;
        }

        /// <summary>
        /// By default set the sector and the link published/active, could set the value passed as the second parameter as false for unpblish
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
                          SELECT @linkId = Aspects.LinkId
                          FROM Aspects WHERE AspectId = @AspectId;
                          UPDATE Aspects set Published = @Published where AspectId = @AspectId;
                          UPDATE Links set Active = @Published where LinkId = @LinkId",
                Entity = new { AspectId = id, Published = published }
            };
            var result = con.ExecuteQuery(query);
            result.Message = result.Success ? "The aspect has been published" : "An error occurred";
            return result;
        }

        public Result UpdatePosition(int aspectId, int position)
        {
            var con = new DapperConnectionManager();
            var query = new QueryEntity();
            query.Entity = new { AspectId = aspectId, Position = position };
            query.Query = @"UPDATE Aspects set Position = @Position where AspectId = @AspectId";
            var result = con.ExecuteQuery(query);
            result.Message = result.Success ? "The Position has been updated" : "An error occurred";
            return result;
        }
    }
}
