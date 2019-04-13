using System;
using System.Collections.Generic;
using System.Linq;
using MyNursingFuture.BL.Entities;
using MyNursingFuture.DL;
using MyNursingFuture.Util;
using System.Data.SqlClient;


namespace MyNursingFuture.BL.Managers
{
    public interface IDataExtractManager
    {
        Result Get(string spName, string type, DateTime startDate, DateTime endDate);
    }
    public class DataExtractManager: IDataExtractManager
    {
        public Result Get(string spName,string extractType,DateTime dateStart, DateTime dateEnd )
        {
            var con = new DapperConnectionManager();
            var query = new QueryEntity
            {
                Query = spName,
                Entity = new { ExtractType = extractType, DateStart = dateStart.Date.ToString("yyyyMMdd"), DateEnd = dateEnd.Date.ToString("yyyyMMdd") }
            };

            return con.ExecuteStoredProcedure(query);
        }     
    }
}
