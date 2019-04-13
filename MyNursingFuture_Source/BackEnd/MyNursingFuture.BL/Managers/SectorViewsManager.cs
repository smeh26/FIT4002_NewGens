using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyNursingFuture.BL.Entities;
using MyNursingFuture.DL;
using MyNursingFuture.Util;

namespace MyNursingFuture.BL.Managers
{
    public interface ISectorViewsManager : IManager<SectorViewEntity>
    {
        Result SetActive(int id, bool active = true);
    }
    public class SectorViewsManager:ISectorViewsManager
    {
        public Result SetPublished(int id, bool published = true)
        {
            throw new InvalidOperationException();
        }
        /// <summary>
        /// Not needed
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public Result Get()
        {
            throw new NotImplementedException();
        }

        public Result Get(int id)
        {
            var con = new DapperConnectionManager();
            var query = new QueryEntity();

            query.Query = @"SELECT * FROM SectorViews
                            where SectorViewId = @SectorViewId";
            query.Entity = new { SectorViewId = id };

            var result = con.ExecuteQuery<SectorViewEntity>(query);

            if (!result.Success)
            {
                result.Message = "Sector View not found";
                return result;
            }

            var r = (IEnumerable<SectorViewEntity>)result.Entity;

            result.Entity = r.FirstOrDefault();

            if (result.Entity == null)
                result.Success = false;


            return result;
        }

        public Result Update(SectorViewEntity entity)
        {
            var con = new DapperConnectionManager();
            
            var query = new QueryEntity();
            query.Entity = entity;
            query.Query = @"UPDATE SectorViews set 
                                      Intro=@Intro,
                                      Video=@Video,
                                      MoreStories=@MoreStories,
                                      CareerPathways=@CareerPathways,
                                      WorkEnvironments=@WorkEnvironments,
                                      CareerOpportunities=@CareerOpportunities,
                                      EducationOpportunities=@EducationOpportunities,
                                      ContactText=@ContactText,
                                      OnlineResources=@OnlineResources
                                      WHERE SectorViewId = @SectorViewId";
            var result = con.ExecuteQuery(query);
            result.Message = result.Success? "The view has been updated" : "An error occurred";
            return result;
        }
        /// <summary>
        /// Not needed
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public Result Insert(SectorViewEntity entity)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Not needed
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public Result Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Result SetActive(int id, bool active = true)
        {
            var con = new DapperConnectionManager();
            var query = new QueryEntity()
            {
                Query = @"UPDATE SectorViews set Active = @Active where SectorViewId = @SectorViewId;",
                Entity = new { SectorViewId = id, Active = active }
            };
            var result = con.ExecuteQuery(query);
            result.Message = result.Success ? "The sector view has been updated" : "An error occurred";
            return result;
        }
    }
}
