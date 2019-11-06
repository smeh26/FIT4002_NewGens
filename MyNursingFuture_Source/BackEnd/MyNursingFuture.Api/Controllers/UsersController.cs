/**
 * 
 * <Author> APNA </Author>
 * <copyright> The following code was the orignial work of APNA.  Edited by Nguyen Pham - 27348032 .  </copyright>
 * <remarks> This is a part of the FIT4002 project. Product owner is APNA. Project supervisor is Robyn McNamara  </remarks>
 * <date>  </date>
 * <summary> </summary>
 */

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
using Newtonsoft.Json.Linq;
using System.Reflection;
using Swashbuckle.Swagger.Annotations;

namespace MyNursingFuture.Api.Controllers
{
    [ExceptionFilter]


    public class UsersController : ApiController
    {
        private readonly IUsersManager _usersManager;
        private readonly ICacheManager _cacheManager;
        private readonly IAnswersManager _answersManager;
        private readonly IQuestionsManager _questionsManager;
        private readonly INurseSelfAssessmentAnswersManager _nurseSelfAssessmentAnswersManager;
        private readonly Dictionary<Tuple<int, decimal>, AnswerEntity> answerDictionary;
        private readonly Dictionary<int, QuestionEntity> questionDictionary;
        public UsersController(IUsersManager usersManager,
            ICacheManager cacheManager,
  
             IAnswersManager answersManager,
             IQuestionsManager questionsManager,
             INurseSelfAssessmentAnswersManager nurseSelfAssessmentAnswersManager
            )
        {
            _usersManager = usersManager;
            _cacheManager = cacheManager;
            _answersManager = answersManager;
            _questionsManager = questionsManager;
            _nurseSelfAssessmentAnswersManager = nurseSelfAssessmentAnswersManager;



            //Get Answers
            var answers_result = _answersManager.Get();
            var answers_List = (List<AnswerEntity>)answers_result.Entity;
            answerDictionary = answers_List.ToDictionary(x => new Tuple<int, decimal>(x.QuestionId, x.Value), x => x);

            //Get Questions
            var questions_result = _questionsManager.Get();
            var questions_List = (List<QuestionEntity>)questions_result.Entity;
            questionDictionary = questions_List.ToDictionary(x => x.QuestionId, x => x);

        }

        /// <summary>
        /// API for registering a user
        /// </summary>
        /// <remarks> Original API - Untouched - Untested  </remarks>
        /// <response code="200"></response>
        /// <response code="400"></response>
        /// <response code="500"></response>
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
            var userEntity = (UserEntity)result.Entity;
            user.Email = userEntity.Email;
            user.Token = userEntity.Token;
            user.Name = userEntity.Name;
            user.ApnaUser = false;
            user.UserId = userEntity.UserId;
            result.Entity = user;
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }


        /// <summary>
        /// Get API to get Career quizz
        /// </summary>
        /// <remarks> Original API - Untouched - Untested  </remarks>
        /// <response code="200"></response>
        /// <response code="400"></response>
        /// <response code="500"></response>
        [JwtAuthorized]
        [Route("api/users/quiz/career/{complete}")]
        public HttpResponseMessage GetCareerQuizzes(string complete)
        {
            var completeLook = complete == "complete";
            object objuser = null;
            Request.Properties.TryGetValue("user", out objuser);
            var user = objuser as UserEntity;
            var result = _usersManager.GetQuizzes(user.UserId, QuizTypes.PATHWAY.ToString(), completeLook);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }


        /// <summary>
        /// API to save quizz career quizz
        /// </summary>
        /// <remarks> TODO: enforce required fields </remarks>
        /// <response code="200"></response>
        /// <response code="400"></response>
        /// <response code="500"></response>
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

        /// <summary>
        /// API to get assessment
        /// </summary>
        /// <remarks> Original API - Untouched - Untested </remarks>
        /// <response code="200"></response>
        /// <response code="400"></response>
        /// <response code="500"></response>
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


