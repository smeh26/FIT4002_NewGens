using MyNursingFuture.BL.Entities;
using MyNursingFuture.DL;
using MyNursingFuture.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNursingFuture.BL.Managers
{
    public interface IAppConfigurationsManager
    {
        Result GetConfiguration(string name);
        Result SetConfiguration(string name,string value);
    }
    public class AppConfigurationsManager : IAppConfigurationsManager
    {
        public Result GetConfiguration(string name)
        {
            var con = new DapperConnectionManager();
            var query = new QueryEntity();

            query.Query = @"SELECT * FROM Configurations
                            where Name = @Name";
            query.Entity = new { Name = name };

            var result = con.ExecuteQuery<ConfigurationEntity>(query);

            if (!result.Success)
            {
                result.Message = "Configuration not found";
                return result;
            }

            var r = (IEnumerable<ConfigurationEntity>)result.Entity;

            result.Entity = r.FirstOrDefault();

            if (result.Entity == null)
                result.Success = false;

            return result;
        }

        public Result SetConfiguration(string name, string value )
        {
            var con = new DapperConnectionManager();
            var result = GetConfiguration(name);
            var query = new QueryEntity();
            query.Entity = new { Name = name, Value = value, DateModified = DateTime.Now};
            if (result.Success)
            {
                query.Query = @"UPDATE Configurations set Value = @Value, DateModified = @DateModified where Name = @Name";
            }
            else
            {
                query.Query = @"INSERT INTO Configurations (Name, Value, DateModified) VALUES (@Name, @Value, @DateModified) ";
            }
            return con.ExecuteQuery(query);
        }
    }
}
