﻿/**
 * 
 * <Author> Nguyen Pham - 27348032  </Author>
 * <copyright> The following code is the work of Nguyen Pham unless other wise specified  </copyright>
 * <remarks> This is a part of the FIT4002 project. Product owner is APNA. Project supervisor is Robyn McNamara  </remarks>
 * <date>  </date>
 * <summary> </summary>
 */

using MyNursingFuture.BL.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Net;
using MyNursingFuture.BL.Entities;
using JWT;
using JWT.Algorithms;
using JWT.Serializers;
namespace MyNursingFuture.Api.Filters
{
    public class EmployerJWTAuthorized : AuthorizationFilterAttribute
    {
        public ICredentialsManager _credentialsManager
        {
            get
            {
                return GlobalConfiguration.Configuration.DependencyResolver.GetService(typeof(ICredentialsManager)) as ICredentialsManager;
            }
        }
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            var token = actionContext.Request.Headers.Authorization;
            if (token == null)
            {
                Challenge(actionContext);
                return;
            }
            var resultToken = _credentialsManager.ValidateEmployerToken(token.Parameter);
            if (!resultToken.Success)
            {
                Challenge(actionContext);
                return;
            }

            var employer = resultToken.Entity as EmployerEntity;
            employer.Token = token.Parameter;
            actionContext.Request.Properties.Add("employer", employer);
            base.OnAuthorization(actionContext);
        }
        private void Challenge(HttpActionContext actionContext)
        {
            actionContext.Response = new HttpResponseMessage(HttpStatusCode.Unauthorized); ;
        }
    }
}