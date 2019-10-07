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
{   public interface INurseSelfAssessmentAnswersManager {
        public Result GetAnswerbyID(int userId);
        Result InsertAnswer(int userId, NurseSelfAssessmentAnswersEntity answer);
        Result InsertAnswers(int userId, List<NurseSelfAssessmentAnswersEntity> answer_list);
        Result UpdateAnswer(int userId, NurseSelfAssessmentAnswersEntity answer);
        Result UpdateAnswers(int userId, List<NurseSelfAssessmentAnswersEntity> answer_list)
    }
    public class NurseSelfAssessmentAnswersManager : INurseSelfAssessmentAnswersManager
    {
        public Result GetAllQuizz_OldDB()
        /*
        * This function retrive all records that are not null in the old database
        * 
        */
        {
            try
            {
                var con = new DapperConnectionManager();
                var query = new QueryEntity();
                query.Entity = new { };
                query.Query = @"SELECT * FROM UsersQuizzes WHERE Results IS NOT NULL AND TYPE = 'ASSESSMENT'";
                var result = con.ExecuteQuery<UsersQuizzesEntity>(query);

                return result;
            }
            catch (Exception ex)
            {
                Logger.Log(ex);
            }
            return new Result(false);

        }

        public Result GetAnswerbyID(int userId)
        /*
        * Get all answers of a user 
        * 
        */
        {
            Result result = new Result();
            try
            {
                var con = new DapperConnectionManager();
                var query = new QueryEntity();
                query.Entity = new { };
                query.Query = @"SELECT * FROM UsersQuizzes WHERE Results IS NOT NULL AND TYPE = 'ASSESSMENT'";
                result = con.ExecuteQuery<UsersQuizzesEntity>(query);

                return result;
            }
            catch (Exception ex)
            {
                Logger.Log(ex);
            }
            return new Result(false);




        }

        public Result InsertAnswer(int userId, NurseSelfAssessmentAnswersEntity answer)
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
                if (answer == null) {
                    result = null;
                    result.Success = false;
                    result.Message = "Login error";
                    return result;
                }
                query.Entity = answer;
               /* query.Entity = new { UserId = userId,
                    AspectId = answer.AspectId,
                   // AnswerId = answer.AnswerId,
                    Value = answer.Value

                };*/

                // check if there is any records exist if yes, update, else, insert


                query.Query = @"
                BEGIN TRAN
                IF EXISTS (SELECT * FROM NurseSelfAssessmentAnswers WHERE UserId = @UserId and AspectId = @AspectId )
                BEGIN
                    UPDATE NurseSelfAssessmentAnswers SET Value = @Value , LastUpdate= @LastUpdate
                    WHERE UserId = @UserId and AspectId = @AspectId
                END 
                ELSE
                BEGIN 
                INSERT INTO NurseSelfAssessmentAnswers (UserId, AspectId, Value)
                                                    VALUES(@UserId, @AspectId, @Value)
";
                result = con.ExecuteQuery<UsersQuizzesEntity>(query);

                return result;
            }
            catch (Exception ex)
            {
                Logger.Log(ex);
            }
            return new Result(false);
        }

        public Result InsertAnswers(int userId, List<NurseSelfAssessmentAnswersEntity> answer_list)
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
                foreach (NurseSelfAssessmentAnswersEntity entity in answer_list)
                {
                     // Temporary solution need to impove to become a batch process
                    result = InsertAnswer(userId, entity);

                }

                result = con.ExecuteQuery<UsersQuizzesEntity>(query);

                return result;
            }
            catch (Exception ex)
            {
                Logger.Log(ex);
            }
            return new Result(false);
        }

        public Result UpdateAnswer(int userId, NurseSelfAssessmentAnswersEntity answer)
            /*
             * Update one Answer entry
             * Currently a wrapper of insert Answer as they can do both at once.
             */
        {
            Result result = new Result();

            //TODO: 

            try
            {

                result = InsertAnswer(userId, answer);
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



        public Result UpdateAnswers(int userId, List<NurseSelfAssessmentAnswersEntity> answer_list)
            /*
             * Update a list of answers
             */
        {
            Result result = new Result();

            //TODO: 

            try
            {
                result = InsertAnswers(userId, answer_list);

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
