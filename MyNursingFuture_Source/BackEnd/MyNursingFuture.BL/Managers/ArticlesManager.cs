using MyNursingFuture.BL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyNursingFuture.Util;
using MyNursingFuture.DL;
using System.Transactions;

namespace MyNursingFuture.BL.Managers
{
    public interface IArticlesManager:IManager<ArticleEntity>
    {
        Result Reasons();
        Result UpdateReasons(List<ReasonEntity> list);
    }
    public class ArticlesManager : IArticlesManager
    {
        public Result Delete(int id)
        {
            var con = new DapperConnectionManager();
            


            var query = new QueryEntity()
            {
                Query = @"DECLARE @linkId int;
                          SELECT @linkId = Articles.LinkId
                          FROM Articles WHERE ArticleId = @ArticleId;
                          UPDATE Articles set Active = 0 WHERE ArticleId = @ArticleId;
                          UPDATE Links set Active = 0  Where LinkId = @linkId;",
                Entity = new { ArticleId = id }
            };
            var result = con.ExecuteQuery(query);
            result.Message = result.Success ? "The section has been deleted" : "An error occurred";
            return result;
        }

        public Result Get()
        {
            var con = new DapperConnectionManager();
            var query = new QueryEntity();

            query.Query = @"SELECT A.ArticleId, A.Title, A.Name, A.CategoryId, A.Published, A.Date, C.Name as CategoryName From Articles as A
                            JOIN Categories as C on A.CategoryId = C.CategoryId WHERE Active = 1 ORDER BY A.Date DESC";

            query.Entity = new { };

            var result = con.ExecuteQuery<ArticleEntity>(query);
            return result;
        }

        public Result Get(int id)
        {
            var con = new DapperConnectionManager();
            var query = new QueryEntity();

            query.Query = @"SELECT a.ArticleId, a.Name as ArticleName, a.Title as ArticleTitle, a.CategoryId, a.Published, a.Type as ArticleType,
                                   c.Name, c.Title, c.Type, c.ContentItemId, c.Position, c.Text 
                            from Articles as a
                            LEFT JOIN ContentItems as c
                            on a.ArticleId = c.ArticleId
                            where a.ArticleId = @ArticleId and a.Active = 1
                            ORDER BY c.Position ASC";
            query.Entity = new { ArticleId = id };

            var result = con.ExecuteQuery(query);
            if (!result.Success)
            {
                result.Message = "Article not found";
                return result;
            }
            var r = (IEnumerable<dynamic>)result.Entity;

            if (!r.Any())
            {
                return result;
            }
            var f = r.First();
            var articleEntity = new ArticleEntity();
            articleEntity.ArticleId = f.ArticleId;
            articleEntity.Name = f.ArticleName;
            articleEntity.Title = f.ArticleTitle;
            articleEntity.CategoryId = f.CategoryId;
            articleEntity.Published = f.Published;
            articleEntity.Type = f.ArticleType;
            articleEntity.ContentItems = new List<ContentItemEntity>();
            foreach (var item in r)
            {
                if (item.ContentItemId == null)
                    continue;
                var contentItem = new ContentItemEntity
                {
                    Name = item.Name,
                    ContentItemId = item.ContentItemId,
                    Text = item.Text,
                    Position = item.Position,
                    Type = item.Type,
                    ArticleId = item.ArticleId,
                    Title = item.Title,
                    TextShort = (item.Text as string ?? string.Empty).Truncate()
                };

                articleEntity.ContentItems.Add(contentItem);
            }
            result.Entity = articleEntity;
            return result;
        }

        public Result Insert(ArticleEntity entity)
        {
            var con = new DapperConnectionManager();
            var query = new QueryEntity();
            var result = new Result();
            using (var scope = new TransactionScope())
            {
                var linkEntity = new LinkEntity
                {
                    Href = " ",
                    Name = entity.Title,
                    Type = "ARTICLE",
                    Active = false
                };

                query.Entity = linkEntity;
                query.Query = @"INSERT INTO Links (Name, Type, Href, Active) VALUES(@Name, @Type, @Href, @Active)";
                entity.Date = DateTime.Now;

                result = con.InsertQueryUnScoped(query);
                if (!result.Success)
                {
                    return result;
                }
                var linkId = (int)result.Entity;

                entity.LinkId = linkId;
                query.Entity = entity;
                query.Query = @"INSERT INTO Articles (Name, Title, Type, CategoryId, LinkId, Published, Date) VALUES(@Name, @Title, @Type, @CategoryId, @LinkId, @Published, @Date)";
                result = con.InsertQueryUnScoped(query);

                if (!result.Success)
                {
                    result.Message = "An error occurred";
                    return result;
                }

                //LINK HREF UPDATE
                var queryUpdateLink = new QueryEntity()
                {
                    Entity = new { Href = "/articles/" + (int)result.Entity, LinkId = linkId },
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
            result.Message = "The Article has been created";
            return result;
        }

        public Result SetPublished(int id, bool published = true)
        {
            var con = new DapperConnectionManager();
            var query = new QueryEntity()
            {
                Query = @"DECLARE @linkId int;
                          SELECT @linkId = Articles.LinkId
                          FROM Articles WHERE ArticleId = @ArticleId;
                          UPDATE Articles set Published = @Published where ArticleId = @ArticleId;
                          UPDATE Links set Active = @Published where LinkId = @LinkId",
                Entity = new { ArticleId = id, Published = published }
            };
            var result = con.ExecuteQuery(query);
            result.Message = result.Success ?
                (published ?
                    "The article has been published" :
                    "The article has been unpublished"
                 ) :
                "An error occurred";
            return result;
        }

        public Result Update(ArticleEntity entity)
        {
            var con = new DapperConnectionManager();
            var query = new QueryEntity();
            query.Entity = entity;
            query.Query = @"UPDATE Articles set Name = @Name, Title = @Title, CategoryId = @CategoryId Where ArticleId = @ArticleId";
            var result = con.ExecuteQuery(query);
            result.Message = result.Success ? "The article has been updated" : "An error occurred";
            result.Entity = entity.ArticleId;
            return result;
        }

        public Result Reasons()
        {
            var con = new DapperConnectionManager();
            var query = new QueryEntity();
            var result = new Result();


            query.Query = @"SELECT * FROM Reasons";
            query.Entity = new {};

            result = con.ExecuteQuery<ReasonEntity>(query);

            return result;
        }

        public Result UpdateReasons(List<ReasonEntity> list)
        {
            var con = new DapperConnectionManager();
            var query = new QueryEntity();
            var result = new Result();

            using(var scope = new TransactionScope())
            {
                query.Query = @"DELETE FROM Reasons";
                query.Entity = new { };
                result = con.ExecuteQueryUnScoped(query);
                if (!result.Success)
                {
                    result.Message = "An error occurred";
                    return result;
                }
                foreach (var item in list)
                {
                    query.Query = @"INSERT into  Reasons (Ix, Text, Title, TextPrev) VALUES(@Ix, @Text, @Title, @TextPrev)";
                    query.Entity = item;
                    result = con.ExecuteQueryUnScoped(query);
                    if (!result.Success)
                    {
                        result.Message = "An error occurred";
                        return result;
                    }
                }
                scope.Complete();
            }
            result.Message = "Reasons updated";
            return result;
        }
    }
}
