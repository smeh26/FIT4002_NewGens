using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyNursingFuture.BL.Entities;
using MyNursingFuture.Util;
using MyNursingFuture.DL;

namespace MyNursingFuture.BL.Managers
{
    public interface IQuizzesManager
    {
        Result GetQuizByType(QuizTypes type);
        Result GetQuizByDomain(int domainId);
    }
    public class QuizzesManager : IQuizzesManager
    {
        
        private Result Insert(QuizEntity entity, DapperConnectionManager con)
        {
            var query = new QueryEntity();
            query.Entity = entity;
            query.Query = @"INSERT INTO Quizzes (Name, Type , DomainId) VALUES(@Name, @Type , @DomainId)";
            var result = con.InsertQuery(query);
            return result;
        }
        

        public Result GetQuizByType(QuizTypes type)
        {
            var con = new DapperConnectionManager();
            var query = new QueryEntity();
            var result = new Result();

            //ASSeSSMENT QUIZZES GET BY DOMAIN NO TYPE
            if(QuizTypes.ASSESSMENT == type)
            {
                result.Success = false;
                return result;
            }

            query.Query = @"SELECT * FROM Quizzes
                            where Type = @Type";
            query.Entity = new { Type = type.ToString() };

            result = con.ExecuteQuery<QuizEntity>(query);

            if (!result.Success)
            {
                result.Message = "An error occurred";
                return result;
            }

            var r = (IEnumerable<QuizEntity>)result.Entity;

            var quiz  = r.FirstOrDefault();

            if (quiz == null)
            {
                //new quiz needs to be inserted
                quiz = new QuizEntity
                {
                    Name = type.ToString(),
                    Type = type.ToString()
                };
                result = Insert(quiz, con);
            }
            else
            {
                var resultQ = GetQuestionsByQuiz(quiz.QuizId, con);
                if (!resultQ.Success)
                {
                    return resultQ;
                }
                quiz.Questions = (List<QuestionEntity>)resultQ.Entity;
            }
            result.Entity = quiz;
            return result;
        }

        public Result GetQuizByDomain(int domainId)
        {
            var con = new DapperConnectionManager();
            var query = new QueryEntity();
            var result = new Result();
            

            query.Query = @"SELECT * FROM Quizzes
                            where DomainId = @DomainId";
            query.Entity = new { DomainId = domainId };

            result = con.ExecuteQuery<QuizEntity>(query);

            if (!result.Success)
            {
                result.Message = "An error occurred";
                return result;
            }

            var r = (IEnumerable<QuizEntity>)result.Entity;

            var quiz = r.FirstOrDefault();

            if (quiz == null)
            {
                var queryDomain = new QueryEntity
                {
                    Entity = new { DomainId = domainId },
                    Query = @"SELECT * FROM Domains WHERE DomainId = @DomainId"
                };
                var resultDomain = con.ExecuteQuery<DomainEntity>(queryDomain);

                if (!resultDomain.Success)
                {
                    return resultDomain;
                }

                var d = (IEnumerable<DomainEntity>)resultDomain.Entity;

                var domain = d.FirstOrDefault();

                //new quiz needs to be inserted
                quiz = new QuizEntity
                {
                    Name = domain.Name,
                    Type = QuizTypes.ASSESSMENT.ToString(),
                    DomainId = domainId
                };
                result = Insert(quiz, con);
            }
            else
            {
                var resultQ = GetQuestionsByQuiz(quiz.QuizId, con);
                if (!resultQ.Success)
                {
                    return resultQ;
                }
                quiz.Questions = (List<QuestionEntity>)resultQ.Entity;
            }
            result.Entity = quiz;
            return result;
        }

        private Result GetQuestionsByQuiz(int quizId, DapperConnectionManager con)
        {
            var query = new QueryEntity();
            var result = new Result();


            query.Query = @"SELECT  udq.FieldName, q.QuestionId, q.QuizId, q.Type, q.AspectId, q.Text, q.SubText, q.Active, q.Requirements, q.Position, q.Examples FROM Questions as q
                            LEFT JOIN UserDataQuestions as udq on q.QuestionId = udq.QuestionId 
                            where q.QuizId = @QuizId and q.Active = 1";
            query.Entity = new { QuizId = quizId };

            result = con.ExecuteQuery<QuestionEntity>(query);

            if (!result.Success)
            {
                result.Message = "An error occurred";
                return result;
            }

            var r = (IEnumerable<QuestionEntity>)result.Entity;

            result.Entity = r.ToList();
            return result;
        }
    }
}
