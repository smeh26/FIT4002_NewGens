using System;
using System.Collections.Generic;
using System.Linq;
using MyNursingFuture.BL.Entities;
using MyNursingFuture.DL;
using MyNursingFuture.Util;

namespace MyNursingFuture.BL.Managers
{
    public interface IEndorsedLogosManager : IManager<EndorsedLogoEntity>
    {
        
    }
    public class EndorsedLogosManager : IEndorsedLogosManager
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
                Query = @"SELECT * FROM EndorsedLogos"
            };
            return con.ExecuteQuery<EndorsedLogoEntity>(query);
        }

        public Result Get(int id)
        {
            var con = new DapperConnectionManager();
            var query = new QueryEntity();

            query.Query = @"SELECT * FROM EndorsedLogos
                            where EndorsedLogoId = @EndorsedLogoId";
            query.Entity = new { EndorsedLogoId = id };

            var result = con.ExecuteQuery<EndorsedLogoEntity>(query);

            if (!result.Success)
            {
                result.Message = "EndorsedLogo not found";
                return result;
            }

            var r = (IEnumerable<EndorsedLogoEntity>)result.Entity;

            result.Entity = r.FirstOrDefault();

            if (result.Entity == null)
                result.Success = false;

            return result;
        }

        public Result Update(EndorsedLogoEntity entity)
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
                //entity.Text = "Image EndorsedLogo";
            }

            var con = new DapperConnectionManager();
            var query = new QueryEntity();
            query.Entity = entity;
            query.Query = @"UPDATE EndorsedLogos set name = @Name, Image = @Image where EndorsedLogoId = @EndorsedLogoId";
            var result = con.ExecuteQuery(query);
            result.Message = result.Success ? "The EndorsedLogo has been updated" : "An error occurred";
            result.Entity = entity.EndorsedLogoId;
            return result;
        }

        public Result Insert(EndorsedLogoEntity entity)
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
                //entity.Text = "Image EndorsedLogo";
            }

            var con = new DapperConnectionManager();
            var query = new QueryEntity();
            query.Entity = entity;
            query.Query = @"INSERT INTO EndorsedLogos (Name, Image) VALUES(@Name, @Image)";
            var result = con.InsertQuery(query);
            result.Message = result.Success ? "The EndorsedLogo has been created" : "An error occurred";
            return result;
        }

        public Result Delete(int id)
        {
            var con = new DapperConnectionManager();
            var query = new QueryEntity()
            {
                Query = @"DELETE FROM EndorsedLogos WHERE EndorsedLogoId = @EndorsedLogoId",
                Entity = new { EndorsedLogoId = id }
            };
            var result = con.ExecuteQuery(query);
            result.Message = result.Success ? "The EndorsedLogo has been deleted" : "An error occurred";
            return result;
        }
        
    }
}
