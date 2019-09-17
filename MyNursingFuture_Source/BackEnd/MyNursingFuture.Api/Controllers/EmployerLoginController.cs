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
using System.Threading.Tasks;

namespace MyNursingFuture.Api.Controllers
{
    public class EmployerLoginController : ApiController
    {
        private readonly IEmployersManager _employersManager;
        private readonly ICacheManager _cacheManager;
        private readonly IAppConfigurationsManager _configManager;
        public EmployerLoginController(IEmployersManager employerManager, IAppConfigurationsManager configManager, ICacheManager cacheManager)
        {
            _employersManager = employerManager;
            _configManager = configManager;
            _cacheManager = cacheManager;

        }
        // POST: api/Login
        public async Task<HttpResponseMessage> Post([FromBody]EmployerEntity value)
        {
            Result result = null;
            var tokenLogin = false;
            var apnaLogin = false;

            var token = Request.Headers.Authorization;
            if (token != null)
            {
                tokenLogin = true;
                result = _employersManager.Login(token.Parameter);
            }

            if ((string.IsNullOrEmpty(value.Email) || string.IsNullOrEmpty(value.Password)) && !tokenLogin)
            {
                result = new Result(false);
                return Request.CreateResponse(HttpStatusCode.BadRequest, result);
            }

            if (!tokenLogin && !apnaLogin)
            {
                result = _employersManager.Login(value);
                apnaLogin = !result.Success;
            }

/*            Result apnaUserResult = null;
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

                            result = _employersManager.LoginApna(userEntityApna);
                        }
                    }
                }
            }*/

            if (!result.Success)
                return Request.CreateResponse(HttpStatusCode.OK, result);

            var employer = new EmployerModel();
            var employerEntity = (EmployerEntity)result.Entity;
            employer.Email = employerEntity.Email;
            employer.Token = employerEntity.Token;
            employer.EmployerName = employerEntity.EmployerName;
//            employer.ApnaUser = apnaLogin;
            employer.EmployerID = employerEntity.EmployerID;

            employer.Area = employerEntity.Area;
            employer.State = employerEntity.State;
            employer.Country = employerEntity.Country;
            employer.Suburb = employerEntity.Suburb;
            employer.PostalCode = employerEntity.PostalCode;
            employer.Setting = employerEntity.Setting;
            result.Entity = employer;
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }
    }
}
