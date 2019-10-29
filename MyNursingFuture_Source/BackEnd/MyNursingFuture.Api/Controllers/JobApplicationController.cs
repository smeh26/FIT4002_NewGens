/**
 * 
 * <Author> Nguyen Pham - 27348032  </Author>
 * <copyright> The following code is the work of Nguyen Pham unless other wise specified  </copyright>
 * <remarks> This is a part of the FIT4002 project. Product owner is APNA. Project supervisor is Robyn McNamara  </remarks>
 * <date>  </date>
 * <summary> </summary>
 */
using MyNursingFuture.Api.Filters;
using MyNursingFuture.Api.Models;
using MyNursingFuture.BL.Entities;
using MyNursingFuture.BL.Managers;
using MyNursingFuture.Util;
using Swashbuckle.Swagger.Annotations;
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;



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

        //==============================================================================
        public enum ApplicationStatus
        {
            Draft,
            Submitted,
            Withdrawn,
            Shortlisted,
            Declined

        }







        //===============================================================================
        private struct JobApplicationResponse
        {
            public string Message { get; set; }
            public bool Success { get; set; }
            public ApplicationWithInfo Entity { get; set; }
        }

        private struct ApplicationWithInfo
        {
            public JobApplicationEntity jobApplicationEntity { get; set; }
            public EmployerModelSecured employerInfo { get; set; }
            public UserModelSecured nurseInfo { get; set; }

        }

        /// <summary>
        /// [Nurse] API For creating Job Application
        /// </summary>
        /// <remarks> Functionally working  
        /// 
        /// TODO - Unit tests 
        /// </remarks>
        /// <response code="200"></response>
        /// <response code="400"></response>
        /// <response code="500"></response>
        [HttpPost]
        [JwtAuthorized]
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(JobApplicationResponse))]
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

            // check if listing is valid
            var jobListingEntity_Result = _jobListingManager.GetListingById(jobApplication.JobListingId);
            if (!jobListingEntity_Result.Success || jobListingEntity_Result.Entity == null)
            {
                result.Entity = null;
                result.Message = "Job Listing not exists";
                result.Success = false;
                return Request.CreateResponse(HttpStatusCode.BadRequest, result);
            }
            var jobListingEntity = (JobListingEntity)jobListingEntity_Result.Entity;

            // check if listing expired
            if (DateTime.Compare(jobListingEntity.ApplicationDeadline, DateTime.Now) < 0)
            {
                result.Entity = null;
                result.Message = "Job Listing expired";
                result.Success = false;
                return Request.CreateResponse(HttpStatusCode.BadRequest, result);
            }

            if (jobApplication.IsDraft)
            {
                jobApplication.ApplicationStatus = ApplicationStatus.Draft.ToString();
            }
            else
            {
                jobApplication.ApplicationStatus = ApplicationStatus.Submitted.ToString();
            }

            jobApplication.EmployerId = jobListingEntity.EmployerId;
            jobApplication.UserId = user.UserId;
            jobApplication.AppliedDate = DateTime.Now;
            jobApplication.LastModifiedDate = jobApplication.AppliedDate;
            jobApplication.IsShortlisted = false;
            jobApplication.IsDeclined = false;
            result = _jobApplicationManager.CreateJobApplication(jobApplication);





            // return failed 
            if (!result.Success)
                return Request.CreateResponse(HttpStatusCode.BadRequest, result);

            // if success
            var response = new ApplicationWithInfo();
            response.jobApplicationEntity = (JobApplicationEntity)result.Entity;
            result.Entity = response;
            return Request.CreateResponse(HttpStatusCode.Created, result);

        }








        //===============================================================================
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








        //===============================================================================
        /// <summary>
        /// [Employer] - API For shortlisting a Job Application
        /// </summary>
        /// <remarks> UNTESTED  </remarks>
        /// <response code="200"></response>
        /// <response code="400"></response>
        /// <response code="500"></response>
        [EmployerJWTAuthorized]
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








        //===============================================================================
        /// <summary>
        /// API For retrieving all job applications by Listing Id
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








        //===============================================================================
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

            var application = (JobApplicationEntity)result.Entity;


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
