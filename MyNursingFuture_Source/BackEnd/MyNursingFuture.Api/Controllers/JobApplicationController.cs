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


        /// <summary>
        /// API For creating Job Application
        /// </summary>
        /// <remarks> UNTESTED  </remarks>
        /// <response code="200"></response>
        /// <response code="400"></response>
        /// <response code="500"></response>
        [HttpPost]
        [JwtAuthorized]
        [Route("api/v1/Applications")]
        public HttpResponseMessage Post([FromBody] JobApplicationEntity jobApplication)
        {
            Result result = new Result();

            // Authentication verify block 
            object objuser = null;
            Request.Properties.TryGetValue("user", out objuser);
            if (objuser == null)
                return Request.CreateResponse(HttpStatusCode.Unauthorized, result);
            var user = objuser as UserEntity;

            jobApplication.UserId = user.UserId;
            jobApplication.AppliedDate = DateTime.Now;
            jobApplication.LastModifiedDate = jobApplication.AppliedDate;
            jobApplication.IsShortlisted = false;
            jobApplication.Name = user.Name;
            jobApplication.Qualification = user.Qualification;
            jobApplication.EmailAddress = user.Email;
            jobApplication.Country = user.Country;
            jobApplication.PostalCode = user.PostalCode;
            jobApplication.Suburb = user.Suburb;
            result = _jobApplicationManager.CreateJobApplication(jobApplication);


            // return failed 
            if (!result.Success)
                return Request.CreateResponse(HttpStatusCode.BadRequest, result);

            // if success
            return Request.CreateResponse(HttpStatusCode.Created, result);

        }

        /// <summary>
        /// API For shortlisting a Job Application
        /// </summary>
        /// <remarks> UNTESTED  </remarks>
        /// <response code="200"></response>
        /// <response code="400"></response>
        /// <response code="500"></response>
        [HttpPost]
        [EmployerJWTAuthorized]
        [Route("api/v1/Applications/Shorlist/{applicationId}")]
        public HttpResponseMessage ShortListApplicationByApplicationId(int applicationId)
        {
            Result result = new Result();
            //Employer vertification 
            object objemployer = null;
            Request.Properties.TryGetValue("employer", out objemployer);
            if (objemployer == null)
                return Request.CreateResponse(HttpStatusCode.Unauthorized, new Result(false));
            var employer = objemployer as EmployerEntity;
            var jobApplication = (JobApplicationEntity)_jobApplicationManager.GetJobApplicationByApplicationId(applicationId).Entity;

            // verify if the employer owns the listing
            var listing = (JobListingEntity)_jobListingManager.GetListingById(jobApplication.JobListingId).Entity;
            if (employer.EmployerId != listing.EmployerId)
                return Request.CreateResponse(HttpStatusCode.Unauthorized, new Result(false));

            jobApplication.IsShortlisted = true;
            jobApplication.ShortListedDate = DateTime.Now;

            result = _jobApplicationManager.UpdateJobApplication(jobApplication);


            // return failed 
            if (!result.Success)
                return Request.CreateResponse(HttpStatusCode.BadRequest, result);

            // if success
            return Request.CreateResponse(HttpStatusCode.Created, result);

        }

        /// <summary>
        /// API For shortlisting a Job Application
        /// </summary>
        /// <remarks> UNTESTED  </remarks>
        /// <response code="200"></response>
        /// <response code="400"></response>
        /// <response code="500"></response>
        [JwtAuthorized]
        [Route("api/v1/Applications/Users/{id}")]
        public HttpResponseMessage GetApplicationByUserId(int id)
        {
            Result result = new Result();

            // Authentication verify block 
            object objuser = null;
            Request.Properties.TryGetValue("user", out objuser);
            if (objuser == null)
                return Request.CreateResponse(HttpStatusCode.Unauthorized, new Result(false));
            var user = objuser as UserEntity;


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

        /// <summary>
        /// API For retrieving a Job Application by Listing Id
        /// </summary>
        /// <remarks> UNTESTED  </remarks>
        /// <response code="200"></response>
        /// <response code="400"></response>
        /// <response code="500"></response>
        [EmployerJWTAuthorized]
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

        /// <summary>
        /// API For retrieving a Job Application by its Id
        /// </summary>
        /// <remarks> UNTESTED  </remarks>
        /// <response code="200"></response>
        /// <response code="400"></response>
        /// <response code="500"></response>
        [HttpGet]
        [GenericJWTAuthorized]
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

            // Authentication verify block 
            object objuser = null;
            Request.Properties.TryGetValue("user", out objuser);

            var user = objuser as UserEntity;

            object objemployer = null;
            Request.Properties.TryGetValue("employer", out objemployer);

            var employer = objemployer as EmployerEntity;

            var application = (JobApplicationEntity) result.Entity;


            if (user != null)
            {
                // User logged in 
                if (user.UserId != application.UserId)
                    return Request.CreateResponse(HttpStatusCode.Unauthorized, result);

            }
            else if (employer != null)
            {
                var listing = (JobListingEntity)_jobListingManager.GetListingById(application.JobListingId).Entity;


                if (employer.EmployerId != listing.EmployerId)
                    return Request.CreateResponse(HttpStatusCode.Unauthorized, new Result(false));
            }


            //If it is good
            return Request.CreateResponse(HttpStatusCode.OK, result);

        }


    }
}
