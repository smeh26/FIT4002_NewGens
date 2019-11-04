using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyNursingFuture.BL.Entities;
using MyNursingFuture.DL;
using MyNursingFuture.Util;
using System.Web;
using Newtonsoft.Json;
using System.Transactions;
using System.Configuration;

namespace MyNursingFuture.BL.Managers
{
    public class SelfAssessmentManager
    {

        /*
            Get all Self-Assessment Results
     */
        public Result GetSeftAssessmentResults()
        {
            string type = "ASSESSMENT";
            try
            {
                var con = new DapperConnectionManager();
                var query = new QueryEntity();
                query.Entity = new { Type = type };
                query.Query = @"SELECT * FROM UsersQuizzes WHERE and Type = @Type";
                var result = con.ExecuteQuery<UsersQuizzesEntity>(query);
                return result;
            }
            catch (Exception ex)
            {
                Logger.Log(ex);
            }
            return new Result(false);
        }



    }

}
