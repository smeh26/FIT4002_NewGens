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
    [ExceptionFilter]
    public class JobListingController : ApiController
    {
        private readonly IUsersManager _usersManager;
        private readonly IEmployersManager _employersManager;
        private readonly ICacheManager _cacheManager;
        private readonly IJobListingManager _jobListingManager;
        private readonly IJobListingCriteriaManager _jobListingCriteriaManager;
        public JobListingController(IUsersManager usersManager,
         IEmployersManager employersManager,
         ICacheManager cacheManager,
         IJobListingManager jobListingManager,
         IJobListingCriteriaManager jobListingCriteriaManager)
        {
            _usersManager = usersManager;
            _employersManager = employersManager;
            _cacheManager = cacheManager;
            _jobListingCriteriaManager = jobListingCriteriaManager;
            _jobListingManager = jobListingManager;

        }
        [JwtAuthorized]
        [Route("api/v1/JobListings")]
        public HttpResponseMessage Post([FromBody] JobListingEntity jobListing)
        {
            Result result = null;
            object objemployer = null;
            Request.Properties.TryGetValue("employer", out objemployer);
            var employer = objemployer as EmployerEntity;
            jobListing.CreateDate = DateTime.Now;
            jobListing.ModificationDate = jobListing.CreateDate;
            result = _jobListingManager.CreateJobListing(jobListing, employer);

            // return failed 
            if (!result.Success)
                return Request.CreateResponse(HttpStatusCode.OK, result);

            // if success

            //Result cri_result = null;
            var listingEntity = (JobListingEntity)result.Entity;
            jobListing.JobListingId = listingEntity.JobListingId;
            jobListing.ModificationDate = listingEntity.ModificationDate;
            jobListing.CreateDate = listingEntity.CreateDate;
            result.Entity = jobListing;
            return Request.CreateResponse(HttpStatusCode.OK, result);

        }

        [JwtAuthorized]
        [Route("api/v1/JobListings/Criteria")]
        public HttpResponseMessage Post([FromBody] List<JobListingCriteriaEntity> jobListingCriteria)
        {
            Result result = null;
            if (jobListingCriteria.Count == 0)
            {
                result = new Result(false);
                return Request.CreateResponse(HttpStatusCode.BadRequest, result);
            }
            result = _jobListingCriteriaManager.InsertCriteria(jobListingCriteria);

            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [JwtAuthorized]
        [Route("api/v1/JobListings/{id}")]
        public HttpResponseMessage GetListingById(int id)
        {
            Result result = null;

            result = _jobListingManager.GetListingById(id);
            return Request.CreateResponse(HttpStatusCode.OK, result);

        }

        [JwtAuthorized]
        [Route("api/v1/JobListings/PotentialApplicants/{id}")]
        public HttpResponseMessage GetPotentialApplicantsByCriteria(int id)
        {
            Result result = null;

            result = _jobListingManager.GetPotentialApplicantsByListingId(id);
            return Request.CreateResponse(HttpStatusCode.OK, result);

        }

        [JwtAuthorized]
        [Route("api/v1/JobListings/PotentialApplicants/")]
        public HttpResponseMessage GetPotentialApplicantsByCriteria([FromBody] List<JobListingCriteriaEntity> jobListingCriteria)
        {
            Result result = null;

            result = _jobListingManager.GetPotentialApplicantsByCriteria(jobListingCriteria);
            return Request.CreateResponse(HttpStatusCode.OK, result);

        }

    }   
}
