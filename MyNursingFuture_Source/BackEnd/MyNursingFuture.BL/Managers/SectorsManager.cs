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
    public interface ISectorsManager : IManager<SectorEntity>
    {
        
    }
    public class SectorsManager: ISectorsManager
    {
        public Result Get()
        {
            var con = new DapperConnectionManager();
            var query = new QueryEntity
            {
                Entity = new { },
                Query = @"SELECT * FROM Sectors where Active = 1 ORDER BY Name ASC "
            };
            return con.ExecuteQuery<SectorEntity>(query);
        }

        public Result Get(int id)
        {
            var con = new DapperConnectionManager();
            var query = new QueryEntity();

            query.Query = @"SELECT s.SectorId, s.Name, s.LinkId, s.Title, s.Published,
                                   v.SectorViewId, v.Type, v.Framework, v.Active
                            FROM Sectors as s
                            JOIN SectorViews as v
                            on s.SectorId = v.SectorId
                            where s.SectorId = @SectorId
                            ORDER BY s.Name ASC";
            query.Entity = new { SectorId = id };

            var result = con.ExecuteQuery(query);
            if (!result.Success)
            {
                result.Message = "Sector not found";
                return result;
            }
            var r = (IEnumerable<dynamic>)result.Entity;

            if (!r.Any())
            {
                return result;
            }

            var sectorEntity = new SectorEntity();
            sectorEntity.SectorId = r.First().SectorId;
            sectorEntity.Name = r.First().Name;
            sectorEntity.Title = r.First().Title;
            List<SectorViewEntity> list = new List<SectorViewEntity>();
            foreach (var item in r)
            {
                var view = new SectorViewEntity()
                {
                    Type = item.Type,
                    Framework = item.Framework,
                    SectorViewId = item.SectorViewId,
                    Active = item.Active
                };

                list.Add(view);
            }
            sectorEntity.SectorEn = list.FirstOrDefault(x => x.Type == "EN");
            sectorEntity.SectorRn = list.FirstOrDefault(x => x.Type == "RN");

            result.Entity = sectorEntity;
            return result;
        }

        public Result GetAll(int id)
        {
            var con = new DapperConnectionManager();
            var query = new QueryEntity();

            query.Query = @"SELECT s.SectorId, s.Name, s.LinkId, s.Title, s.Published,
                                   v.SectorViewId, v.Type, v.Framework, v.Intro,                              
                                  ,v.Video
                                  ,v.MoreStories
                                  ,v.CareerPathways
                                  ,v.WorkEnvironments
                                  ,v.CareerOpportunities
                                  ,v.EducationOpportunities
                                  ,v.ContactText
                            FROM Sectors
                            JOIN SectorViews as v
                            on s.SectorId = v.SectorId
                            where s.SectorId = @SectorId
                            ORDER BY c.Position ASC";
            query.Entity = new { SectorId = id };

            var result = con.ExecuteQuery(query);
            if (!result.Success)
            {
                result.Message = "Sector not found";
                return result;
            }
            var r = (IEnumerable<dynamic>)result.Entity;

            if (!r.Any())
            {
                return result;
            }

            var sectorEntity = new SectorEntity();
            sectorEntity.SectorId = r.First().SectorId;
            sectorEntity.Name = r.First().Name;
            sectorEntity.Title = r.First().Title;
            List<SectorViewEntity> list = new List<SectorViewEntity>();
            foreach (var item in r)
            {
                var view = new SectorViewEntity()
                {
                    Type = item.Type,
                    CareerOpportunities = item.CareerOpportunities,
                    CareerPathways = item.CareerPathways,
                    ContactText = item.ContactText,
                    EducationOpportunities = item.EducationOppotunities,
                    Framework = item.Framework,
                    Intro = item.Intro,
                    MoreStories = item.MoreStories,
                    SectorId = item.SectorId,
                    SectorViewId = item.SectorViewId,
                    Video = item.Video,
                    WorkEnvironments = item.WorkEnviroments
                };

                list.Add(view);
            }
            sectorEntity.SectorEn = list.FirstOrDefault(x => x.Type == "EN");
            sectorEntity.SectorRn = list.FirstOrDefault(x => x.Type == "RN");

            result.Entity = sectorEntity;
            return result;
        }

        public Result Update(SectorEntity entity)
        {
            var con = new DapperConnectionManager();
            
            var query = new QueryEntity();
            query.Entity = entity;
            query.Query = @"UPDATE Sectors set 
                                      Title=@Title 
                                      WHERE SectorId = @SectorId";
            var result = con.ExecuteQuery(query);

            result.Message = result.Success ? "The sector has been updated" : "An error occurred";
            result.Entity = entity.SectorId;
            return result;
        }

        public Result Insert(SectorEntity entity)
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
                    Type = LinksTypes.SECTOR.ToString(),
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
                query.Query = @"INSERT INTO Sectors (Name, Title, Published, LinkId) VALUES(@Name, @Title, @Published, @LinkId)";
                result = con.InsertQueryUnScoped(query);

                if (!result.Success)
                {
                    result.Message = "An error occurred";
                    return result;
                }
                var sectorId = (int)result.Entity;

                var sectorViewRn = new SectorViewEntity
                {
                    Type = "RN",
                    Framework = "rn",
                    SectorId = sectorId
                };

                query.Entity = sectorViewRn;
                query.Query = @"INSERT INTO SectorViews (Type, Framework, SectorId) VALUES(@Type, @Framework, @SectorId)";
                result = con.InsertQueryUnScoped(query);
                if (!result.Success)
                {
                    result.Message = "An error occurred";
                    return result;
                }

                var sectorViewEn = new SectorViewEntity
                {
                    Type = "EN",
                    Framework = "en",
                    SectorId = sectorId
                };

                query.Entity = sectorViewEn;
                query.Query = @"INSERT INTO SectorViews (Type, Framework, SectorId) VALUES(@Type, @Framework, @SectorId)";
                result = con.InsertQueryUnScoped(query);
                if (!result.Success)
                {
                    result.Message = "An error occurred";
                    return result;
                }

                //LINK HREF UPDATE
                var queryUpdateLink = new QueryEntity()
                {
                    Entity = new { Href = "/sections/" + sectorId, LinkId = linkId },
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
                result.Entity = sectorId;
                result.Message = "The sector has been created";
            }
            return result;
        }

        public Result Delete(int id)
        {
            var con = new DapperConnectionManager();

            var queryCheck = new QueryEntity()
            {
                Entity = new { SectorId = id },
                Query = @"SELECT * FROM SectorsQuestions WHERE SectorId = @SectorId"
            };

            var resultCheck = con.ExecuteQuery(queryCheck);
            if (!resultCheck.Success)
            {
                resultCheck.Message = "An error occurred";
                return resultCheck;
            }

            if (((IEnumerable<dynamic>)resultCheck.Entity).Any())
            {
                resultCheck.Message = "Questions depends on this sector, it can't be deleted";
                resultCheck.Success = false;
                return resultCheck;
            }


            var query = new QueryEntity()
            {
                Query = @"DECLARE @linkId int;
                          SELECT @linkId = Sectors.LinkId
                          FROM Sectors WHERE SectorId = @SectorId;
                          Update SectorViews set Active = 0 WHERE SectorId = @SectorId;
                          Update Sectors set Active = 0 WHERE SectorId = @SectorId;
                          Update Links set Active = 0 Where LinkId = @linkId;",
                Entity = new { SectorId = id }
            };
            var result = con.ExecuteQuery(query);
            result.Message = result.Success ? "The sector has been deleted" : "An error occurred";
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
                          SELECT @linkId = Sectors.LinkId
                          FROM Sectors WHERE SectorId = @SectorId;
                          UPDATE Sectors set Published = @Published where SectorId = @SectorId;
                          UPDATE Links set Active = @Published where LinkId = @LinkId",
                Entity = new { SectorId = id, Published = published }
            };
            var result = con.ExecuteQuery(query);
            result.Message = result.Success ? "The sector has been updated" : "An error occurred";
            return result;
        }
    }
}
