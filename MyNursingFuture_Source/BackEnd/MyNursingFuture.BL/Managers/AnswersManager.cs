using MyNursingFuture.BL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyNursingFuture.Util;
using MyNursingFuture.DL;
using System.Transactions;

namespace MyNursingFuture.BL.Managers
{
    public interface IAnswersManager : IManager<AnswerEntity>
    {
        
    }
    public class AnswersManager:IAnswersManager
    {
        public Result Get()
        {
            var con = new DapperConnectionManager();
            var query = new QueryEntity();

            query.Query = @"SELECT * FROM(
SELECT *,  RANK() OVER
 (PARTITION BY a.QuestionId , a.Value ORDER BY a.AnswerId) RK

FROM Answers  as a
) as d

WHERE  d.RK  = 1 and d.Type IS NULL
                            ";
            query.Entity = new { };

            var result = con.ExecuteQuery<AnswerEntity>(query);

            if (!result.Success)
            {
                result.Message = "Answers not found";
                return result;
            }

            return result;
        }

        public Result Get(int id)
        {
            throw new NotImplementedException();
        }

        public Result Update(AnswerEntity entity)
        {
            throw new NotImplementedException();
        }

        public Result Insert(AnswerEntity entity)
        {
            throw new NotImplementedException();
        }

        public Result Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Result SetPublished(int id, bool published = true)
        {
            throw new NotImplementedException();
        }
    }
}
