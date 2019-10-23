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
using System.Threading.Tasks;
using Swashbuckle.Swagger.Annotations;

namespace MyNursingFuture.Api.Controllers
{
    [ExceptionFilter]
    public class JobListingsController : ApiController
    {
        private readonly IUsersManager _usersManager;
        private readonly IEmployersManager _employersManager;
        private readonly ICacheManager _cacheManager;
        private readonly IJobListingManager _jobListingManager;
        private readonly IJobListingCriteriaManager _jobListingCriteriaManager;
        public JobListingsController(IUsersManager usersManager,
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

        struct PostListingResponse {
            public string Message { get; set; }
            public bool Success { get; set; }
            public JobListingEntity Entity { get; set; }
        }

        /// <summary>
        /// API to post a job listing 
        /// </summary>
        /// <remarks> 
        /// Joblisting requires the following minimum fields:
        /// employerId
        /// maxsalary
        /// minsalary
        /// 
        /// TODO: enforce required fields
        /// </remarks>
        /// <response code="200"></response>
        /// <response code="400"></response>
        /// <response code="500"></response>
        [HttpPut]
        [EmployerJWTAuthorized]
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(PostListingResponse))]
        [Route("api/v1/JobListings")]
        public HttpResponseMessage PostListing(JobListingEntity jobListing)
        {
            //Woking , tested

            var result = new Result();

            //Employer vertification 
            object objemployer = null;
            Request.Properties.TryGetValue("employer", out objemployer);
            if (objemployer == null)
                return Request.CreateResponse(HttpStatusCode.Unauthorized, new Result(false));
            var employer = objemployer as EmployerEntity;
            var employerentity_full = (EmployerEntity) _employersManager.GetEmployerById(employer.EmployerId).Entity;

            if (employerentity_full.MembershipEndDate == DateTime.MinValue ||  DateTime.Compare(employerentity_full.MembershipEndDate, DateTime.Now) < 0 ) 
                {
                // membership expired
                result = new Result();
                result.Success = false;
                result.Message = "Membership expired";

                return Request.CreateResponse(HttpStatusCode.Unauthorized, result);
            } 


            jobListing.CreateDate = DateTime.Now;
            jobListing.EmployerId = employer.EmployerId;
            jobListing.ModificationDate = jobListing.CreateDate;
            result = _jobListingManager.CreateJobListingById(jobListing, employer.EmployerId);

            // return failed 
            if (!result.Success)
                return Request.CreateResponse(HttpStatusCode.BadRequest , result);


            // if success
            if (jobListing.JobListingCriteria != null || jobListing.JobListingCriteria.Count > 0) {
                var cri_res = _jobListingCriteriaManager.InsertCriteria(jobListing.JobListingCriteria);
            }

            //Result cri_result = null;
            var listingID = (int)result.Entity;
            var listing = (JobListingEntity) _jobListingManager.GetListingById(listingID).Entity;
            result.Entity = listing;
            return Request.CreateResponse(HttpStatusCode.Created, result);

        }

        /// <summary>
        /// API to retrieve all listing on the system
        /// </summary>
        /// <remarks> TODO: enforce required fields </remarks>
        /// <response code="200"></response>
        /// <response code="400"></response>
        /// <response code="500"></response>
        [HttpGet]
        [Route("api/v1/JobListings")]
        public HttpResponseMessage GetAllListings()
        {
            //Working, tested
            var result = new Result();
            object objemployer = null;
            Request.Properties.TryGetValue("employer", out objemployer);
            var employer = objemployer as EmployerEntity;

            result = _jobListingManager.GetAllListings();

            if (!result.Success)
                return Request.CreateResponse(HttpStatusCode.BadRequest, result);

            if (result.Entity == null)
                return Request.CreateResponse(HttpStatusCode.NotFound, result);


            return Request.CreateResponse(HttpStatusCode.OK, result);


        }
        /// <summary>
        /// API to retrieve all listings of the current logged in employer
        /// </summary>
        /// <remarks> TODO: enforce required fields </remarks>
        /// <response code="200"></response>
        /// <response code="400"></response>
        /// <response code="500"></response>
        [EmployerJWTAuthorized]
        [HttpGet]
        [Route("api/v1/JobListings/Employers")]
        public HttpResponseMessage GetAllListingsOfCurrentEmployer()
        {
            //Working, tested
            var result = new Result();
            object objemployer = null;
            Request.Properties.TryGetValue("employer", out objemployer);
            var employer = objemployer as EmployerEntity;

            result = _jobListingManager.GetAllListingsByEmployer(employer);

            if (!result.Success)
                return Request.CreateResponse(HttpStatusCode.BadRequest, result);

            if (result.Entity == null)
                return Request.CreateResponse(HttpStatusCode.NotFound, result);


            return Request.CreateResponse(HttpStatusCode.OK, result);


        }


        /*        [HttpPut]
                [EmployerJWTAuthorized]
                [Route("api/v1/JobListings/Criteria")]
                public HttpResponseMessage PutCriteria([FromBody] List<JobListingCriteriaEntity> jobListingCriteria)
                {
                    var result = new Result();
                    if (jobListingCriteria.Count == 0)
                    {
                        result = new Result(false);
                        return Request.CreateResponse(HttpStatusCode.NotFound, result);
                    }
                    result = _jobListingCriteriaManager.InsertCriteria(jobListingCriteria);

                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
        */

