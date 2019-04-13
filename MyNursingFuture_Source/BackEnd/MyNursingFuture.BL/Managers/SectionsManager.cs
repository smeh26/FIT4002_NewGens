using System;
using System.Collections.Generic;
using System.Linq;
using MyNursingFuture.BL.Entities;
using MyNursingFuture.DL;
using MyNursingFuture.Util;
using System.Transactions;

namespace MyNursingFuture.BL.Managers
{
    public interface ISectionsManager : IManager<SectionEntity>
    {
    }
    public class SectionsManager:ISectionsManager
    {
        public Result Get()
        {
            var con = new DapperConnectionManager();
            var query = new QueryEntity();

            query.Query = @"SELECT * From Sections WHERE Active = 1";

            query.Entity = new {  };

            var result = con.ExecuteQuery<SectionEntity>(query);
            return result;
        }

        public Result Get(int id)
        {
            var con = new DapperConnectionManager();
            var query = new QueryEntity();

            query.Query = @"SELECT s.SectionId, s.Name as SectionName, s.Title as SectionTitle,
                                   c.Name, c.Title, c.Type, c.ContentItemId, c.Position, c.Text from Sections as s
                            LEFT JOIN ContentItems as c
                            on s.SectionId = c.SectionId
                            where s.SectionId = @SectionId
                            ORDER BY c.Position ASC";
            query.Entity = new {SectionId = id};

            var result = con.ExecuteQuery(query);
            if (!result.Success)
            {
                result.Message = "Section not found";
                return result;
            }
            var r = (IEnumerable<dynamic>) result.Entity;

            if (!r.Any())
            {
                return result;
            }

            var sectionEntity = new SectionEntity();
            sectionEntity.SectionId = r.First().SectionId;
            sectionEntity.Name = r.First().SectionName;
            sectionEntity.Title = r.First().SectionTitle;
            sectionEntity.ContentItems = new List<ContentItemEntity>();
            foreach (var item in r)
            {
                if(item.ContentItemId == null)
                    continue;
               var contentItem = new ContentItemEntity
                {
                    Name = item.Name,
                    ContentItemId = item.ContentItemId,
                    Text = item.Text,
                    Position = item.Position,
                    Type = item.Type,
                    SectionId = item.SectionId,
                    Title = item.Title,
                    TextShort = (item.Text as string ?? string.Empty).Truncate()
                };

                sectionEntity.ContentItems.Add(contentItem);
            }
            result.Entity = sectionEntity;
            return result;
        }

        public Result Update(SectionEntity entity)
        {
            var con = new DapperConnectionManager();
            var query = new QueryEntity();
            query.Entity = entity;
            query.Query = @"UPDATE Sections set Name = @Name, Title = @Title Where SectionId = @SectionId";
            var result = con.ExecuteQuery(query);
            result.Message = result.Success ? "The section has been updated" : "An error occurred";
            result.Entity = entity.SectionId;
            return result;
        }

        public Result Insert(SectionEntity entity)
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
                    Type = "SECTION",
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
                query.Query = @"INSERT INTO Sections (Name, Title, Sealed, Published, LinkId) VALUES(@Name, @Title, @Sealed, @Published, @LinkId)";
                result = con.InsertQueryUnScoped(query);

                if (!result.Success)
                {
                    result.Message = "An error occurred";
                    return result;
                }

                //LINK HREF UPDATE
                var queryUpdateLink = new QueryEntity()
                {
                    Entity = new { Href = "/sections/" + (int)result.Entity, LinkId = linkId },
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
            result.Message = "The section has been created";
            return result;
        }

        public Result Delete(int id)
        {
            var con = new DapperConnectionManager();

            var queryCheck = new QueryEntity()
            {
                Query = @"SELECT SectionId FROM Sections where SectionId = @SectionId and Sealed = 0",
                Entity = new {SectionId = id}
            };

            var resCheck = con.ExecuteQuery<int>(queryCheck);
            if (!resCheck.Success)
            {
                resCheck.Message = "An error has ocurred";
                return resCheck;
            }

            var entity = (IEnumerable<int>)resCheck.Entity;

            if (!entity.Any())
            {
                resCheck.Success = false;
                resCheck.Message = "That section can not be deleted";
                return resCheck;
            }


            var query = new QueryEntity()
            {
                Query = @"DECLARE @linkId int;
                          SELECT @linkId = Sections.LinkId
                          FROM Sections WHERE SectionId = @SectionId;
                          Update Sections Set Active = 0 WHERE SectionId = @SectionId;
                          Update Links set Active = 0 Where LinkId = @linkId;",
                Entity = new {SectionId = id}
            };
            var result = con.ExecuteQuery(query);
            result.Message = result.Success ? "The section has been deleted" : "An error occurred";
            return result;
        }

        /// <summary>
        /// By default set the section and the link published/active, if specify set the value passed as the second parameter
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
                          SELECT @linkId = Sections.LinkId
                          FROM Sections WHERE SectionId = @SectionId;
                          UPDATE Sections set Published = @Published where SectionId = @SectionId;
                          UPDATE Links set Active = @Published where LinkId = @LinkId",
                Entity = new { SectionId = id, Published = published }
            };
            var result = con.ExecuteQuery(query);
            result.Message = result.Success ? 
                (published?
                    "The section has been published": 
                    "The section has been unpublished"
                 ) : 
                "An error occurred";
            return result;
        }
    }
}
