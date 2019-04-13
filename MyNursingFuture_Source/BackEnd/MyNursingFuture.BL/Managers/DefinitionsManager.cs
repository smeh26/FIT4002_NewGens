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
    public interface IDefinitionsManager : IManager<DefinitionEntity>
    {
        
    }
    public class DefinitionsManager: IDefinitionsManager
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
                Query = @"SELECT * FROM Definitions ORDER BY Name ASC"
            };
            return con.ExecuteQuery<DefinitionEntity>(query);
        }

        public Result Get(int id)
        {
            var con = new DapperConnectionManager();
            var query = new QueryEntity();

            query.Query = @"SELECT * FROM Definitions
                            where DefinitionId = @DefinitionId";
            query.Entity = new { DefinitionId = id };

            var result = con.ExecuteQuery<DefinitionEntity>(query);

            if (!result.Success)
            {
                result.Message = "Definition not found";
                return result;
            }

            var r = (IEnumerable<DefinitionEntity>)result.Entity;

            result.Entity = r.FirstOrDefault();

            if (result.Entity == null)
                result.Success = false;

            return result;
        }

        public Result Update(DefinitionEntity entity)
        {
            var con = new DapperConnectionManager();
            var query = new QueryEntity();
            query.Entity = entity;
            query.Query = @"UPDATE Definitions set Name = @Name, Text = @Text where DefinitionId = @DefinitionId";
            var result = con.ExecuteQuery(query);
            result.Message = result.Success ? "The Definition has been updated" : "An error occurred";
            result.Entity = entity.DefinitionId;
            return result;
        }

        public Result Insert(DefinitionEntity entity)
        {
            var con = new DapperConnectionManager();
            var query = new QueryEntity();
            query.Entity = entity;
            query.Query = @"INSERT INTO Definitions (Name, Text) VALUES(@Name, @Text)";
            var result = con.InsertQuery(query);
            result.Message = result.Success ? "The Definition has been created" : "An error occurred";
            return result;
        }

        public Result Delete(int id)
        {
            var con = new DapperConnectionManager();
            var query = new QueryEntity()
            {
                Query = @"DELETE FROM Definitions WHERE DefinitionId = @DefinitionId",
                Entity = new { DefinitionId = id }
            };
            var result = con.ExecuteQuery(query);
            result.Message = result.Success ? "The Definition has been deleted" : "An error occurred";
            return result;
        }
        
    }
}
