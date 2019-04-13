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
    public interface IMenuManager 
    {
        Result GetByType(string type);
        Result Update(List<MenuEntity> list, string type);
    }
    public class MenusManager: IMenuManager
    {
        public Result GetByType(string type)
        {
            var con = new DapperConnectionManager();
            var query = new QueryEntity
            {
                Entity = new { Type = type },
                Query = @"SELECT * FROM Menus WHERE Type = @Type ORDER BY Position ASC"
            };
            return con.ExecuteQuery<MenuEntity>(query);
        }

        public Result Update(List<MenuEntity> list, string type)
        {
            var con = new DapperConnectionManager();
            
            var queryList = new List<QueryEntity>();
            var query = new QueryEntity
            {
                Query = @"DELETE from Menus WHERE Type = @Type",
                Entity = new {Type = type},
            };
            queryList.Add(query);
            foreach (var item in list)
            {
                item.Type = type;
                var queryItem = new QueryEntity()
                {
                    Entity = item,
                    Query = @"INSERT into Menus (Title, Href, Position, Type, Submenu, Separator) VALUES(@Title, @Href, @Position, @Type, @Submenu, @Separator)"
                };
                queryList.Add(queryItem);
            }
            var result = con.ExecuteQueries(queryList);
            result.Message = result.Success ? "The menu have been modifed" : "An error has ocurred";
            return result;
        }
    }
}
