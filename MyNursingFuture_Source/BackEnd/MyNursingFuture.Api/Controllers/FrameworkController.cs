using MyNursingFuture.Api.Filters;
using MyNursingFuture.Api.Models;
using MyNursingFuture.BL.Managers;
using MyNursingFuture.Util;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Hosting;
using System.Web.Http;
using System.Web.Http.Cors;

namespace MyNursingFuture.Api.Controllers
{
    [ExceptionFilter]
    public class FrameworkController : ApiController
    {
        private readonly IFrameworkManager _frameworkManager;
        private readonly ICacheManager _cacheManager;
        public FrameworkController(IFrameworkManager frameworkManager, ICacheManager cacheManager)
        {
            _frameworkManager = frameworkManager;
            _cacheManager = cacheManager;
        }

        // GET: api/Framework
        public HttpResponseMessage Get()
        {
            var data = _cacheManager.Get(CacheTypes.Framework);
            if(data == null)
            {
                data = _frameworkManager.Get();
                var result = data as Result;
                if (result.Success)
                {
                    _cacheManager.Add(CacheTypes.Framework, data);
                }
            }
            return Request.CreateResponse(HttpStatusCode.OK, data);
        }

        [Route("api/Framework/nocache")]
        public HttpResponseMessage GetNoCache()
        {
            var data = _frameworkManager.Get();
            return Request.CreateResponse(HttpStatusCode.OK, data);
        }

        [Route("api/Framework/contact")]
        public HttpResponseMessage PostContact([FromBody]ContactModel value)
        {
            var result = _frameworkManager.ContactMessage(value);

            return Request.CreateResponse(HttpStatusCode.OK, result);
        }


    }
}