        /// <summary>
        /// API to save self assessment
        /// </summary>
        /// <remarks> 
        /// 
        /// The API was modified by Nguyen Pham 29-Oct-2019
        /// Due to the original implementation was used with multiple purposes, 
        /// and the saving of JASON object is not useful for scanning anf filtering records
        /// The code was modified to accomodate JSON parsing of the selfassessment result
        /// into separare objects and trace back to the Question and the Anwser
        /// 
        /// With this method, new records was added to / updated into the NurseSelfAssessmentAnswer table 
        /// Please use the according manager to perform any action with those records,
        /// </remarks>
        /// <response code="200"></response>
        /// <response code="400"></response>
        /// <response code="500"></response>
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

            var error_counter = 0;

            if (!result.Success)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, new Result(false));

            }
            if (result.Success)
            {

                var userQuizid = (int) result.Entity;
                //Console.WriteLine(entity.Results);
                JObject parent_json = JObject.Parse(entity.Results);
                var answer = parent_json.Value<JObject>("answers").Properties();

                var temp_result = new Result();

                if (answer != null)
                {
                    var nurse_answer_dict = answer.ToDictionary(k => Int32.Parse(k.Name), v => Decimal.Parse(v.Value.ToString()));

                    foreach (KeyValuePair<int, decimal> ans in nurse_answer_dict)
                    {
                        NurseSelfAssessmentAnswersEntity ans_entity = new NurseSelfAssessmentAnswersEntity();
                        ans_entity.QuestionId = ans.Key;
                        ans_entity.Value = ans.Value;
                        ans_entity.LastUpdate = entity.DateVal;
                        ans_entity.UserId = entity.UserId;
                        ans_entity.UserQuizId = userQuizid;

                        AnswerEntity answer_entity = null;
                        if (answerDictionary.TryGetValue(new Tuple<int, decimal>(ans_entity.QuestionId, ans_entity.Value), out answer_entity))
                        {
                            ans_entity.AnswerId = answer_entity.AnswerId;
                        }

                        QuestionEntity question_entity = null;
                        if (questionDictionary.TryGetValue(ans.Key, out question_entity))
                        {
                            ans_entity.AspectId = (int)question_entity.AspectId;


                        }
                        // insert answer into database
                        temp_result = _nurseSelfAssessmentAnswersManager.InsertAnswer(entity.UserId, ans_entity);

                        if (!temp_result.Success)
                        {
                            error_counter++;
                        }

                    }


                }
            }

            result.Message += String.Format(". Process ended with {0} error ", error_counter);

            return Request.CreateResponse(HttpStatusCode.OK, result);

        }

        /// <summary>
        /// APi to save about you quizz
        /// </summary>
        /// <remarks> Original API - Untouched - Untested  </remarks>
        /// <response code="200"></response>
        /// <response code="400"></response>
        /// <response code="500"></response>
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

        /// <summary>
        /// API to get user quizzes
        /// </summary>
        /// <remarks> Original API - Untouched - Untested  </remarks>
        /// <response code="200"></response>
        /// <response code="400"></response>
        /// <response code="500"></response>
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


        /// <summary>
        /// API to get user quizzes
        /// </summary>
        /// <remarks> Original API - Untouched - Untested </remarks>
        /// <response code="200"></response>
        /// <response code="400"></response>
        /// <response code="500"></response>
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

        /// <summary>
        /// API to recover account
        /// </summary>
        /// <remarks> Original API - Untouched - Untested  </remarks>
        /// <response code="200"></response>
        /// <response code="400"></response>
        /// <response code="500"></response>
        [Route("api/users/recover")]
        public HttpResponseMessage Recover([FromBody]UserEntity value)
        {
            if (string.IsNullOrEmpty(value.Email))
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, new Result(false));
            }
            if (!value.Email.Contains("@") || value.Email.Length < 3)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, new Result(false));
            }

            var result = _usersManager.GenerateRecoveringCode(value);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        /// <summary>
        /// API to reset your password
        /// </summary>
        /// <remarks> Original API - Untouched - Untested  </remarks>
        /// <response code="200"></response>
        /// <response code="400"></response>
        /// <response code="500"></response>
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
            if (value.Password.Length < 6)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, new Result(false));
            }
            value.Token = HttpUtility.HtmlDecode(value.Token);
            var result = _usersManager.ResetPassword(value);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        /// <summary>
        /// API to update User details
        /// </summary>
        /// <remarks> 
        /// 
        /// * TODO Insert comment
        /// </remarks>
        /// <response code="200"></response>
        /// <response code="400"></response>
        /// <response code="500"></response>
        [HttpPost]
        [JwtAuthorized]
        [Route("api/users/edit")]
        public HttpResponseMessage EditDetails([FromBody]UserModel userModel)
        {
            var value = new UserEntity();
            PropertyCopier<UserModel, UserEntity>.Copy(userModel, value);

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
            value.ModifyDate = DateTime.Now;


            var result = _usersManager.UpdateDetails(value);

            if (result.Success)
            {
                var updated_user = _usersManager.GetUserDetails(user);
                if (updated_user.Success)
                {
                    var user_temp = (UserEntity)updated_user.Entity;

                    PropertyCopier<UserEntity, UserModel>.Copy(user_temp, userModel);
                    return Request.CreateResponse(HttpStatusCode.OK, userModel);
                }

            }

            return Request.CreateResponse(HttpStatusCode.BadRequest, new Result(false));
        }

        /// <summary>
        /// API to get the loggedin  users details
        /// </summary>
        /// <remarks> TODO: enforce required fields </remarks>
        /// <response code="200"></response>
        /// <response code="400"></response>
        /// <response code="500"></response>
        [HttpGet]
        [JwtAuthorized]
        [Route("api/users/details")]
        public HttpResponseMessage GetCurrentUserDetails()
        {

            object objuser = null;
            Request.Properties.TryGetValue("user", out objuser);
            var user = objuser as UserEntity;

            if (user == null)
                return Request.CreateResponse(HttpStatusCode.BadRequest, new Result(false));

            var updated_user = _usersManager.GetUserDetails(user);
            var usermodel = new UserModel();
            PropertyCopier<UserEntity, UserModel>.Copy((UserEntity)updated_user.Entity, usermodel);

            if (updated_user.Success)
                return Request.CreateResponse(HttpStatusCode.OK, updated_user.Entity);

            return Request.CreateResponse(HttpStatusCode.BadRequest, new Result(false));
        }


        /// <summary>
        /// API to change users password
        /// </summary>
        /// <remarks> Original API - Untouched - Untested  </remarks>
        /// <response code="200"></response>
        /// <response code="400"></response>
        /// <response code="500"></response>
        [JwtAuthorized]
        [Route("api/users/changepassword")]
        public HttpResponseMessage ChangePassword([FromBody]UserEntity value)
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
            var user = objuser as UserEntity;
            user.Password = value.Password;
            user.NewPassword = value.NewPassword;
            var result = _usersManager.ChangePassword(user);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        /// <summary>
        /// API to delete/deactivate a nurse
        /// </summary>
        /// <remarks> Original API - Untouched - Untested  </remarks>
        /// <response code="200"></response>
        /// <response code="400"></response>
        /// <response code="500"></response>
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

        // ==========================================================================================================================================
        struct GetUserQuiz_Response
        {
            public string Message { get; set; }
            public bool Success { get; set; }
            public List<NurseSelfAssessmentAnswersEntity> Entity { get; set; }

        }

        /// <summary>
        /// API to get UserQuiz Result by ID 
        /// </summary>
        /// 
        /// 
        /// <remarks> use this API in case you need to fetch a quiz, knowing QuizId 
        /// Authorization from either Nurse or Employer is required
        /// </remarks>
        /// <response code="200"></response>
        /// <response code="400"></response>
        /// <response code="500"></response>
        [HttpGet]
        [GenericJWTAuthorized]
        [Route("api/users/quiz/{id}")]
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(GetUserQuiz_Response))]

        public HttpResponseMessage GetUserQuiz(int id)
        {

            Result result = _nurseSelfAssessmentAnswersManager.GetAnswersbyUserQuizzId(id);

            // return failed 
            if (!result.Success)
                return Request.CreateResponse(HttpStatusCode.BadRequest, result);

            // if success
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }


    }
}
