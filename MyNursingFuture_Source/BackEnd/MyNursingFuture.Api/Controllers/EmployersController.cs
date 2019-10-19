using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Results;
using MyNursingFuture.Api.Models;
using MyNursingFuture.BL.Entities;
using MyNursingFuture.BL.Managers;
using MyNursingFuture.Util;
using MyNursingFuture.Api.Filters;
using System.Web.Hosting;
using Newtonsoft.Json;
using System.Configuration;

namespace MyNursingFuture.Api.Controllers
{
    [ExceptionFilter]

    public class EmployersController: ApiController
    {
        private readonly IEmployersManager _employersManager;
        private readonly ICacheManager _cacheManager;

        public EmployersController(IEmployersManager employersManager, ICacheManager cacheManager)
        {
            _employersManager = employersManager;
            _cacheManager = cacheManager;
        }

        // POST: api/employers
        [Route("api/v1/employers")]
        public HttpResponseMessage RegisterEmployer([FromBody]EmployerEntity value)
        {
           // System.Diagnostics.Debugger.Break();
            var dump = ObjectDumper.Dump(value);

  
            //
            Console.WriteLine(dump);
            Result result = null;
            if (string.IsNullOrEmpty(value.Email) || string.IsNullOrEmpty(value.Password) || string.IsNullOrEmpty(value.EmployerName))
            {
                result = new Result(false);
                return Request.CreateResponse(HttpStatusCode.BadRequest, result);
            }

            result = _employersManager.Register(value);

            if (!result.Success)
                return Request.CreateResponse(HttpStatusCode.OK, result);

            var employer = new EmployerModel();
            var employerEntity = (EmployerEntity)result.Entity;
            employer.Email = employerEntity.Email;
            employer.Token = employerEntity.Token;
            employer.EmployerName = employerEntity.EmployerName;
            //employer.ApnaUser = false;
            employer.EmployerID = employerEntity.EmployerId;
            result.Entity = employer;
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

       /* [JwtAuthorized]
        [Route("api/employers/quiz/career/{complete}")]
        public HttpResponseMessage GetCareerQuizzes(string complete)
        {
            var completeLook = complete == "complete";
            object objuser = null;
            Request.Properties.TryGetValue("user", out objuser);
            var user = objuser as UserEntity;
            var result = _employersManager.GetQuizzes(user.UserId, QuizTypes.PATHWAY.ToString(), completeLook);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }*/


/*        [JwtAuthorized]
        [Route("api/employers/quiz/career/save")]
        public HttpResponseMessage SaveQuizCareer([FromBody]UsersQuizzesEntity entity)
        {
            object objuser = null;
            Request.Properties.TryGetValue("user", out objuser);
            var user = objuser as UserEntity;
            entity.UserId = user.UserId;
            entity.Type = QuizTypes.PATHWAY.ToString();
            var result = _employersManager.SaveQuiz(entity);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }*/

/*        [JwtAuthorized]
        [Route("api/employers/quiz/selfassessment/{complete}")]
        public HttpResponseMessage GetAssessmentQuizzes(string complete)
        {
            var completeLook = complete == "complete";
            object objuser = null;
            Request.Properties.TryGetValue("user", out objuser);
            var user = objuser as UserEntity;
            var result = _employersManager.GetQuizzes(user.UserId, QuizTypes.ASSESSMENT.ToString(), completeLook);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }*/


/*        [JwtAuthorized]
        [Route("api/employers/quiz/selfassessment/save")]
        public HttpResponseMessage SaveAssessmentQuiz([FromBody]UsersQuizzesEntity entity)
        {
            object objuser = null;
            Request.Properties.TryGetValue("user", out objuser);
            var user = objuser as UserEntity;
            entity.UserId = user.UserId;
            entity.Type = QuizTypes.ASSESSMENT.ToString();
            var result = _employersManager.SaveQuiz(entity);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }*/

/*        [JwtAuthorized]
        [Route("api/employers/quiz/aboutyou/save")]
        public HttpResponseMessage SaveAboutyouQuiz([FromBody]UsersQuizzesEntity entity)
        {
            object objuser = null;
            Request.Properties.TryGetValue("user", out objuser);
            var user = objuser as UserEntity;
            entity.UserId = user.UserId;
            entity.Type = QuizTypes.ASSESSMENT.ToString();

            var dictionary = JsonConvert.DeserializeObject<Dictionary<int, object>>(entity.Answers);
            var result = _employersManager.SaveQuiz(entity, dictionary);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }*/

/*        [JwtAuthorized]
        [Route("api/employers/quizzes")]
        public HttpResponseMessage GetAllQuizzes()
        {
            object objuser = null;
            Request.Properties.TryGetValue("user", out objuser);
            var user = objuser as UserEntity;
            var result = _employersManager.GetQuizzes(user.UserId);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }
*/

/*        [JwtAuthorized]
        [HttpDelete]
        [Route("api/employers/quizzes/{id}")]
        public HttpResponseMessage GetAllQuizzes(int id)
        {
            object objuser = null;
            Request.Properties.TryGetValue("user", out objuser);
            var user = objuser as UserEntity;
            var result = _employersManager.GetQuizzes(user.UserId);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }*/

        [Route("api/employers/recover")]
        public HttpResponseMessage Recover([FromBody]EmployerEntity value)
        {
            if (string.IsNullOrEmpty(value.Email))
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, new Result(false));
            }
            if (!value.Email.Contains("@") || value.Email.Length < 3)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, new Result(false));
            }

            var result = _employersManager.GenerateRecoveringCode(value);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [Route("api/employers/recover/reset")]
        public HttpResponseMessage ResetPassword([FromBody]EmployerEntity value)
        {
            if (string.IsNullOrEmpty(value.Token))
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, new Result(false));
            }
            if (string.IsNullOrEmpty(value.Password))
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, new Result(false));
            }
            if (value.Password.Length < 6)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, new Result(false));
            }
            value.Token = HttpUtility.HtmlDecode(value.Token);
            var result = _employersManager.ResetPassword(value);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [EmployerJWTAuthorized]
        [Route("api/employers/edit")]
        public HttpResponseMessage EditDetails([FromBody]EmployerEntity value)
        {
            if (string.IsNullOrEmpty(value.Email) || string.IsNullOrEmpty(value.EmployerName))
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, new Result(false));
            }
            if (!value.Email.Contains("@") || value.Email.Length < 3)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, new Result(false));
            }
            object objuser = null;
            Request.Properties.TryGetValue("user", out objuser);
            var employer = objuser as EmployerEntity;
            employer.Email = value.Email;
            employer.EmployerName = value.EmployerName;
            var result = _employersManager.UpdateDetails(employer);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [EmployerJWTAuthorized]
        [Route("api/employers/changepassword")]
        public HttpResponseMessage ChangePassword([FromBody]EmployerEntity value)
        {
            if (string.IsNullOrEmpty(value.NewPassword) || string.IsNullOrEmpty(value.Password))
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, new Result(false));
            }

            if (value.NewPassword.Length < 6 || value.Password.Length < 6)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, new Result(false));
            }

            object objuser = null;
            Request.Properties.TryGetValue("user", out objuser);
            var user = objuser as EmployerEntity;
            user.Password = value.Password;
            user.NewPassword = value.NewPassword;
            var result = _employersManager.ChangePassword(user);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }


        [EmployerJWTAuthorized]
        // DELETE: api/employers/5
        public HttpResponseMessage Delete(int id)
        {
            Result result = null;
            object objuser = null;
            Request.Properties.TryGetValue("user", out objuser);
            var employer = objuser as EmployerEntity;
            if (employer == null)
            {
                result = new Result(false);
                return Request.CreateResponse(HttpStatusCode.BadRequest, result);
            }
            result = _employersManager.Delete(employer);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

    }
}