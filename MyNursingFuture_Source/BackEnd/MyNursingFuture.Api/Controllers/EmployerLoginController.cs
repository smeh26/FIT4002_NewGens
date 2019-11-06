/**
 * 
 * <Author> Nguyen Pham - 27348032  </Author>
 * <copyright> The following code is the work of Nguyen Pham unless other wise specified  </copyright>
 * <remarks> This is a part of the FIT4002 project. Product owner is APNA. Project supervisor is Robyn McNamara  </remarks>
 * <date>  </date>
 * <summary> </summary>
 */
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
using Swashbuckle.Swagger.Annotations;

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

        private struct EmployerLoginReturnType{
           
            public string Message { get; set; }
            public bool Success { get; set; }
            public EmployerModel Entity { get; set; }

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
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(EmployerLoginReturnType))]
        [Route("api/v1/EmployerLogin")]
        public async Task<HttpResponseMessage> Post([FromBody] LoginObject value)
        {
            var result = new Result();
            var tokenLogin = false;
            var apnaLogin = false;


            var employer = new EmployerEntity();

            PropertyCopier<LoginObject, EmployerEntity>.Copy(value, employer);

            var token = Request.Headers.Authorization;
            if (token != null)
            {
                tokenLogin = true;
                result = _employersManager.Login(token.Parameter);
            }

            if ((string.IsNullOrEmpty(employer.Email) || string.IsNullOrEmpty(employer.Password)) && !tokenLogin)
            {
                result = new Result(false);
                return Request.CreateResponse(HttpStatusCode.BadRequest, result);
            }

            if (!tokenLogin && !apnaLogin)
            {
                result = _employersManager.Login(employer);
                apnaLogin = !result.Success;
            }


            var employer_model = new EmployerModel();
            var employerEntity = (EmployerEntity)result.Entity;

            if (!result.Success)
                return Request.CreateResponse(HttpStatusCode.BadRequest, result);
            employer_model.Token = employerEntity.Token;
            PropertyCopier<EmployerEntity, EmployerModel>.Copy(employerEntity, employer_model);

            if (token != null)
            {
                employer_model.Token = token.Parameter;
            }


            result.Entity = employer_model;
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }
    }
}
