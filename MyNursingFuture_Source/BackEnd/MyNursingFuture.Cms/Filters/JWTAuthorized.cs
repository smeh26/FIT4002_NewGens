using System.Web;
using System.Web.Mvc;
using MyNursingFuture.BL.Entities;
using MyNursingFuture.BL.Managers;

namespace MyNursingFuture.Cms.Filters
{
    public class JwtAuthorized : AuthorizeAttribute, IAuthorizationFilter
    {
        public ICredentialsManager _credentialsManager {
            get
            {
                return DependencyResolver.Current.GetService<ICredentialsManager>();
            }
        }
        
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext.ActionDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true)
                || filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true))
            {
                // Don't check for authorization as AllowAnonymous filter is applied to the action or controller
                return;
            }

            // Check for authorization
            if (HttpContext.Current.Session["AdminUser"] == null)
            {
                var cookie = HttpContext.Current.Request.Cookies["MNFCMS"];
                if (cookie == null)
                {
                    filterContext.Result = new RedirectResult("~/Login/Index");
                    return;
                }
                var token = cookie.Value;
                var result = _credentialsManager.ValidateAdminToken(token);
                if (!result.Success)
                {
                    filterContext.Result = new RedirectResult("~/Login/Index");
                    return;
                }
                var user = (AdministratorEntity) result.Entity;
                HttpContext.Current.Session["AdminUser"] = result.Entity;
                user.Token = token;
            }
            else
            {
                var user = (AdministratorEntity) HttpContext.Current.Session["AdminUser"];
                var result = _credentialsManager.ValidateAdminToken(user.Token);
                if (result.Success)
                    return;
                filterContext.Result = new RedirectResult("~/Login/Index");
            }
        }
    }
}