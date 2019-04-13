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
    public interface ILogChangesManager
    {
        Result Get(int page);
        Result Insert(LogChangeEntity entity);
    }
    public class LogChangesManager : ILogChangesManager
    {
        public Result Get(int page)
        {
            var con = new DapperConnectionManager();
            var query = new QueryEntity
            {
                Entity = new { page },
                Query = @"DECLARE @PageNumber AS INT, @RowspPage AS INT
                        SET @PageNumber = @page
                        SET @RowspPage = 20
                        SELECT *
                        FROM LogChanges
                        ORDER BY LogChangeId DESC
                        OFFSET ((@PageNumber - 1) * @RowspPage) ROWS
                        FETCH NEXT @RowspPage ROWS ONLY;"
            };
            var resultList = con.ExecuteQuery<LogChangeEntity>(query);


            var logChangeRows = new LogChangeRows();
            logChangeRows.List = (IEnumerable<LogChangeEntity>)resultList.Entity;

            var queryRows = new QueryEntity
            {
                Entity = new { page },
                Query = @"SELECT count(*) as r FROM LogChanges"
            };


            var resultRows = con.ExecuteQuery(queryRows);
            logChangeRows.Rows = ((IEnumerable<dynamic>)resultRows.Entity).First().r;

            var result = new Result();
            result.Entity = logChangeRows;
            return result;
        }

        public Result Insert(LogChangeEntity entity)
        {
            var con = new DapperConnectionManager();
            var query = new QueryEntity();
            entity.Date = DateTime.Now;          
            query.Entity = entity;
            query.Query = @"INSERT INTO LogChanges (Name, Username, Date, TableName, Identifier, Operation) 
                                             VALUES(@Name, @Username, @Date, @TableName, @Identifier, @Operation)";
            var result = con.InsertQuery(query);
            return result;
        }
    }
}
