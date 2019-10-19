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
    public interface IJobListingCriteriaManager
    {
        Result GetCriteriaByListingId(int listingId);
        Result GetAnswerbyListingbyAspect(int listingId, int aspectId);
        Result InsertCriterion(JobListingCriteriaEntity criterion);
        Result InsertCriteria(List<JobListingCriteriaEntity> criteria);
        Result UpdateCriterion(JobListingCriteriaEntity criterion);
        Result UpdateCriteria(List<JobListingCriteriaEntity> criteria);
    }
    public class JobListingCriteriaManager : IJobListingCriteriaManager
    {


        public Result GetCriteriaByListingId(int jobListingId)
        /*
        * Get all criteria of a listing 
        * 
        */
        {
            Result result = new Result();
            try
            {
                var con = new DapperConnectionManager();
                var query = new QueryEntity();
                query.Entity = new { JobListingId = jobListingId };
                query.Query = @"SELECT * FROM JobListingCriteria WHERE JobListingId = @JobListingId ";
                result = con.ExecuteQuery<JobListingCriteriaEntity>(query);

                return result;
            }
            catch (Exception ex)
            {
                Logger.Log(ex);
                result.Success = false;
                result.Message = ex.Message;
            }

            return result;
        }

        public Result GetAnswerbyListingbyAspect(int listingId, int aspectId)
        /*
        * Get one aspect of a listing 
        * 
        */
        {
            Result result = new Result();
            try
            {
                var con = new DapperConnectionManager();
                var query = new QueryEntity();
                query.Entity = new
                {
                    JobListingId = listingId,
                    AspectId = aspectId
                };
                query.Query = @"SELECT * FROM JobListingCriteria WHERE JobListingId = @JobListingId AND  AspectId = @AspectId  ";
                result = con.ExecuteQuery<JobListingCriteriaEntity>(query);

                return result;
            }
            catch (Exception ex)
            {
                Logger.Log(ex);
                result.Success = false;
                result.Message = ex.Message;
            }

            return result;
        }

        public Result InsertCriterion(JobListingCriteriaEntity criterion)
        /*
         * Insert one answer (one aspect) into the database
         * 
         */

        {
            Result result = new Result();

            //TODO: 
            try
            {
                var con = new DapperConnectionManager();
                var query = new QueryEntity();
                if (criterion == null)
                {
                    result = null;
                    result.Success = false;
                    result.Message = "Login error";
                    return result;
                }
                query.Entity = criterion;
                /* query.Entity = new { UserId = userId,
                     AspectId = answer.AspectId,
                    // AnswerId = answer.AnswerId,
                     Value = answer.Value

                 };*/

                // check if there is any records exist if yes, update, else, insert
                // Assume that Listing ID and AspectID are enforced at the controller

                query.Query = @"
                BEGIN TRAN
                IF EXISTS (SELECT * FROM dbo.JobListingCriteria WHERE JobListingId = @JobListingId and AspectId = @AspectId )
                BEGIN
                    UPDATE dbo.JobListingCriteria SET Value = @Value , QuestionId = @QuestionId, AnswerId = @AnswerId , LastUpdate= @LastUpdate
                    WHERE JobListingId = @JobListingId and AspectId = @AspectId
                END 
                ELSE
                BEGIN 
                INSERT INTO dbo.JobListingCriteria (JobListingId, AspectId, Value, QuestionId, AnswerId, LastUpdate )
                                                    VALUES(@JobListingId, @AspectId, @Value, @QuestionId, @AnswerId, @LastUpdate)
                END
                COMMIT TRAN
";
                result = con.ExecuteQuery<UsersQuizzesEntity>(query);

                return result;
            }
            catch (Exception ex)
            {
                Logger.Log(ex);
                result.Success = false;
                result.Message = ex.Message;
            }

            return result;
        }

        public Result InsertCriteria(List<JobListingCriteriaEntity> criteria)
        /*
        * Insert a lisy of Answers into the database
        * 
        */
        {
            Result result = new Result();

            //TODO: 

            try
            {
                var con = new DapperConnectionManager();
                var query = new QueryEntity();
                var results = new List<Result>();
                foreach (JobListingCriteriaEntity entity in criteria)

                {
                    // Temporary solution need to impove to become a batch process
                    if (entity == null) continue;
                    result = new Result();
                    result = InsertCriterion(entity);

                    results.Add(result);

                }
                foreach(Result r in results)
                {
                    if (!r.Success)
                    {
                        return new Result(false);
                    }


                }


                return new Result(true);
            }
            catch (Exception ex)
            {
                Logger.Log(ex);
                result.Success = false;
                result.Message = ex.Message;
            }

            return result;
        }

        public Result UpdateCriterion(JobListingCriteriaEntity criterion)
        /*
         * Update one Answer entry
         * Currently a wrapper of insert Answer as they can do both at once.
         */
        {
            Result result = new Result();

            //TODO: 

            try
            {

                result = InsertCriterion(criterion);
                return result;
            }
            catch (Exception ex)
            {
                Logger.Log(ex);
                result.Success = false;
                result.Message = ex.Message;
            }

            return result;
        }



        public Result UpdateCriteria(List<JobListingCriteriaEntity> criteria)
        /*
         * Update a list of answers
         */
        {
            Result result = new Result();

            //TODO: 

            try
            {
                result = InsertCriteria(criteria);

                return result;
            }
            catch (Exception ex)
            {
                Logger.Log(ex);
                result.Success = false;
                result.Message = ex.Message;
            }

            return result;
        }


    }
}
