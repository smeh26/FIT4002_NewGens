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
using AutoMapper;
using System.Collections.Generic;

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
        private readonly INurseSelfAssessmentAnswersManager _nurseSelfAssessmentAnswersManager;

        public JobApplicationController(IUsersManager usersManager,
         IEmployersManager employersManager,
         ICacheManager cacheManager,
         IJobListingManager jobListingManager,
         IJobListingCriteriaManager jobListingCriteriaManager,
         IJobApplicationManager jobApplicationManager,
         INurseSelfAssessmentAnswersManager nurseSelfAssessmentAnswersManager
         )
        {
            _usersManager = usersManager;
            _employersManager = employersManager;
            _cacheManager = cacheManager;
            _jobListingCriteriaManager = jobListingCriteriaManager;
            _jobListingManager = jobListingManager;
            _jobApplicationManager = jobApplicationManager;
            _nurseSelfAssessmentAnswersManager = nurseSelfAssessmentAnswersManager;
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
        private struct JobApplicationList_NurseResponse
        {
            public string Message { get; set; }
            public bool Success { get; set; }
            public List<ApplicationWithInfo> Entity { get; set; }
        }
        private struct JobApplicationList_EmployerResponse
        {
            public string Message { get; set; }
            public bool Success { get; set; }
            public List<ApplicationWithInfo> Entity { get; set; }
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

            var nurse = (UserEntity)_usersManager.GetSecuredUserDetails(user.UserId).Entity;


            jobApplication.EmployerId = jobListingEntity.EmployerId;
            jobApplication.ExpectedSalary = nurse.salary;
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
        /// <remarks>  Give a standard of 60 days to contact the nurse
        /// If different date is required, use a different API
        /// TODO - Unit tests  </remarks>
        /// <response code="200"></response>
        /// <response code="400"></response>
        /// <response code="500"></response>
        [HttpPost]
        [EmployerJWTAuthorized]
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(JobApplicationResponse))]
        [Route("api/v1/Applications/Shortlist/{applicationId}")]
        public HttpResponseMessage ShortListApplicationByApplicationId(int applicationId)
        {
            Result result = new Result();
            //Employer verification 
            object objemployer = null;
            Request.Properties.TryGetValue("employer", out objemployer);
            if (objemployer == null)
                return Request.CreateResponse(HttpStatusCode.Unauthorized, new Result(false));
            var employer = objemployer as EmployerEntity;
            var jobApplication = (JobApplicationEntity)_jobApplicationManager.GetJobApplicationByApplicationId(applicationId).Entity;

            // verify if the employer owns the listing
            var listing = (JobListingEntity)_jobListingManager.GetListingById(jobApplication.JobListingId).Entity;
            if (employer.EmployerId != listing.EmployerId || jobApplication.IsDraft)
                return Request.CreateResponse(HttpStatusCode.Unauthorized, new Result(false));
            jobApplication.ApplicationStatus = ApplicationStatus.Shortlisted.ToString();
            jobApplication.IsShortlisted = true;
            jobApplication.ShortListedDate = DateTime.Now;
            jobApplication.IsDeclined = false;
            jobApplication.DeclinedDate = null;
            jobApplication.MakeContactDeadline = DateTime.Now.AddDays(60);

            result = _jobApplicationManager.ShortlistOrDeclineJobApplication(jobApplication);


            // return failed 
            if (!result.Success)
                return Request.CreateResponse(HttpStatusCode.BadRequest, result);

            // if success
            return Request.CreateResponse(HttpStatusCode.OK, result);

        }

        public struct ShortList_Application
        {
            public int ApplicationId { set; get; }
            public int NumberOfDaysEmployerWillMakeContact { set; get; }

        }

        //===============================================================================
        /// <summary>
        /// API For shortlisting a Job Application with Specified number of dates 
        /// </summary>
        /// <remarks>  This API give a different way to shortlist an application and specify a set number of date that the employer will make contact with the   
        /// TODO - Unit tests  </remarks>
        /// <response code="200">Application is Shortlisted</response>
        /// <response code="400"> Bad request, the API  failed to fetch the record</response>
        /// <response code="500">Server Error</response>
        [HttpPost]
        [EmployerJWTAuthorized]
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(JobApplicationResponse))]
        [Route("api/v1/Applications/Shortlist/")]
        public HttpResponseMessage ShortListApplication([FromBody] ShortList_Application application)
        {
            int applicationId = application.ApplicationId;
            Result result = new Result();
            //Employer verification 
            object objemployer = null;
            Request.Properties.TryGetValue("employer", out objemployer);
            if (objemployer == null)
                return Request.CreateResponse(HttpStatusCode.Unauthorized, new Result(false));
            var employer = objemployer as EmployerEntity;
            var jobApplication = (JobApplicationEntity)_jobApplicationManager.GetJobApplicationByApplicationId(applicationId).Entity;

            // verify if the employer owns the listing
            var listing = (JobListingEntity)_jobListingManager.GetListingById(jobApplication.JobListingId).Entity;
            if (employer.EmployerId != listing.EmployerId || jobApplication.IsDraft)
                return Request.CreateResponse(HttpStatusCode.Unauthorized, new Result(false));
            jobApplication.ApplicationStatus = ApplicationStatus.Shortlisted.ToString();
            jobApplication.IsShortlisted = true;
            jobApplication.ShortListedDate = DateTime.Now;
            jobApplication.IsDeclined = false;
            jobApplication.DeclinedDate = null;
            jobApplication.MakeContactDeadline = DateTime.Now.AddDays(application.NumberOfDaysEmployerWillMakeContact);

            result = _jobApplicationManager.ShortlistOrDeclineJobApplication(jobApplication);


            // return failed 
            if (!result.Success)
                return Request.CreateResponse(HttpStatusCode.BadRequest, result);

            // if success
            return Request.CreateResponse(HttpStatusCode.OK, result);

        }





        //=====================================================================================================
        /// <summary>
        /// API For Employer to Decline a Job Application
        /// </summary>
        /// <remarks> Functional
        /// 
        /// TODO - Unit tests   
        /// </remarks>
        /// <response code="200"></response>
        /// <response code="400"></response>
        /// <response code="500"></response>
        [HttpPost]
        [EmployerJWTAuthorized]
        [Route("api/v1/Applications/Decline/{applicationId}")]
        public HttpResponseMessage DeclineApplicationByApplicationId(int applicationId)
        {
            Result result = new Result();
            //Employer verification 
            object objemployer = null;
            Request.Properties.TryGetValue("employer", out objemployer);
            if (objemployer == null)
                return Request.CreateResponse(HttpStatusCode.Unauthorized, new Result(false));
            var employer = objemployer as EmployerEntity;
            var jobApplication = (JobApplicationEntity)_jobApplicationManager.GetJobApplicationByApplicationId(applicationId).Entity;

            // verify if the employer owns the listing
            var listing = (JobListingEntity)_jobListingManager.GetListingById(jobApplication.JobListingId).Entity;
            if (employer.EmployerId != listing.EmployerId || jobApplication.IsDraft)
                return Request.CreateResponse(HttpStatusCode.Unauthorized, new Result(false));
            
            //Update  info 
            jobApplication.ApplicationStatus = ApplicationStatus.Declined.ToString();
            jobApplication.IsShortlisted = true;
            jobApplication.ShortListedDate = null;
            jobApplication.IsDeclined = false;
            jobApplication.DeclinedDate = DateTime.Now;
            jobApplication.MakeContactDeadline = null;

            result = _jobApplicationManager.ShortlistOrDeclineJobApplication(jobApplication);


            // return failed 
            if (!result.Success)
                return Request.CreateResponse(HttpStatusCode.BadRequest, result);

            // if success
            return Request.CreateResponse(HttpStatusCode.OK, result);

        }










        //===============================================================================
        /// <summary>
        /// [Nurse] - API For getting all Job Applications from a nurse, by userId
        /// </summary>
        /// <remarks> Functional
        /// 
        /// TODO - Unit tests   </remarks>
        /// <response code="200"></response>
        /// <response code="400"></response>
        /// <response code="500"></response>
        [JwtAuthorized]
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(JobApplicationList_NurseResponse))]
        [Route("api/v1/Applications/Users/")]
        public HttpResponseMessage GetApplicationsByUser()
        {
            Result result = new Result();

            // Authentication verify block 
            object objuser = null;
            Request.Properties.TryGetValue("user", out objuser);
            if (objuser == null)
                return Request.CreateResponse(HttpStatusCode.Unauthorized, new Result(false));
            var user = objuser as UserEntity;


            result = _jobApplicationManager.GetJobApplicationByUserId(user.UserId);

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
        /// [Employers] API For retrieving all job applications by Listing Id
        /// </summary>
        /// <remarks> Functional
        /// 
        /// TODO - Unit tests   </remarks>
        /// <response code="200"></response>
        /// <response code="400"></response>
        /// <response code="500"></response>
        [EmployerJWTAuthorized]
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(JobApplicationList_EmployerResponse))]
        [Route("api/v1/Applications/Listing/{id}")]
        public HttpResponseMessage GetApplicationsByListingId(int id)
        {
            Result result = new Result();

            //Employer verification 
            object objemployer = null;
            Request.Properties.TryGetValue("employer", out objemployer);
            if (objemployer == null)
                return Request.CreateResponse(HttpStatusCode.Unauthorized, new Result(false));
            var employer = objemployer as EmployerEntity;
            var listing = (JobListingEntity) _jobListingManager.GetListingById(id).Entity;
            if (listing == null || listing.EmployerId != employer.EmployerId)
                return Request.CreateResponse(HttpStatusCode.Unauthorized, new Result(false));
            result = _jobApplicationManager.GetJobApplicationByListingId(id);

            // Get default quiz for each application
            var applications = (List<JobApplicationEntity>)result.Entity;

            foreach (JobApplicationEntity application in applications) {
                var nurse = (UserEntity)_usersManager.GetSecuredUserDetails(application.UserId).Entity;
                var quizzAnswers = (List<NurseSelfAssessmentAnswersEntity>)_nurseSelfAssessmentAnswersManager.GetAnswersbyUserQuizzId(nurse.defaultQuizId).Entity;
                application.PreferedQuizz = quizzAnswers;

            }

            result.Entity = applications;
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
        /// <remarks> Functional
        /// 
        /// TODO - Unit tests   </remarks>
        /// <response code="200"></response>
        /// <response code="400"></response>
        /// <response code="500"></response>
        [HttpGet]
        [GenericJWTAuthorized]
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(JobApplicationResponse))]
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

            var response = new ApplicationWithInfo();

            response.jobApplicationEntity = application;
            if (user != null)
            {
                // User logged in 
                if (user.UserId != application.UserId)
                    return Request.CreateResponse(HttpStatusCode.Unauthorized, result);

            }
            else if (employer != null)
            {
                var listing = (JobListingEntity)_jobListingManager.GetListingById(application.JobListingId).Entity;


                if (employer.EmployerId != listing.EmployerId || application.IsDraft)
                    return Request.CreateResponse(HttpStatusCode.Unauthorized, new Result(false));
            }

            

            if (!application.IsDraft && (application.IsShortlisted == true) && (application.IsDeclined != true))
            {
                // get details only if the application is shortlisted;
                var nurseEntity = (UserEntity)_usersManager.GetSecuredUserDetails(application.UserId).Entity;
                var nurseDetails = new UserModelSecured();
                PropertyCopier<UserEntity, UserModelSecured>.Copy(nurseEntity, nurseDetails);

                var employerEntity = (EmployerEntity)_employersManager.GetEmployerById(application.EmployerId).Entity;
                var employerDetails = new EmployerModelSecured();
                PropertyCopier<EmployerEntity, EmployerModelSecured>.Copy(employerEntity, employerDetails);

                var quizAnswers = (List<NurseSelfAssessmentAnswersEntity>)_nurseSelfAssessmentAnswersManager.GetAnswersbyUserQuizzId(nurseEntity.defaultQuizId).Entity;
                response.jobApplicationEntity.PreferedQuizz = quizAnswers;
                response.employerInfo = employerDetails;
                response.nurseInfo = nurseDetails;

            }

            //If it is good

            result.Entity = response;
            return Request.CreateResponse(HttpStatusCode.OK, result);

        }





        //===============================================================================
        /// <summary>
        /// [Nurse] - API For Nurse to leave feedback for Nurse on an Application
        /// </summary>
        /// <remarks> Functional
        /// 
        /// TODO - Unit tests   </remarks>
        /// <response code="200"></response>
        /// <response code="400"></response>
        /// <response code="500"></response>
        [JwtAuthorized]
        [HttpPost]
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(JobApplicationResponse))]
        [Route("api/v1/Applications/FeedbackFromNurse/")]
        public HttpResponseMessage UpdateFeedbackFromNurse([FromBody] JobApplicationEntity applicationEntity)
        {
            Result result = new Result();

            // Authentication verify block 
            object objuser = null;
            Request.Properties.TryGetValue("user", out objuser);
            if (objuser == null)
                return Request.CreateResponse(HttpStatusCode.Unauthorized, new Result(false));
            var user = objuser as UserEntity;

            if (applicationEntity.UserId != user.UserId) {
                return Request.CreateResponse(HttpStatusCode.Unauthorized, new Result(false));
            }
            if (String.IsNullOrEmpty(applicationEntity.FeedbackFromNurse) || String.IsNullOrWhiteSpace(applicationEntity.FeedbackFromNurse)) {
                result.Message = "Feedback cannot be empty";
                return Request.CreateResponse(HttpStatusCode.BadRequest, result);

            }

            result = _jobApplicationManager.UpdateFeedbackFromNurse(applicationEntity);

            //If failed
            if (!result.Success)
                return Request.CreateResponse(HttpStatusCode.BadRequest, result);
            //If it not found
            if (result.Entity == null)
                return Request.CreateResponse(HttpStatusCode.NotFound, result);

            //If it is good
            return Request.CreateResponse(HttpStatusCode.OK, result);


        }


        //===============================================================================
        /// <summary>
        /// [Employer] - API for Employer to leave feedback for Nurse on an Application
        /// </summary>
        /// <remarks> Functional
        /// 
        /// TODO - Unit tests   </remarks>
        /// <response code="200"></response>
        /// <response code="400"></response>
        /// <response code="500"></response>
        [HttpPost]
        [EmployerJWTAuthorized]
        [Route("api/v1/Applications/FeedbackFromEmployer/")]
        public HttpResponseMessage UpdateFeedbackFromEmployer([FromBody] JobApplicationEntity applicationEntity)
        {
            Result result = new Result();
            //Employer vertification 
            object objemployer = null;
            Request.Properties.TryGetValue("employer", out objemployer);
            var employer = objemployer as EmployerEntity;
            if (objemployer == null || employer.EmployerId != applicationEntity.EmployerId)
                return Request.CreateResponse(HttpStatusCode.Unauthorized, new Result(false));

            if (String.IsNullOrEmpty(applicationEntity.FeedbackFromEmployer) || String.IsNullOrWhiteSpace(applicationEntity.FeedbackFromEmployer))
            {
                result.Message = "Feedback cannot be empty";
                return Request.CreateResponse(HttpStatusCode.BadRequest, result);
            }

            result = _jobApplicationManager.UpdateFeedbackFromEmployer(applicationEntity);


            // return failed 
            if (!result.Success)
                return Request.CreateResponse(HttpStatusCode.BadRequest, result);

            // if success
            return Request.CreateResponse(HttpStatusCode.OK , result);

        }

        //===============================================================================
        /// <summary>
        /// [Nurse] - API For Nurse to Retract their Application
        /// </summary>
        /// <remarks> Functional
        /// 
        /// TODO - Unit tests   </remarks>
        /// <response code="200"></response>
        /// <response code="400"></response>
        /// <response code="500"></response>
        [JwtAuthorized]
        [HttpPost]
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(JobApplicationResponse))]
        [Route("api/v1/Applications/Retract/{id}")]
        public HttpResponseMessage RetractApplication(int id)
        {
            Result result = new Result();

            // Authentication verify block 
            object objuser = null;
            Request.Properties.TryGetValue("user", out objuser);
            if (objuser == null)
                return Request.CreateResponse(HttpStatusCode.Unauthorized, new Result(false));
            var user = objuser as UserEntity;

            var applicationEntity = (JobApplicationEntity)_jobApplicationManager.GetJobApplicationByApplicationId(id).Entity;

            if (applicationEntity.UserId != user.UserId)
            {
                return Request.CreateResponse(HttpStatusCode.Unauthorized, new Result(false));
            }

            applicationEntity.IsShortlisted = false;
            applicationEntity.ShortListedDate = null;
            applicationEntity.DeclinedDate = null;
            applicationEntity.IsDeclined = false;
            applicationEntity.IsDraft = true;
            applicationEntity.ApplicationStatus = ApplicationStatus.Withdrawn.ToString();
            applicationEntity.LastModifiedDate = DateTime.Now;

            result = _jobApplicationManager.UpdateJobApplication(applicationEntity);

            //If failed
            if (!result.Success)
                return Request.CreateResponse(HttpStatusCode.BadRequest, result);
            //If it not found
            if (result.Entity == null)
                return Request.CreateResponse(HttpStatusCode.NotFound, result);

            //If it is good
            return Request.CreateResponse(HttpStatusCode.OK, result);


        }


    }
}
