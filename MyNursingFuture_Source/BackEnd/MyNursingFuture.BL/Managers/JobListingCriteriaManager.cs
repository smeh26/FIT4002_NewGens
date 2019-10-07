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
        Result GetAnswerbyListing(int listingId);
        Result GetAnswerbyListingbyCriteria(int listingId, int criteriaId);
        Result InsertCriterion(JobListingCriteriaEntity criterion);
        Result InsertCriteria(List<JobListingCriteriaEntity> criteria);
        Result UpdateCriterion(JobListingCriteriaEntity criterion);
        Result UpdateCriteria(List<JobListingCriteriaEntity> criteria);
    }
    public class JobListingCriteriaManager : IJobListingCriteriaManager
    {


        public Result GetAnswerbyListing(int jobListingId)
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

        public Result GetAnswerbyListingbyCriteria(int listingId, int criteriaId)
        /*
        * Get one criteria of a listing 
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
                    CriteriaId = criteriaId
                };
                query.Query = @"SELECT * FROM JobListingCriteria WHERE JobListingId = @JobListingId AND  CriteriaId = @CriteriaId  ";
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
                IF EXISTS (SELECT * FROM JobListingCriteria WHERE JobListingId = @JobListingId and AspectId = @AspectId )
                BEGIN
                    UPDATE NurseSelfAssessmentAnswers SET Value = @Value , QuestionId = @QuestionId, AnswerId = @AnswerId , LastUpdate= @LastUpdate
                    WHERE JobListingId = @JobListingId and AspectId = @AspectId
                END 
                ELSE
                BEGIN 
                INSERT INTO JobListingCriteria (JobListingId, AspectId, Value, QuestionId, AnswerId, LastUpdate )
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
                foreach (JobListingCriteriaEntity entity in criteria)
                {
                    // Temporary solution need to impove to become a batch process
                    result = InsertCriterion(entity);

                }

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
