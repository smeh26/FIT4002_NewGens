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

namespace MyNursingFuture.Api.Filters
{
    public class JwtAuthorized: AuthorizationFilterAttribute
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
            var token =  actionContext.Request.Headers.Authorization;



            if(token == null)
            {
                Challenge(actionContext);
                return;
            }

            var resultToken = _credentialsManager.ValidateUserToken(token.Parameter);
            if (!resultToken.Success)
            {
                Challenge(actionContext);
                return;
            }
            var user = resultToken.Entity as UserEntity;
            user.Token = token.Parameter;
            actionContext.Request.Properties.Add("user", user);
            base.OnAuthorization(actionContext);

        }
        private void Challenge(HttpActionContext actionContext)
        {
            actionContext.Response = new HttpResponseMessage(HttpStatusCode.Unauthorized); ;
        }
    }
}