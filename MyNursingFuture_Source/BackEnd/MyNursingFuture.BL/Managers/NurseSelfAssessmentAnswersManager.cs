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
        Result GetAnswerbyUserID(int userId);
        Result GetAnswersbyUserQuizzId(int userQuizzId);

        Result InsertAnswer(int userId, NurseSelfAssessmentAnswersEntity answer);
        Result InsertAnswers(int userId, List<NurseSelfAssessmentAnswersEntity> answer_list);
        Result UpdateAnswer(int userId, NurseSelfAssessmentAnswersEntity answer);
        Result UpdateAnswers(int userId, List<NurseSelfAssessmentAnswersEntity> answer_list);
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

        public Result GetAnswerbyUserID(int userId)
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
                query.Entity = new { UserId = userId  };
                query.Query = @"SELECT * FROM NurseSelfAssessmentAnswers WHERE UserId = @UserId";
                result = con.ExecuteQuery<UsersQuizzesEntity>(query);

                return result;
            }
            catch (Exception ex)
            {
                Logger.Log(ex);
            }
            return new Result(false);




        }

        public Result GetAnswersbyUserQuizzId(int userQuizzId)
        /*
        * Get all answers of a UserQuizzId 
        * 
        */
        {
            Result result = new Result();
            try
            {
                var con = new DapperConnectionManager();
                var query = new QueryEntity();
                query.Entity = new { UserQuizId = userQuizzId };
                query.Query = @"SELECT * FROM NurseSelfAssessmentAnswers WHERE UserQuizId = @UserQuizId ";
                result = con.ExecuteQuery<NurseSelfAssessmentAnswersEntity>(query);

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

                String query_string = String.Format(@"
                BEGIN TRAN
                IF EXISTS (SELECT * FROM NurseSelfAssessmentAnswers WHERE UserId = {0} and QuestionId = @QuestionId and UserQuizId = @UserQuizId)
                BEGIN
                    UPDATE NurseSelfAssessmentAnswers SET Value = @Value 
                                                        , LastUpdate= @LastUpdate 
                                                        , QuestionId = @QuestionId 
                                                        , AnswerId = @AnswerId
                                                        , AspectId = @AspectId
                                                        , TextAnswerField = @TextAnswerField
                    WHERE UserId = {0} and QuestionId = @QuestionId and UserQuizId = @UserQuizId
                END 
                ELSE
                BEGIN 
                INSERT INTO NurseSelfAssessmentAnswers (UserId 
                                                        ,AspectId
                                                        ,Value 
                                                        ,QuestionId
                                                        ,AnswerId
                                                        ,TextAnswerField
                                                        ,LastUpdate
                                                        ,UserQuizId)
                                                    VALUES({0}
                                                            , @AspectId
                                                            , @Value 
                                                            , @QuestionId
                                                            ,@AnswerId
                                                            ,@TextAnswerField
                                                            ,@LastUpdate
                                                            ,@UserQuizId)
                END
                COMMIT TRAN

", userId);
                query.Query = query_string;
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
