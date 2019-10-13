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
    public class GenericJWTAuthorized : AuthorizationFilterAttribute
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

            var result_payload = _credentialsManager.GetPayLoad(token.Parameter);
            var payload = (Dictionary<string, Object>)result_payload.Entity;
            Object roles;
            if (payload.TryGetValue("roles" , out roles))
                {
                var roles_l = roles as List<String> ;
                if (roles_l.Contains("employer"))
                    {

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

                } else if (roles_l.Contains("nurse")){

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


            }


           
        }
        private void Challenge(HttpActionContext actionContext)
        {
            actionContext.Response = new HttpResponseMessage(HttpStatusCode.Unauthorized); ;
        }
    }
}