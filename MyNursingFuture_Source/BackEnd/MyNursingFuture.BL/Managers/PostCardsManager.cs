using System;
using System.Collections.Generic;
using System.Linq;
using MyNursingFuture.BL.Entities;
using MyNursingFuture.DL;
using MyNursingFuture.Util;

namespace MyNursingFuture.BL.Managers
{
    public interface IPostCardsManager : IManager<PostCardEntity>
    {
        
    }
    public class PostCardsManager:IPostCardsManager
    {
        public Result SetPublished(int id, bool published = true)
        {
            throw new InvalidOperationException();
        }
        public Result Get()
        {
            var con = new DapperConnectionManager();
            var query = new QueryEntity
            {
                Entity = new { },
                Query = @"SELECT * FROM PostCards"
            };
            return con.ExecuteQuery<PostCardEntity>(query);
        }

        public Result Get(int id)
        {
            var con = new DapperConnectionManager();
            var query = new QueryEntity();

            query.Query = @"SELECT * FROM PostCards
                            where PostCardId = @PostCardId";
            query.Entity = new { PostCardId = id };

            var result = con.ExecuteQuery<PostCardEntity>(query);

            if (!result.Success)
            {
                result.Message = "PostCard not found";
                return result;
            }

            var r = (IEnumerable<PostCardEntity>)result.Entity;

            result.Entity = r.FirstOrDefault();

            if (result.Entity == null)
                result.Success = false;

            return result;
        }

        public Result Update(PostCardEntity entity)
        {
            ImageManager imageManager = new ImageManager();
            if (entity.ImageFile != null)
            {
                var resImgCheck = imageManager.CheckFileImageAndSave(entity.ImagePath, entity.ImageFile, 30, 30, 3000, 3000);
                if (!resImgCheck.Success)
                {
                    return resImgCheck;
                }
                entity.Image = resImgCheck.Entity.ToString();
                //entity.Text = "Image Postcard";
            }

            var con = new DapperConnectionManager();
            var query = new QueryEntity();
            query.Entity = entity;
            query.Query = @"UPDATE PostCards set Text = @Text, Image = @Image where PostCardId = @PostCardId";
            var result = con.ExecuteQuery(query);
            result.Message = result.Success ? "The PostCard has been updated" : "An error occurred";
            result.Entity = entity.PostCardId;
            return result;
        }

        public Result Insert(PostCardEntity entity)
        {
            ImageManager imageManager = new ImageManager();
            if (entity.ImageFile != null)
            {
                var resImgCheck = imageManager.CheckFileImageAndSave(entity.ImagePath, entity.ImageFile, 100, 100, 3000, 3000);
                if (!resImgCheck.Success)
                {
                    return resImgCheck;
                }
                entity.Image = resImgCheck.Entity.ToString();
                //entity.Text = "Image Postcard";
            }

            var con = new DapperConnectionManager();
            var query = new QueryEntity();
            query.Entity = entity;
            query.Query = @"INSERT INTO PostCards (Text, Image) VALUES(@Text, @Image)";
            var result = con.InsertQuery(query);
            result.Message = result.Success ? "The PostCard has been created" : "An error occurred";
            return result;
        }

        public Result Delete(int id)
        {
            var con = new DapperConnectionManager();
            var query = new QueryEntity()
            {
                Query = @"DELETE FROM PostCards WHERE PostCardId = @PostCardId",
                Entity = new { PostCardId = id }
            };
            var result = con.ExecuteQuery(query);
            result.Message = result.Success ? "The PostCard has been deleted" : "An error occurred";
            return result;
        }
        
    }
}
