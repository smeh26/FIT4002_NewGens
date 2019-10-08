using System;
using System.Collections.Generic;
using System.Linq;
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
using System.Web;
using System.Web.Hosting;
using Newtonsoft.Json;
using System.Configuration;

namespace MyNursingFuture.Api.Controllers
{
    public class JobApplicationController : ApiController
    {
        private readonly IUsersManager _usersManager;
        private readonly IEmployersManager _employersManager;
        private readonly ICacheManager _cacheManager;
        private readonly IJobListingManager _jobListingManager;
        private readonly IJobListingCriteriaManager _jobListingCriteriaManager;
        private readonly IJobApplicationManager _jobApplicationManager;
        public JobApplicationController(IUsersManager usersManager,
         IEmployersManager employersManager,
         ICacheManager cacheManager,
         IJobListingManager jobListingManager,
         IJobListingCriteriaManager jobListingCriteriaManager,
         IJobApplicationManager jobApplicationManager
         )
        {
            _usersManager = usersManager;
            _employersManager = employersManager;
            _cacheManager = cacheManager;
            _jobListingCriteriaManager = jobListingCriteriaManager;
            _jobListingManager = jobListingManager;
            _jobApplicationManager = jobApplicationManager;

        }

        [JwtAuthorized]
        public HttpResponseMessage Post([FromBody] JobApplicationEntity jobApplication)
        {
            Result result = new Result();
            jobApplication.AppliedDate = DateTime.Now;
            jobApplication.LastModifiedDate = jobApplication.AppliedDate;
            result = _jobApplicationManager.CreateJobApplication(jobApplication);

            // return failed 
            if (!result.Success)
                return Request.CreateResponse(HttpStatusCode.BadRequest, result);

            // if success
            return Request.CreateResponse(HttpStatusCode.Created, result);



        }

        [JwtAuthorized]
        [Route("api/v1/Applications/Users/{id}")]
        public HttpResponseMessage GetApplicationByUserId(int id)
        {
            Result result = new Result();

            result = _jobApplicationManager.GetJobApplicationByUserId(id);

            //If failed
            if (!result.Success)
                return Request.CreateResponse(HttpStatusCode.BadRequest, result);
            //If if not found
            if (result.Entity == null)
                return Request.CreateResponse(HttpStatusCode.NotFound, result);

            //If it is good
            return Request.CreateResponse(HttpStatusCode.OK, result);


        }

        [JwtAuthorized]
        [Route("api/v1/Applications/Listing/{id}")]
        public HttpResponseMessage GetApplicationByListingId(int id)
        {
            Result result = new Result();

            result = _jobApplicationManager.GetJobApplicationByListingId(id);

            //If failed
            if (!result.Success)
                return Request.CreateResponse(HttpStatusCode.BadRequest, result);
            //If if not found
            if (result.Entity == null)
                return Request.CreateResponse(HttpStatusCode.NotFound, result);

            //If it is good
            return Request.CreateResponse(HttpStatusCode.OK, result);

        }

        [JwtAuthorized]
        [Route("api/v1/Applications/{id}")]
        public HttpResponseMessage GetApplicationById(int id)
        {
            Result result = new Result();

            result = _jobApplicationManager.GetJobApplicationByApplicationId(id);

            //If failed
            if (!result.Success)
                return Request.CreateResponse(HttpStatusCode.BadRequest, result);
            //If if not found
            if (result.Entity == null)
                return Request.CreateResponse(HttpStatusCode.NotFound, result);

            //If it is good
            return Request.CreateResponse(HttpStatusCode.OK, result);

        }


    }
}
