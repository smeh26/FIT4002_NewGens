/**
 * 
 * <Author> Nguyen Pham - 27348032  </Author>
 * <copyright> The following code is the work of Nguyen Pham unless other wise specified  </copyright>
 * <remarks> This is a part of the FIT4002 project. Product owner is APNA. Project supervisor is Robyn McNamara  </remarks>
 * <date>  </date>
 * <summary> </summary>
 */

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

    public enum JobListingStatus {

        Draft,
        Published,
        Expired


    }

    [ExceptionFilter]
    public class JobListingsController : ApiController
    {
        private readonly IUsersManager _usersManager;
        private readonly IEmployersManager _employersManager;
        private readonly ICacheManager _cacheManager;
        private readonly IJobListingManager _jobListingManager;
        private readonly IJobListingCriteriaManager _jobListingCriteriaManager;
        private readonly INurseSelfAssessmentAnswersManager _nurseSelfAssessmentManager;
        public JobListingsController(IUsersManager usersManager,
         IEmployersManager employersManager,
         ICacheManager cacheManager,
         IJobListingManager jobListingManager,
         IJobListingCriteriaManager jobListingCriteriaManager,
         INurseSelfAssessmentAnswersManager nurseSelfAssessmentAnswersManager
         )
        {
            _usersManager = usersManager;
            _employersManager = employersManager;
            _cacheManager = cacheManager;
            _jobListingCriteriaManager = jobListingCriteriaManager;
            _jobListingManager = jobListingManager;
            _nurseSelfAssessmentManager = nurseSelfAssessmentAnswersManager;


        }
        //======================================================================================================================

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

            if (employerentity_full.MembershipEndDate == DateTime.MinValue ||  employerentity_full.MembershipEndDate < (DateTime?) DateTime.Now ) 
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

        //======================================================================================================================

        private struct GetAllListingsResponse
        {
            public string Message { get; set; }
            public bool Success { get; set; }
            public List<JobListingEntity> Entity { get; set; }
        }
        /// <summary>
        /// API to retrieve all listing on the system
        /// </summary>
        /// <remarks> TODO: enforce required fields </remarks>
        /// <response code="200"></response>
        /// <response code="400"></response>
        /// <response code="500"></response>
        [HttpGet]
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(GetAllListingsResponse))]
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


        //======================================================================================================================

        /// <summary>
        /// API to retrieve all listings of the current logged in employer
        /// </summary>
        /// <remarks> TODO: enforce required fields </remarks>
        /// <response code="200"></response>
        /// <response code="400"></response>
        /// <response code="500"></response>
        [EmployerJWTAuthorized]
        [HttpGet]
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(GetAllListingsResponse))]
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
        //======================================================================================================================

        /// <summary>
        /// API to retrieve all potential Job listing for the current logged in nurse
        /// </summary>
        /// <remarks> TODO: enforce required fields </remarks>
        /// <response code="200"></response>
        /// <response code="400"></response>
        /// <response code="500"></response>
        [JwtAuthorized]
        [HttpGet]
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(GetAllListingsResponse))]
        [Route("api/v1/JobListings/User")]
        public HttpResponseMessage GetAllListingsOfCurrentNurse()
        {
            //Working, tested
            var result = new Result();
            object objuser = null;
            Request.Properties.TryGetValue("user", out objuser);
            var user = objuser as UserEntity;
            if (user == null)
            {
                result = new Result(false);
                return Request.CreateResponse(HttpStatusCode.Unauthorized, result);
            }

            var nurse = (UserEntity)_usersManager.GetUserDetails(user).Entity;

            if (nurse.defaultQuizId == 0 ) {
                result.Success = false;
                result.Message = "User must complete and nominate a Quizz to use as their profile";
                result.Entity = null;
                return Request.CreateResponse(HttpStatusCode.NotFound, result);
            }

            result = _nurseSelfAssessmentManager.GetAnswersbyUserQuizzId(nurse.defaultQuizId);
            var answersList = (List<NurseSelfAssessmentAnswersEntity>) result.Entity;

            result = _jobListingManager.GetAllListingsByNurseSelfAssessmentAnswer(answersList);

            if (!result.Success)
                return Request.CreateResponse(HttpStatusCode.BadRequest, result);

            if (result.Entity == null)
                return Request.CreateResponse(HttpStatusCode.NotFound, result);

            var listings = (List<int>)result.Entity;
            var listingEntityLists = new List<JobListingEntity>();

            foreach(int listingId in listings)
            {
                var lt_entity = (JobListingEntity)_jobListingManager.GetListingById(listingId).Entity;
                if ( lt_entity.maxSalary >= nurse.salary) {
                    listingEntityLists.Add(lt_entity);

                }


            }

            result.Entity = listingEntityLists;

            return Request.CreateResponse(HttpStatusCode.OK, result);


        }



        //========================================================================================================================
        private struct JLModel
        {
            public int JobListingId { get; set; }
            public int EmployerId { get; set; }
            public string Title { get; set; }
            public string NurseType { get; set; }
            public string SpecialRequirements { get; set; }
            public bool PublishStatus { get; set; }
            public int minSalary { get; set; }
            public int maxSalary { get; set; }
            public DateTime CreateDate { get; set; }
            public DateTime ApplicationDeadline { get; set; }

            public DateTime ModificationDate { get; set; }
            public string Area { get; set; }
            public string State { get; set; }
            public string Country { get; set; }
            public string Suburb { get; set; }
            public string PostalCode { get; set; }
            public string AddressLine1 { get; set; }
            public string AddressLine2 { get; set; }
            public bool Completed { get; set; }
            public string JobType { get; set; }

            public Dictionary<int, decimal> jobListingCriteria_Dict_QuestionID_Value { get; set; }
        }
        private struct GetAllListingsResponseV2
        {
            public string Message { get; set; }
            public bool Success { get; set; }
            public List<JLModel>  Entity { get; set; }
       
        }
        /// <summary>
        /// API to retrieve all listings of the current logged in employer
        /// </summary>
        /// <remarks> The type  of jobListingCriteria_Dict_QuestionID_Value is in a dictionary which the key is the question id and the value is the value of the anwser
        /// 
        /// 
        /// <code>
        /// jobListingCriteria_Dict_QuestionID_Value :
        ///     {
        ///     int  : decimal  # QuestionID : Value  
        /// } 
        /// 
        /// </code>
        /// 
        /// 
        /// 
        ///  </remarks>
        /// <response code="200"></response>
        /// <response code="400"></response>
        /// <response code="500"></response>
        [EmployerJWTAuthorized]
        [HttpGet]
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(GetAllListingsResponseV2))]
        [Route("api/v2/JobListings/Employers")]
        public HttpResponseMessage GetAllListingsOfCurrentEmployerV2()
        {
            //Working, tested
            var result = new Result();
            object objemployer = null;
            Request.Properties.TryGetValue("employer", out objemployer);
            var employer = objemployer as EmployerEntity;

            result = _jobListingManager.GetAllListingsByEmployerV2(employer);

            if (!result.Success)
                return Request.CreateResponse(HttpStatusCode.BadRequest, result);

            if (result.Entity == null)
                return Request.CreateResponse(HttpStatusCode.NotFound, result);


            return Request.CreateResponse(HttpStatusCode.OK, result);


        }


        //======================================================================================================================

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
        //======================================================================================================================



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
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(PostListingResponse))]
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

        //======================================================================================================================

        struct GetUserByListingResponse
        {
            public string Message { get; set; }
            public bool Success { get; set; }
            public List<int> Entity { get; set; }
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
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(GetUserByListingResponse))]
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

        //======================================================================================================================
        struct GetNumberofUserByCriteriaResponse
        {
            public string Message { get; set; }
            public bool Success { get; set; }
            public int Entity { get; set; }
        }
        /// <summary>
        /// Get the number of nurses meeting the requirement 
        /// </summary>
        /// <remarks> 
        /// To perform this, payment not required.
        /// 
        /// </remarks>
        /// <response code="200"></response>
        /// <response code="400"></response>
        /// <response code="500"></response>
        [EmployerJWTAuthorized]
        [HttpGet]
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(GetNumberofUserByCriteriaResponse))]
        [Route("api/v1/JobListings/PreviewPotentialApplicants/")]
        public HttpResponseMessage GetPotentialApplicantsByCriteria([FromBody] List<JobListingCriteriaEntity> jobListingCriteria)
        {
            var result = new Result();

            result = _jobListingManager.GetPotentialApplicantsByCriteria(jobListingCriteria);
            if (!result.Success)
                return Request.CreateResponse(HttpStatusCode.BadRequest, result);

            result.Entity = ((List<int>)result.Entity).Count();
            result.Message = "Total number of nurses meet the criteria in the system";

            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        //======================================================================================================================

        /// <summary>
        /// Edit Listing  
        /// </summary>
        /// <remarks> 
        /// To perform this, payment not required.
        /// 
        /// </remarks>
        /// <response code="200"></response>
        /// <response code="400"></response>
        /// <response code="500"></response>
        [EmployerJWTAuthorized]
        [HttpPut]
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(PostListingResponse))]
        [Route("api/v1/JobListings/Edit/")]
        public HttpResponseMessage UpdateJobListing([FromBody] JobListingEntity joblisting)
        {
            var result = new Result();

            object objemployer = null;
            Request.Properties.TryGetValue("employer", out objemployer);
            if (objemployer == null)
                return Request.CreateResponse(HttpStatusCode.Unauthorized, new Result(false));
            var employer = objemployer as EmployerEntity;

            //get full entity
            result = _employersManager.GetEmployerById(joblisting.EmployerId);
            var employer_entity = (EmployerEntity)result.Entity;


            if (employer.EmployerId != employer_entity.EmployerId)
                return Request.CreateResponse(HttpStatusCode.Unauthorized, new Result(false));

            if (employer_entity.MembershipEndDate < (DateTime?)DateTime.Now )
            {
                joblisting.PublishStatus = false;
                
            }

            result = new Result();

            result = _jobListingManager.EditJobListing(joblisting);
            if (!result.Success)
                return Request.CreateResponse(HttpStatusCode.BadRequest, result);
            result.Entity = _jobListingManager.GetListingById(joblisting.JobListingId).Entity;
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }












    }
}
