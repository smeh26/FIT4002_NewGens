using System;
using System.Collections.Generic;
using System.Linq;
using MyNursingFuture.BL.Entities;
using MyNursingFuture.Util;
using MyNursingFuture.DL;

namespace MyNursingFuture.BL.Managers
{
    public interface IContentItemsManager : IManager<ContentItemEntity>
    {
        Result InsertGeneric(int sectionId, string type);
        Result InsertGenericArticle(int articleId, string type);
        Result UpdatePosition(int positionId, int position);
    }

    public class ContentItemsManager : IContentItemsManager
    {
        public Result SetPublished(int id, bool published = true)
        {
            throw new InvalidOperationException();
        }
        public Result Get()
        {
            throw new NotImplementedException();
        }

        public Result Get(int id)
        {
            var con = new DapperConnectionManager();
            var query = new QueryEntity();

            query.Query = @"SELECT * FROM ContentItems
                            where ContentItemId = @ContentItemId";
            query.Entity = new { ContentItemId = id };

            var result = con.ExecuteQuery<ContentItemEntity>(query);
            
            if (!result.Success)
            {
                result.Message = "Content Item not found";
                return result;
            }

            var r = (IEnumerable<ContentItemEntity>)result.Entity;

            result.Entity = r.FirstOrDefault();

            if (result.Entity == null)
                result.Success = false;

            return result;
        }

        public Result Update(ContentItemEntity entity)
        {
            var con = new DapperConnectionManager();
            ImageManager imageManager = new ImageManager();
            if (entity.ImagePosted != null)
            {
                var resImgCheck = imageManager.CheckFileImageAndSave(entity.ImagePath, entity.ImagePosted, 30, 30, 3000, 3000);
                if (!resImgCheck.Success)
                {
                    return resImgCheck;
                }
                entity.Image = resImgCheck.Entity.ToString();
            }

            if (entity.TitleImagePosted != null)
            {
                var resImgCheck = imageManager.CheckFileImageAndSave(entity.ImagePath, entity.TitleImagePosted, 30, 30, 3000, 3000);
                if (!resImgCheck.Success)
                {
                    return resImgCheck;
                }
                entity.TitleImage = resImgCheck.Entity.ToString();
            }

            var query = new QueryEntity();
            query.Entity = entity;
            query.Query = @"UPDATE ContentItems set 
                                      Name=@Name,
                                      Title=@Title,
                                      SectionId=@SectionId,
                                      ArticleId=@ArticleId,
                                      Text=@Text,
                                      Type=@Type,
                                      Image=@Image,
                                      Carousel=@Carousel,
                                      Link=@Link,
                                      ButtonLink=@ButtonLink,
                                      Video=@Video,
                                      TitleImage = @TitleImage
                                      WHERE ContentItemId = @ContentItemId";
            var result = con.ExecuteQuery(query);
            result.Message = result.Success ? "The content item has been updated" : "An error occurred";
            result.Entity = entity.ContentItemId;
            return result;
        }

        public Result Insert(ContentItemEntity entity)
        {
            var con = new DapperConnectionManager();

            ImageManager imageManager = new ImageManager();
            if (entity.ImagePosted != null)
            {
                var resImgCheck = imageManager.CheckFileImageAndSave(entity.ImagePath, entity.ImagePosted, 30, 30, 3000, 3000);
                if (!resImgCheck.Success)
                {
                    return resImgCheck;
                }
                entity.Image = resImgCheck.Entity.ToString();
            }

            if (entity.TitleImagePosted != null)
            {
                var resImgCheck = imageManager.CheckFileImageAndSave(entity.ImagePath, entity.TitleImagePosted, 30, 30, 3000, 3000);
                if (!resImgCheck.Success)
                {
                    return resImgCheck;
                }
                entity.TitleImage = resImgCheck.Entity.ToString();
            }

            var query = new QueryEntity();
            query.Entity = entity;

            query.Query = @"INSERT INTO ContentItems (
                            Name,
                            Title,
                            Position,
                            SectionId,
                            ArticleId,
                            Text,
                            Type,
                            Image,
                            Carousel,
                            Link,
                            ButtonLink,
                            Video,
                            TitleImage) 
                            VALUES(
                               @Name,
                               @Title,
                               @Position,
                               @SectionId,
                               @ArticleId,
                               @Text,
                               @Type,
                               @Image,
                               @Carousel,
                               @Link,
                               @ButtonLink,
                               @Video,
                               @TitleImage
                            )";
            var result = con.InsertQuery(query);
            result.Message = result.Success ? "The content item has been created" : "An error occurred";
            return result;
        }

        public Result InsertGeneric(int sectionId, string type)
        {
            var result = new Result();

            var con = new DapperConnectionManager();
            var query = new QueryEntity();
            query.Entity = new { SectionId = sectionId, Type = type, Position = 0 };

            query.Query = @"INSERT INTO ContentItems (SectionId, Position,Type) 
                            VALUES(@SectionId,  @Position, @Type)";
            result = con.InsertQuery(query);
            result.Message = result.Success ? "The content item has been created" : "An error occurred";
            return result;
        }

        public Result Delete(int id)
        {
            var con = new DapperConnectionManager();
            var query = new QueryEntity()
            {
                Query = @"DELETE FROM ContentItems WHERE ContentItemId = @ContentItemId",
                Entity = new { ContentItemId = id }
            };
            var result = con.ExecuteQuery(query);
            result.Message = result.Success ? "The content item has been deleted" : "An error occurred";
            return result;
        }

        public Result InsertGenericArticle(int articleId, string type)
        {
            var result = new Result();

            var con = new DapperConnectionManager();
            var query = new QueryEntity();
            query.Entity = new { ArticleId = articleId, Type = type, Position = 100 };

            query.Query = @"INSERT INTO ContentItems (ArticleId, Position,Type) 
                            VALUES(@ArticleId,  @Position, @Type)";
            result = con.InsertQuery(query);
            result.Message = result.Success ? "The content item has been created" : "An error occurred";
            return result;
        }

        public Result UpdatePosition(int contentItemId, int position)
        {
            var con = new DapperConnectionManager();
            var query = new QueryEntity();
            query.Entity = new { ContentItemId = contentItemId, Position = position};
            query.Query = @"UPDATE ContentItems set 
                                      Position = @Position
                                      WHERE ContentItemId = @ContentItemId";
            var result = con.ExecuteQuery(query);
            result.Message = result.Success ? "The content item has been updated" : "An error occurred";
            return result;
        }
    }
}
