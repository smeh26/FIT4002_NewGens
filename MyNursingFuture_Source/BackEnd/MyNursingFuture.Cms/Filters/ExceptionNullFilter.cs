using MyNursingFuture.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyNursingFuture.Cms.Filters
{
    public class ExceptionNullFilter : FilterAttribute, IExceptionFilter
    {
        public void OnException(ExceptionContext filterContext)
        {
            if (!filterContext.ExceptionHandled)
            {
                filterContext.Controller.TempData.Add("ErrorMessage", filterContext.Exception.Message);
                filterContext.ExceptionHandled = true;
                filterContext.Exception = new Exception("");
                filterContext.Result = new RedirectResult("~/Home/Index");
                Logger.Log(filterContext.Exception);            
            }
        }
    }
}