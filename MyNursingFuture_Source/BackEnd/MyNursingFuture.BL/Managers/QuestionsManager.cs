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
    public interface IQuestionsManager: IManager<QuestionEntity>
    {
        Result GetQuestionsQuizAndText(int id, string text);
        Result GetByAspect(int aspectId);
        Result UpdatePosition(int questionId, int position);
        Result UpdateFieldNameAndPosition(int questionId, int position, string fieldName);
        Result Get();
    }
    public class QuestionsManager : IQuestionsManager
    {
        public Result Delete(int id)
        {
            var con = new DapperConnectionManager();
            var query = new QueryEntity()
            {
                Query = @"Update Questions set Active = 0 Where QuestionId = @QuestionId;",
                Entity = new { QuestionId = id }
            };
            var result = con.ExecuteQuery(query);
            result.Message = result.Success ? "The question has been deleted" : "An error occurred";
            return result;
        }

        public Result Get()
        {
            var con = new DapperConnectionManager();
            var query = new QueryEntity();

            query.Query = @"SELECT * FROM Questions
WHERE AspectId IS NOT NULL
                            ";
            query.Entity = new {};

            var result = con.ExecuteQuery<QuestionEntity>(query);

            if (!result.Success)
            {
                result.Message = "Bad request - Question not found" ;
                return result;
            }

            return result;

        }

        public Result Get(int id)
        {
            var con = new DapperConnectionManager();
            var query = new QueryEntity();

            query.Query = @"SELECT * FROM Questions
                            where QuestionId = @QuestionId";
            query.Entity = new { QuestionId = id };

            var result = con.ExecuteQuery<QuestionEntity>(query);

            if (!result.Success)
            {
                result.Message = "Question not found";
                return result;
            }

            var r = (IEnumerable<QuestionEntity>)result.Entity;

            var question = r.FirstOrDefault(); 

            if (result.Entity == null)
            {
                result.Success = false;
                result.Message = "Question not found";
                return result;
            }

            query.Query = @"SELECT * FROM Answers WHERE QuestionId = @QuestionId";
            query.Entity = new { QuestionId = id};
            result = con.ExecuteQuery<AnswerEntity>(query);
            if (!result.Success)
            {
                result.Message = "An error occurred";
                return result;
            }
            question.Answers = ((IEnumerable<AnswerEntity>)result.Entity).ToList();


            query.Query = @"SELECT * FROM SectorsQuestions WHERE QuestionId = @QuestionId";
            query.Entity = new { QuestionId = id };
            result = con.ExecuteQuery<SectorsQuestionsEntity>(query);
            if (!result.Success)
            {
                result.Message = "An error occurred";
                return result;
            }
            question.SectorsQuestions = ((IEnumerable<SectorsQuestionsEntity>)result.Entity).ToList();


            result.Entity = question;
            return result;
                
        }

        public Result GetByAspect(int aspectId)
        {
            var con = new DapperConnectionManager();
            var query = new QueryEntity();

            query.Query = @"SELECT * FROM Questions
                            where AspectId = @AspectId and Active = 1";
            query.Entity = new { AspectId = aspectId };

            var result = con.ExecuteQuery<QuestionEntity>(query);

            if (!result.Success)
            {
                result.Message = "Question not found";
                return result;
            }

            var r = (IEnumerable<QuestionEntity>)result.Entity;

            var question = r.FirstOrDefault();

            if (question == null)
            {
                result.Success = false;
                result.Message = "Question not found";
                return result;
            }

            query.Query = @"SELECT * FROM Answers WHERE QuestionId = @QuestionId";
            query.Entity = new { QuestionId = question.QuestionId };
            result = con.ExecuteQuery<AnswerEntity>(query);
            if (!result.Success)
            {
                result.Message = "An error occurred";
                return result;
            }
            question.Answers = ((IEnumerable<AnswerEntity>)result.Entity).ToList();

            result.Entity = question;
            return result;

        }

        public Result GetQuestionsQuizAndText(int id, string text)
        {
            var con = new DapperConnectionManager();
            var query = new QueryEntity();
            var result = new Result();


            query.Query = @"SELECT QuestionId, Text FROM Questions
                            where QuizId = @QuizId and Active = 1";
            query.Entity = new { QuizId = id};

            result = con.ExecuteQuery<QuestionEntity>(query);

            if (!result.Success)
            {
                result.Message = "An error occurred";
                return result;
            }

            var r = (IEnumerable<QuestionEntity>)result.Entity;

            result.Entity = r.Where(x=>x.Text.ToLower().Contains(text.Trim().ToLower())).ToList();
            return result;
        }

        public Result UpdatePosition(int questionId, int position)
        {
            var con = new DapperConnectionManager();
            var query = new QueryEntity();
            query.Entity = new { QuestionId = questionId, Position = position };
            query.Query = @"UPDATE Questions set Position = @Position where QuestionId = @QuestionId";
            var result = con.ExecuteQuery(query);
            result.Message = result.Success ? "The Position has been updated" : "An error occurred";
            return result;
        }

        public Result UpdateFieldNameAndPosition(int questionId, int position, string fieldName)
        {
            var con = new DapperConnectionManager();
            var query = new QueryEntity();
            var result = new Result();
            using (var scope = new TransactionScope())
            {
                query.Query = @"DELETE FROM UserDataQuestions where QuestionId = @QuestionId";
                query.Entity = new { QuestionId = questionId };

                var resultDelete = con.ExecuteQueryUnScoped(query);

                if (!resultDelete.Success)
                {
                    return resultDelete;
                }
                if (!string.IsNullOrEmpty(fieldName))
                {
                    query.Query = @"INSERT UserDataQuestions (QuestionId, FieldName) VALUES (@QuestionId, @FieldName)";
                    query.Entity = new { QuestionId = questionId, FieldName = fieldName };
                    var resultUserData = con.ExecuteQueryUnScoped(query);
                    if (!resultUserData.Success)
                    {
                        return resultUserData;
                    }
                }
                

                query.Entity = new { QuestionId = questionId, Position = position };
                query.Query = @"UPDATE Questions set Position = @Position where QuestionId = @QuestionId";
                result = con.ExecuteQuery(query);
                result.Message = result.Success ? "The Position has been updated" : "An error occurred";
                scope.Complete();
            }
            
            return result??new Result(false);
        }

        public Result Insert(QuestionEntity entity)
        {
            var con = new DapperConnectionManager();
            var result = new Result();
            using (var scope = new TransactionScope())
            {
                var query = new QueryEntity();
                query.Entity = entity;
                query.Query = @"INSERT INTO Questions (QuizId, Type, AspectId, Text, SubText, EmployerText, EmployerSubText, Requirements, Examples) 
                            VALUES(@QuizId, @Type, @AspectId, @Text, @SubText, @EmployerText, @EmployerSubText, @Requirements, @Examples)";
                result = con.InsertQueryUnScoped(query);

                if (!result.Success)
                {
                    return result;
                }

                var questionId = (int)result.Entity;

                Result resultAnswers;
                var queryAnswers = new QueryEntity();
                foreach(var q in entity.Answers)
                {
                    q.QuestionId = questionId;
                    queryAnswers.Entity = q;
                    queryAnswers.Query = @"INSERT INTO Answers (QuestionId, Text, EmployerText, Value, MatchText, Type, TextValue) 
                            VALUES(@QuestionId, @Text, @EmployerText, @Value, @MatchText, @Type, @TextValue)";
                    resultAnswers = con.InsertQueryUnScoped(queryAnswers);
                    if (!resultAnswers.Success)
                    {
                        resultAnswers.Message = "An error occurred";
                        return resultAnswers;
                    }
                }
                if(!entity.SectorsQuestions.Any()) {
                    scope.Complete();
                    result.Message = result.Success ? "The Question has been created" : "An error occurred";
                    return result;
                }

                Result resultSectors;
                var querySectors = new QueryEntity();
                
                foreach (var q in entity.SectorsQuestions)
                {
                    q.QuestionId = questionId;
                    queryAnswers.Entity = q;
                    queryAnswers.Query = @"INSERT INTO SectorsQuestions (QuestionId, SectorId, Value) 
                            VALUES(@QuestionId, @SectorId, @Value)";
                    resultSectors = con.ExecuteQueryUnScoped(queryAnswers);
                    if (!resultSectors.Success)
                    {
                        resultSectors.Message = "An error occurred";
                        return resultSectors;
                    }
                }
                scope.Complete();
                result.Message = result.Success ? "The Question has been created" : "An error occurred";
            } 
            return result;
        }

        public Result Update(QuestionEntity entity)
        {
            var con = new DapperConnectionManager();
            var result = new Result();

            using (var scope = new TransactionScope())
            {
                var query = new QueryEntity();
                query.Entity = entity;
                query.Query = @"UPDATE Questions set Text = @Text, SubText = @SubText, EmployerText = IFNULL(@EmployerText, @EmployerText), EmployerSubText = IFNULL(@EmployerSubText, @EmployerSubText)  , SubText = @SubText , Requirements = @Requirements, Examples = @Examples WHERE QuestionId = @QuestionId";
                result = con.ExecuteQueryUnScoped(query);

                if (!result.Success)
                {
                    return result;
                }

                var questionId = entity.QuestionId;

                Result resultAnswers;
                var queryAnswers = new QueryEntity();

                if (entity.Answers.Any())
                {
                    
                    queryAnswers.Entity = new { QuestionId = questionId };
                    queryAnswers.Query = @"DELETE FROM Answers where QuestionId = @QuestionId";
                    resultAnswers = con.ExecuteQueryUnScoped(queryAnswers);

                    if (!resultAnswers.Success)
                    {
                        resultAnswers.Message = "An error occurred";
                        return resultAnswers;
                    }
                }
                

                foreach (var q in entity.Answers)
                {
                    q.QuestionId = questionId;
                    queryAnswers.Entity = q;
                    queryAnswers.Query = @"INSERT INTO Answers (QuestionId, Text, EmployerText , Value, MatchText, Type, TextValue) 
                            VALUES(@QuestionId, @Text, @EmployerText, @Value, @MatchText, @Type, @TextValue)";
                    resultAnswers = con.InsertQueryUnScoped(queryAnswers);
                    if (!resultAnswers.Success)
                    {
                        resultAnswers.Message = "An error occurred";
                        return resultAnswers;
                    }
                }


                if (!entity.SectorsQuestions.Any())
                {
                    scope.Complete();
                    result.Entity = entity.QuestionId;
                    result.Message = result.Success ? "The Question has been created" : "An error occurred";
                    return result;
                }

                var querySectors = new QueryEntity();
                querySectors.Entity = new { QuestionId = questionId };
                querySectors.Query = @"DELETE FROM SectorsQuestions where QuestionId = @QuestionId";
                var resultSectors = con.ExecuteQueryUnScoped(querySectors);
                if (!resultSectors.Success)
                {
                    resultSectors.Message = "An error occurred";
                    return resultSectors;
                }

                foreach (var q in entity.SectorsQuestions)
                {
                    q.QuestionId = questionId;
                    queryAnswers.Entity = q;
                    queryAnswers.Query = @"INSERT INTO SectorsQuestions (QuestionId, SectorId, Value) 
                            VALUES(@QuestionId, @SectorId, @Value)";
                    resultSectors = con.ExecuteQueryUnScoped(queryAnswers);
                    if (!resultSectors.Success)
                    {
                        resultSectors.Message = "An error occurred";
                        return resultSectors;
                    }
                }

                scope.Complete();
                result.Entity = entity.QuestionId;
                result.Message = result.Success ? "The Question has been updated" : "An error updated";
            }

            return result;
        }

        public Result SetPublished(int id, bool published = true)
        {
            throw new InvalidOperationException();
        }
    }
}
