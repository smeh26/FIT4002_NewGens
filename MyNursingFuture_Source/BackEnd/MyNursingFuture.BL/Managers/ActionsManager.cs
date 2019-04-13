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
    public interface IActionsManager:IManager<ActionEntity>
    {
        Result GetLike(string name);
    }
    public class ActionsManager: IActionsManager
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
                Query = @"SELECT * FROM Actions ORDER BY Title ASC"
            };
            return con.ExecuteQuery<ActionEntity>(query);
        }

        public Result GetLike(string name)
        {
            var con = new DapperConnectionManager();
            var query = new QueryEntity
            {
                Entity = new { Name = string.Concat("%",name,"%") },
                Query = @"SELECT TOP 10 * FROM Actions Where  Title like @Name ORDER BY Title ASC"
            };
            return con.ExecuteQuery<ActionEntity>(query);
        }

        public Result Get(int id)
        {
            var con = new DapperConnectionManager();
            var query = new QueryEntity();

            query.Query = @"SELECT * FROM Actions
                            where ActionId = @ActionId";
            query.Entity = new { ActionId = id };

            var result = con.ExecuteQuery<ActionEntity>(query);

            if (!result.Success)
            {
                result.Message = "Action not found";
                return result;
            }

            var r = (IEnumerable<ActionEntity>)result.Entity;

            result.Entity = r.FirstOrDefault();

            if (result.Entity == null)
                result.Success = false;

            return result;
        }

        public Result Update(ActionEntity entity)
        {
            var con = new DapperConnectionManager();
            var query = new QueryEntity();
            query.Entity = entity;
            query.Query = @"UPDATE Actions set Text = @Text, Title = @Title, Type=@Type Where ActionId = @ActionId";
            var result = con.ExecuteQuery(query);
            result.Message = result.Success ? "The action has been updated" : "An error occurred";
            result.Entity = entity.ActionId;
            return result;
        }

        public Result Insert(ActionEntity entity)
        {
            var con = new DapperConnectionManager();
            var query = new QueryEntity();
            query.Entity = entity;
            query.Query = @"INSERT INTO Actions (Text, Title, Type) VALUES(@Text, @Title, @Type)";
            var result = con.InsertQuery(query);
            result.Message = result.Success ? "The action has been created" : "An error occurred";
            return result;
        }

        public Result Delete(int id)
        {
            var con = new DapperConnectionManager();
            var query = new QueryEntity();

            query.Query = @"SELECT * FROM AspectsActions
                            where ActionId = @ActionId";
            query.Entity = new { ActionId = id };

            var result = con.ExecuteQuery(query);
            if (!result.Success)
            {
                result.Message = "An error ocurred";
                return result;
            }

            var listAction = (IEnumerable<dynamic>)result.Entity;
            if (listAction.Any())
            {
                result.Message = "Aspects of practices are using this action, it can't be deleted";
                return result;
            }
            query.Query = @"DELETE FROM DomainsActions where ActionId = @ActionId;
                            DELETE FROM Actions where ActionId = @ActionId;";
            query.Entity = new { ActionId = id };

            result = con.ExecuteQuery(query);
            if (!result.Success)
            {
                result.Message = "An error ocurred";
                return result;
            }
            result.Message = "The action has been deleted";
            return result;
        }
        
    }
}