        /// <summary>
        /// API to put in list of required answers 
        /// </summary>
        /// <remarks> 
        /// 
        /// All fields are required  </remarks>
        /// <response code="200"></response>
        /// <response code="400"></response>
        /// <response code="500"></response>
        [HttpPut]
        [EmployerJWTAuthorized]
        [Route("api/v1/JobListings/Criteria")]
        public HttpResponseMessage PutCriteria_V1([FromBody] ListingCriteriaModel listingCriteriaModel)
        {
            var result = new Result();

            //Employer vertification 
            object objemployer = null;
            Request.Properties.TryGetValue("employer", out objemployer);
            if (objemployer == null)
                return Request.CreateResponse(HttpStatusCode.Unauthorized, new Result(false));
            var employer = objemployer as EmployerEntity;
            if (employer.EmployerId != listingCriteriaModel.EmmployerId)
                return Request.CreateResponse(HttpStatusCode.Unauthorized, new Result(false));


            var timestamp = DateTime.Now;

            var ListingCriteria = new List<JobListingCriteriaEntity>();


            foreach (KeyValuePair<int, AnswerEntity> entry in listingCriteriaModel.Answers)
            {
                int aspectId = entry.Key;
                AnswerEntity answer = entry.Value;

                var listingCriterion = new JobListingCriteriaEntity();
                listingCriterion.AspectId = aspectId;
                listingCriterion.QuestionId = answer.QuestionId;
                listingCriterion.AnswerId = answer.AnswerId;
                listingCriterion.Value = answer.Value;
                listingCriterion.LastUpdate = timestamp;
                listingCriterion.JobListingId = listingCriteriaModel.JobListingId;

                ListingCriteria.Add(listingCriterion);

            }

            result = _jobListingCriteriaManager.InsertCriteria(ListingCriteria);

            if (!result.Success)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, result);
            }

            result.Message = " Criteria updated successfully";
            return Request.CreateResponse(HttpStatusCode.OK, result);


        }

        /// <summary>
        /// Get a single listing object by Id
        /// </summary>
        /// <remarks> TODO : return the listing  </remarks>
        /// <response code="200"></response>
        /// <response code="400"></response>
        /// <response code="500"></response>
        //[JwtAuthorized]
        [HttpGet]
        [GenericJWTAuthorized]
        [Route("api/v1/JobListings/{id}")]
        public HttpResponseMessage GetListingById(int id)
        {
            //Working, tested
            var result = new Result();

            result = _jobListingManager.GetListingById(id);

            //Test the return

            if (!result.Success)
                return Request.CreateResponse(HttpStatusCode.BadRequest, result);

            if (result.Entity == null)
                return Request.CreateResponse(HttpStatusCode.NotFound, result);


            return Request.CreateResponse(HttpStatusCode.OK, result);

        }

        /// <summary>
        /// Get all applicants (ID only) meeting the requirement of a listing (indexed by Id) 
        /// </summary>
        /// <remarks> 
        /// Check list:
        /// - check if looking for job - done
        /// - check if salary is in prefered range - done 
        /// - check if nurse meet the attached citeria - done 
        /// Return a list of nurse ID
        /// -> use this to estimate the number of matches. 
        /// -> use 
        /// //TODO : filter Applicant by their prefered quiz result only ( currently it take into account everything )
        /// 
        /// </remarks>
        /// <response code="200"></response>
        /// <response code="400"></response>
        /// <response code="500"></response>
        [HttpGet]
        [EmployerJWTAuthorized]
        [Route("api/v1/JobListings/PotentialApplicants/{id}")]
        public HttpResponseMessage GetPotentialApplicantsByListingId(int id)
        {
            var result = new Result();

            //Employer vertification 
            object objemployer = null;
            Request.Properties.TryGetValue("employer", out objemployer);
            if (objemployer == null)
                return Request.CreateResponse(HttpStatusCode.Unauthorized, new Result(false));
            var employer = objemployer as EmployerEntity;

            var listing = (JobListingEntity) _jobListingManager.GetListingById(id).Entity;


            if (employer.EmployerId != listing.EmployerId)
                return Request.CreateResponse(HttpStatusCode.Unauthorized, new Result(false));



            result = _jobListingManager.GetPotentialApplicantsByListingId(id);
            if (!result.Success)
                return Request.CreateResponse(HttpStatusCode.BadRequest, result);

            if (result.Entity == null)
                return Request.CreateResponse(HttpStatusCode.NotFound, result);


            return Request.CreateResponse(HttpStatusCode.OK, result);

        }

/*        [EmployerJWTAuthorized]
        [Route("api/v1/JobListings/PotentialApplicants/")]
        public HttpResponseMessage GetPotentialApplicantsByCriteria([FromBody] List<JobListingCriteriaEntity> jobListingCriteria)
        {
            var result = new Result();

            result = _jobListingManager.GetPotentialApplicantsByCriteria(jobListingCriteria);
            if (!result.Success)
                return Request.CreateResponse(HttpStatusCode.BadRequest, result);

            if (result.Entity == null)
                return Request.CreateResponse(HttpStatusCode.NotFound, result);


            return Request.CreateResponse(HttpStatusCode.OK, result);

        }*/





    }   
}
