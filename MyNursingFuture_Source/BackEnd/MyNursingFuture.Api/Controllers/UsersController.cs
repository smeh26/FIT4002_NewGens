using System;
using System.Collections.Generic;
using System.Linq;
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
using System.Web;
using System.Web.Hosting;
using Newtonsoft.Json;
using System.Configuration;

namespace MyNursingFuture.Api.Controllers
{
    [ExceptionFilter]
    public class UsersController : ApiController
    {
        private readonly IUsersManager _usersManager;
        private readonly ICacheManager _cacheManager;
        public UsersController(IUsersManager usersManager, ICacheManager cacheManager)
        {
            _usersManager = usersManager;
            _cacheManager = cacheManager;
        }
        [HttpPost]
        // POST: api/Users
        public HttpResponseMessage Post([FromBody]UserEntity value)
        {
            //System.Diagnostics.Debugger.Break();

            var result = new Result();
            if (string.IsNullOrEmpty(value.Email) || string.IsNullOrEmpty(value.Password) || string.IsNullOrEmpty(value.Name))
            {
                result = new Result(false);
                return Request.CreateResponse(HttpStatusCode.BadRequest, result);
            }
            
            result = _usersManager.Register(value);
           
            if (!result.Success)
                return Request.CreateResponse(HttpStatusCode.OK, result);

            var user = new UserModel();
            var userEntity = (UserEntity) result.Entity;
            user.Email = userEntity.Email;
            user.Token = userEntity.Token;
            user.Name = userEntity.Name;
            user.ApnaUser = false;
            user.UserId = userEntity.UserId;
            result.Entity = user;
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [JwtAuthorized]
        [Route("api/users/quiz/career/{complete}")]
        public HttpResponseMessage GetCareerQuizzes(string complete)
        {
            var completeLook = complete == "complete";
            object objuser = null;
            Request.Properties.TryGetValue("user", out objuser);
            var user = objuser as UserEntity;
            var result = _usersManager.GetQuizzes(user.UserId, QuizTypes.PATHWAY.ToString(),completeLook);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }


        [JwtAuthorized]
        [Route("api/users/quiz/career/save")]
        public HttpResponseMessage SaveQuizCareer([FromBody]UsersQuizzesEntity entity)
        {
            object objuser = null;
            Request.Properties.TryGetValue("user", out objuser);
            var user = objuser as UserEntity;
            entity.UserId = user.UserId;
            entity.Type = QuizTypes.PATHWAY.ToString();
            var result = _usersManager.SaveQuiz(entity);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [JwtAuthorized]
        [Route("api/users/quiz/selfassessment/{complete}")]
        public HttpResponseMessage GetAssessmentQuizzes(string complete)
        {
            var completeLook = complete == "complete";
            object objuser = null;
            Request.Properties.TryGetValue("user", out objuser);
            var user = objuser as UserEntity;
            var result = _usersManager.GetQuizzes(user.UserId, QuizTypes.ASSESSMENT.ToString(), completeLook);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }


        [JwtAuthorized]
        [Route("api/users/quiz/selfassessment/save")]
        public HttpResponseMessage SaveAssessmentQuiz([FromBody]UsersQuizzesEntity entity)
        {
            object objuser = null;
            Request.Properties.TryGetValue("user", out objuser);
            var user = objuser as UserEntity;
            entity.UserId = user.UserId;
            entity.Type = QuizTypes.ASSESSMENT.ToString();
            var result = _usersManager.SaveQuiz(entity);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [JwtAuthorized]
        [Route("api/users/quiz/aboutyou/save")]
        public HttpResponseMessage SaveAboutyouQuiz([FromBody]UsersQuizzesEntity entity)
        {
            object objuser = null;
            Request.Properties.TryGetValue("user", out objuser);
            var user = objuser as UserEntity;
            entity.UserId = user.UserId;
            entity.Type = QuizTypes.ASSESSMENT.ToString();
            
            var dictionary = JsonConvert.DeserializeObject<Dictionary<int, object>>(entity.Answers);
            var result = _usersManager.SaveQuiz(entity, dictionary);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [JwtAuthorized]
        [Route("api/users/quizzes")]
        public HttpResponseMessage GetAllQuizzes()
        {
            object objuser = null;
            Request.Properties.TryGetValue("user", out objuser);
            var user = objuser as UserEntity;
            var result = _usersManager.GetQuizzes(user.UserId);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }


        [JwtAuthorized]
        [HttpDelete]
        [Route("api/users/quizzes/{id}")]
        public HttpResponseMessage GetQuizzesby(int id)
        {
            object objuser = null;
            Request.Properties.TryGetValue("user", out objuser);
            var user = objuser as UserEntity;
            var result = _usersManager.GetQuizzes(user.UserId);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [Route("api/users/recover")]
        public HttpResponseMessage Recover([FromBody]UserEntity value)
        {
            if(string.IsNullOrEmpty (value.Email)){
                return Request.CreateResponse(HttpStatusCode.BadRequest, new Result(false));
            }
            if (!value.Email.Contains("@") || value.Email.Length < 3)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, new Result(false));
            }

            var result = _usersManager.GenerateRecoveringCode(value);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [Route("api/users/recover/reset")]
        public HttpResponseMessage ResetPassword([FromBody]UserEntity value)
        {
            if (string.IsNullOrEmpty(value.Token))
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, new Result(false));
            }
            if (string.IsNullOrEmpty(value.Password))
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, new Result(false));
            }
            if ( value.Password.Length < 6)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, new Result(false));
            }
            value.Token = HttpUtility.HtmlDecode(value.Token);
            var result = _usersManager.ResetPassword(value);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [HttpPost]
        [JwtAuthorized]
        [Route("api/users/edit")]
        public HttpResponseMessage EditDetails([FromBody]UserEntity value)
        {
            if (string.IsNullOrEmpty(value.Email) || string.IsNullOrEmpty(value.Name))
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, new Result(false));
            }
            if (!value.Email.Contains("@") || value.Email.Length < 3)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, new Result(false));
            }
            object objuser = null;
            Request.Properties.TryGetValue("user", out objuser);
            var user = objuser as UserEntity;
            value.UserId = user.UserId;
  

            var result = _usersManager.UpdateDetails(value);

            if (result.Success)
            {
                var updated_user = _usersManager.GetUserDetails(user);
                if (updated_user.Success)
                    return Request.CreateResponse(HttpStatusCode.OK, updated_user.Entity);
            }

            return Request.CreateResponse(HttpStatusCode.BadRequest, new Result(false));
        }

        [HttpGet]
        [JwtAuthorized]
        [Route("api/users")]
        public HttpResponseMessage GetCurrentUserDetails()
        {

            object objuser = null;
            Request.Properties.TryGetValue("user", out objuser);
            var user = objuser as UserEntity;

            if ( user == null)
                return Request.CreateResponse(HttpStatusCode.BadRequest, new Result(false));

            var updated_user = _usersManager.GetUserDetails(user);

            if (updated_user.Success)
                return Request.CreateResponse(HttpStatusCode.OK, updated_user.Entity);

            return Request.CreateResponse(HttpStatusCode.BadRequest, new Result(false));
        }

        [JwtAuthorized]
        [Route("api/users/changepassword")]
        public HttpResponseMessage ChangePassword([FromBody]UserEntity value)
        {
            if (string.IsNullOrEmpty(value.NewPassword) || string.IsNullOrEmpty(value.Password))
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, new Result(false));
            }

            if (value.NewPassword.Length < 6|| value.Password.Length < 6)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, new Result(false));
            }

            object objuser = null;
            Request.Properties.TryGetValue("user", out objuser);
            var user = objuser as UserEntity;
            user.Password = value.Password;
            user.NewPassword = value.NewPassword;
            var result = _usersManager.ChangePassword(user);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }


        [JwtAuthorized]
        // DELETE: api/Users/5
        public HttpResponseMessage Delete(int id)
        {
            var result = new Result();
            object objuser = null;
            Request.Properties.TryGetValue("user", out objuser);
            var user = objuser as UserEntity;
            if (user == null)
            {
                result = new Result(false);
                return Request.CreateResponse(HttpStatusCode.BadRequest, result);
            }
            result = _usersManager.Delete(user);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }
    }
}
