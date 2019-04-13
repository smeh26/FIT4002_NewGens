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
    public interface ICategoriesManager : IManager<CategoryEntity>
    {
        
    }
    public class CategoriesManager:ICategoriesManager
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
                Query = @"SELECT * FROM Categories ORDER BY Name ASC"
            };
            return con.ExecuteQuery<CategoryEntity>(query);
        }

        public Result Get(int id)
        {
            var con = new DapperConnectionManager();
            var query = new QueryEntity();

            query.Query = @"SELECT * FROM Categories
                            where CategoryId = @CategoryId";
            query.Entity = new { CategoryId = id };

            var result = con.ExecuteQuery<CategoryEntity>(query);

            if (!result.Success)
            {
                result.Message = "Category not found";
                return result;
            }

            var r = (IEnumerable<CategoryEntity>)result.Entity;

            result.Entity = r.FirstOrDefault();

            if (result.Entity == null)
                result.Success = false;

            return result;
        }

        public Result Update(CategoryEntity entity)
        {
            var con = new DapperConnectionManager();
            var query = new QueryEntity();
            query.Entity = entity;
            query.Query = @"Update Categories set Name = @Name where CategoryId = @CategoryId";
            var result = con.ExecuteQuery(query);
            result.Entity = entity.CategoryId;
            result.Message = result.Success ? "The category has been update" : "An error occurred";
            return result;
        }

        public Result Insert(CategoryEntity entity)
        {
            var con = new DapperConnectionManager();
            var query = new QueryEntity();
            query.Entity = entity;
            query.Query = @"Insert into Categories (Name) VALUES(@Name)";
            var result = con.InsertQuery(query);
            result.Message = result.Success ? "The category has been created" : "An error occurred";
            return result;
        }

        public Result Delete(int id)
        {
            var con = new DapperConnectionManager();
            var query = new QueryEntity();

            query.Query = @"SELECT * FROM Articles
                            where CategoryId = @CategoryId";
            query.Entity = new { CategoryId = id };

            var result = con.ExecuteQuery<CategoryEntity>(query);

            if (!result.Success)
            {
                result.Message = "Category not found";
                return result;
            }

            var r = (IEnumerable<CategoryEntity>)result.Entity;
            if (r.Any())
            {
                result.Success = false;
                result.Entity = null;
                result.Message = "Articles are using this category, it can not be deleted";
                return result;
            }

            query.Query = @"DELETE FROM Categories
                            where CategoryId = @CategoryId";

            return con.ExecuteQuery(query);
        }
        
    }
}
