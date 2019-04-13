using MyNursingFuture.Api.Filters;
using MyNursingFuture.Api.Models;
using MyNursingFuture.BL.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Hosting;
using System.Web.Http;
using System.Web.Http.Cors;

namespace MyNursingFuture.Api.Controllers
{
    [ExceptionFilter]
    public class ArticlesController : ApiController
    {
        private readonly IFrameworkManager _frameworkManager;
        private readonly ICacheManager _cacheManager;
        public ArticlesController(IFrameworkManager frameworkManager, ICacheManager cacheManager)
        {
            _frameworkManager = frameworkManager;
            _cacheManager = cacheManager;
        }

        // GET: api/Articles
        public HttpResponseMessage Get()
        {
            var data = _frameworkManager.GetArticles();
            return Request.CreateResponse(HttpStatusCode.OK, data);
        }

        // GET: api/Articles
        public HttpResponseMessage Get(int id)
        {
            var data = _frameworkManager.GetArticles(id);
            return Request.CreateResponse(HttpStatusCode.OK, data);
        }
        [Route("api/Articles/feedback")]
        public HttpResponseMessage Feedback([FromBody]ArticleFeedback value)
        {
            var result = _frameworkManager.FeedbackArticle(value);            
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }
    }
}
