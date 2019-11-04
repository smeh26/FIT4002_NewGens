using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using MyNursingFuture.Api.Models;
using MyNursingFuture.BL.Entities;
using MyNursingFuture.BL.Managers;
using MyNursingFuture.Util;
using System.Threading.Tasks;
using MyNursingFuture.Api.Infrastructure;
using System.Configuration;
using MyNursingFuture.Api.Filters;
using Newtonsoft.Json;
using System.Web.Hosting;
using Swashbuckle.Swagger.Annotations;

namespace MyNursingFuture.Api.Controllers
{
    [ExceptionFilter]
    public class LoginController : ApiController
    {
        private readonly IUsersManager _usersManager;
        private readonly IAppConfigurationsManager _configManager;
        private readonly ICacheManager _cacheManager;
        public LoginController(IUsersManager usersManager, IAppConfigurationsManager configManager, ICacheManager cacheManager)
        {
            _usersManager = usersManager;
            _configManager = configManager;
            _cacheManager = cacheManager;
            
        }
        private struct UserLoginReturnType
        {

            public string Message { get; set; }
            public bool Success { get; set; }
            public UserModel Entity { get; set; }

        }

        /// <summary>
        /// Employer registration API
        /// </summary>
        /// <remarks> TODO: enforce required fields
        /// 
        /// Implement object copy function to Filter out unnessary fields  
        /// </remarks>
        /// <response code="200"></response>
        /// <response code="400"></response>
        /// <response code="500"></response>
        [HttpPost]
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(UserLoginReturnType))]
        // POST: api/Login
        public async Task<HttpResponseMessage> Post([FromBody]LoginObject loginobject)
        {
            var value = new UserEntity();
            PropertyCopier<LoginObject, UserEntity>.Copy(loginobject, value);

            var result = new Result();
            var tokenLogin = false;
            var apnaLogin = false;

            var token = Request.Headers.Authorization;
            if (token != null)
            {
                tokenLogin = true;
                result = _usersManager.Login(token.Parameter);
            }

            if ((string.IsNullOrEmpty(value.Email) || string.IsNullOrEmpty(value.Password)) && !tokenLogin)
            {
                result = new Result(false);
                return Request.CreateResponse(HttpStatusCode.BadRequest, result);
            }
            
            if(!tokenLogin && !apnaLogin)
            {
                result = _usersManager.Login(value);
                apnaLogin = !result.Success;
            }
            // Disable APNA login for now 
            Result apnaUserResult = null;
            var tryLoginApna = ConfigurationManager.AppSettings["apna.login"] == "true";
            //APNA CRM LOGIN
            if (!result.Success && tryLoginApna)
            {
                var resultToken = await GetApnaToken();
                if (resultToken.Success)
                {
                    var config = (ConfigurationEntity)resultToken.Entity;
                    var resultCheckApnaUser = await GetApnaUser(config.Value, value.Email, value.Password);
                    if (resultCheckApnaUser.Success)
                    {
                        apnaUserResult = await GetApnaUserData(config.Value, value.Email);
                        if (apnaUserResult.Success)
                        {
                            var apnaUser = (ApnaUser)apnaUserResult.Entity;
                            var userEntityApna = new UserEntity();
                            userEntityApna.ApnaMemberId = apnaUser.MemberId;
                            userEntityApna.Email = apnaUser.Email;
                            userEntityApna.State = apnaUser.HomeAddress.State;
                            userEntityApna.Suburb = apnaUser.HomeAddress.Suburb;
                            userEntityApna.PostalCode = apnaUser.HomeAddress.Postcode;
                            userEntityApna.Country = apnaUser.HomeAddress.Country;
                            userEntityApna.Name = String.Concat((apnaUser.FirstName ?? ""), " ", (apnaUser.LastName ?? ""));

                            result = _usersManager.LoginApna(userEntityApna);
                        }
                    }
                }
            }

            if (!result.Success)
                return Request.CreateResponse(HttpStatusCode.BadRequest, result);

           

            var user = new UserModel();
            var userEntity = (UserEntity)result.Entity;

            PropertyCopier<UserEntity, UserModel>.Copy(userEntity, user);
            result.Entity = user;

            return Request.CreateResponse(HttpStatusCode.OK, result);
        }
       
        // DELETE: api/Login/5
        public HttpResponseMessage Delete(int id, [FromBody]UserEntity value)
        {
            var result = new Result();
            if (string.IsNullOrEmpty(value.Token) || value.UserId == 0)
            {
                result = new Result(false);
                return Request.CreateResponse(HttpStatusCode.BadRequest, result);
            }

            result = _usersManager.Delete(value);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        private async Task<Result> GetApnaToken()
        {
            var resultConfig = _configManager.GetConfiguration(ConfigNames.APNATOKEN.ToString());
            if (resultConfig.Success)
            {
                var configurationEntity = (ConfigurationEntity)resultConfig.Entity;

                if (configurationEntity.DateModified.Date >= DateTime.Now.Date.AddDays(-5))
                {
                    return resultConfig;
                } 
            }

            try
            {
                var userName = ConfigurationManager.AppSettings["apna.user"];
                var password = ConfigurationManager.AppSettings["apna.password"];
                var url = string.Concat(ConfigurationManager.AppSettings["apna.url"],"token");

                HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Post, url);
                var keyValues = new List<KeyValuePair<string, string>>();
                keyValues.Add(new KeyValuePair<string, string>("grant_type", "password"));
                keyValues.Add(new KeyValuePair<string, string>("username", userName));
                keyValues.Add(new KeyValuePair<string, string>("password", password));

                requestMessage.Content = new FormUrlEncodedContent(keyValues);

                
                string json = null;

                using (var responseGetToken = await HttpClientManager.Client.SendAsync(requestMessage))                
                    if (responseGetToken.StatusCode == HttpStatusCode.OK)
                        using (HttpContent content = responseGetToken.Content)
                        {
                            json = content.ReadAsStringAsync().Result;
                            var result = new Result();
                            var responseApna = JsonConvert.DeserializeObject<ApnaTokenResponse>(json);
                            resultConfig = _configManager.SetConfiguration(ConfigNames.APNATOKEN.ToString(), responseApna.access_token);
                            if (!resultConfig.Success)
                            {
                                result.Success = false;
                                return result;
                            }
                            var configEntity = new ConfigurationEntity();
                            configEntity.Value = responseApna.access_token;
                            result.Entity = configEntity;
                            return result;
                        }
                
            }
            catch (Exception ex)
            {
                Logger.Log(ex);
                return new Result(false);
            }
            
            return new Result(true);
        }


        private async Task<Result> GetApnaUser(string token, string username, string password)
        {
            var result = new Result();
            try
            {
                var url = string.Concat(ConfigurationManager.AppSettings["apna.url"], "/api/Member/IsMember?password=",password, "&username=",username);

                HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Get, url);

                requestMessage.Headers.Add("Authorization",string.Concat("Bearer ",token));


                string json = null;
                
                using (var responseGetToken = await HttpClientManager.Client.SendAsync(requestMessage))
                    if (responseGetToken.StatusCode == HttpStatusCode.OK)
                        using (HttpContent content = responseGetToken.Content)
                        {
                            json = content.ReadAsStringAsync().Result;
                            
                            var responseApna = JsonConvert.DeserializeObject<ApnaUser>(json);
                            if (!responseApna.IsMember)
                            {
                                result.Success = false;
                            }
                        }

            }
            catch (Exception ex)
            {
                Logger.Log(ex);
                result.Success = false;
            }

            return result;
        }

        private async Task<Result> GetApnaUserData(string token, string username)
        {
            var result = new Result();
            try
            {
                var url = string.Concat(ConfigurationManager.AppSettings["apna.url"], "/api/Member/GetMemberDetailsByUsername", "?username=", username);

                HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Get, url);

                requestMessage.Headers.Add("Authorization", string.Concat("Bearer ", token));


                string json = null;

                using (var responseGetToken = await HttpClientManager.Client.SendAsync(requestMessage))
                    if (responseGetToken.StatusCode == HttpStatusCode.OK)
                        using (HttpContent content = responseGetToken.Content)
                        {
                            json = content.ReadAsStringAsync().Result;

                            var responseApna = JsonConvert.DeserializeObject<ApnaUser>(json);
                            result.Entity = responseApna;                            
                        }

            }
            catch (Exception ex)
            {
                Logger.Log(ex);
                result.Success = false;
            }

            return result;
        }
    }
}
