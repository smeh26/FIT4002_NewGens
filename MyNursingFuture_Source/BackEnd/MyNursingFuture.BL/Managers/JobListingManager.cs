using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyNursingFuture.BL.Entities;
using MyNursingFuture.DL;
using MyNursingFuture.Util;
using System.Web;
using Newtonsoft.Json;
using System.Transactions;
using System.Configuration;
namespace MyNursingFuture.BL.Managers
{
    public interface IJobListingManager
    {
        Result CreateJobListing(JobListingEntity entity, EmployerEntity employer);
        Result CreateJobListingById(JobListingEntity entity, int employerId); // Working, tested
        Result EditJobListing(JobListingEntity entity, EmployerEntity employer);
        Result PublishJobListing(JobListingEntity entity, EmployerEntity employer);
        Result DeleteJobListing(JobListingEntity entity, EmployerEntity employer);
        Result GetListingById(int listingId); //working, tested
        Result GetPotentialApplicantsByCriteria(List<JobListingCriteriaEntity> criteria);
        Result GetPotentialApplicantsByListingId(int jobListingId);
        Result GetAllListings();// working, tested 
        Result GetAllListingsByEmployer(EmployerEntity employer);
        Result GetAllListingsByEmployerV2(EmployerEntity employer);
        //Boolean IsListingBelongToEmployer(int ListingId, int EmployerId);


    }
    public class JobListingManager : IJobListingManager
    {
        private Result ValidateJL(JobListingEntity entity)
        {
            var result = new Result();
            try
            {
                var con = new DapperConnectionManager();
                var query = new QueryEntity();
                var credentials = new CredentialsManager();

                // Check if listing is valid

                query.Query = @"SELECT * FROM JobListings
                            where JobListingId = @JobListing";
                query.Entity = entity;
                result = con.ExecuteQuery<UserEntity>(query);

                if (!result.Success)
                {
                    result.Message = "Query Error";
                    return result;
                }
                var r = (IEnumerable<UserEntity>)result.Entity;

                var employer = r.FirstOrDefault();

                if (employer == null)
                {
                    result.Message = "Employer not exist";
                    result.Success = false;
                    result.Entity = null;
                    return result;
                }
            }
            catch (Exception ex)
            {
                if (result == null)
                {
                    result = new Result();
                }
                Logger.Log(ex);
                result.Entity = null;
                result.Success = false;
                result.Message = "An error occurred";
            }
            return result;

        }
        public Result CreateJobListing(JobListingEntity entity, EmployerEntity employer)
        {
            var result = new Result();
            try
            {
                var con = new DapperConnectionManager();
                var query = new QueryEntity();
                var credentials = new CredentialsManager();

                // Check if employer is valid

                result = ValidateEmployer(entity);

                if (result.Success == false)
                {
                    return result;
                }


                // check if the listing has required fields
                if (entity.Title == null ||
                    entity.NurseType == null ||
                    entity.Suburb == null)
                {

                    entity.PublishStatus = false;
                }

                entity.CreateDate = DateTime.Now;
                entity.ModificationDate = entity.CreateDate;

                // TODO : check for each element type and length



                query.Entity = entity;
                query.Query = @"INSERT INTO [dbo].[JobListings]
                                           ([EmployerId]
                                           ,[Title]
                                           ,[NurseType]
                                           ,[SpecialRequirements]
                                           ,[PublishStatus]
                                           ,[MinSalary]
                                           ,[MaxSalary]
                                           ,[CreateDate]
                                           ,[ApplicationDeadline]
                                           ,[ModificationDate]
                                           ,[Area]
                                           ,[State]
                                           ,[Country]
                                           ,[Suburb]
                                           ,[PostalCode]
                                           ,[AddressLine1]
                                           ,[AddressLine2]
                                           ,[Completed]
                                           ,[JobType])
                                     VALUES
                                           (@EmployerId 
                                           ,@Title
                                           ,@NurseType
                                           ,@SpecialRequirements
                                           ,@PublishStatus
                                           ,@MinSalary
                                           ,@MaxSalary
                                           ,@CreateDate
                                           ,@ApplicationDeadline
                                           ,@ModificationDate
                                           ,@Area
                                           ,@State
                                           ,@Country
                                           ,@Suburb
                                           ,@PostalCode
                                           ,@AddressLine1
                                           ,@AddressLine2
                                           ,@Completed
                                           ,@JobType)";


                result = con.ExecuteQuery<JobListingEntity>(query);
                return result;

            }
            catch (Exception ex)
            {
                if (result == null)
                {
                    result = new Result();
                }
                Logger.Log(ex);
                result.Entity = null;
                result.Success = false;
                result.Message = "An error occurred";
            }
            return result;


        }

        public Result CreateJobListingById(JobListingEntity entity, int employerId)
        {
            var result = new Result();
            try
            {
                var con = new DapperConnectionManager();
                var query = new QueryEntity();
                var credentials = new CredentialsManager();


                // check if the listing has required fields
                if (entity.Title == null ||
                    entity.NurseType == null ||
                    entity.Suburb == null)
                {

                    entity.PublishStatus = false;
                }

                entity.CreateDate = DateTime.Now;
                entity.ModificationDate = entity.CreateDate;

                if (entity.ApplicationDeadline < entity.CreateDate) entity.ApplicationDeadline = entity.CreateDate;

                // TODO : check for each element type and length



                query.Entity = entity;
                query.Query = @"


INSERT INTO [dbo].[JobListings]
                                           ([EmployerId]
                                           ,[Title]
                                           ,[NurseType]
                                           ,[SpecialRequirements]
                                           ,[PublishStatus]
                                           ,[MinSalary]
                                           ,[MaxSalary]
                                           ,[CreateDate]
                                           ,[ApplicationDeadline]
                                           ,[ModificationDate]
                                           ,[Area]
                                           ,[State]
                                           ,[Country]
                                           ,[Suburb]
                                           ,[PostalCode]
                                           ,[AddressLine1]
                                           ,[AddressLine2]
                                           ,[Completed]
                                           ,[JobType])

                                     VALUES
                                           ( " + employerId.ToString() + @" 
                                           ,@Title
                                           ,@NurseType
                                           ,@SpecialRequirements
                                           ,@PublishStatus
                                           ,@MinSalary
                                           ,@MaxSalary
                                           ,@CreateDate
                                           ,@ApplicationDeadline
                                           ,@ModificationDate
                                           ,@Area
                                           ,@State
                                           ,@Country
                                           ,@Suburb
                                           ,@PostalCode
                                           ,@AddressLine1
                                           ,@AddressLine2
                                           ,@Completed
                                           ,@JobType) ;
                                    
";


                result = con.InsertQuery(query);
                return result;

            }
            catch (Exception ex)
            {
                if (result == null)
                {
                    result = new Result();
                }
                Logger.Log(ex);
                result.Entity = null;
                result.Success = false;
                result.Message = "An error occurred";
            }
            return result;


        }

        public Result EditJobListing(JobListingEntity entity, EmployerEntity employer)
        {
            var result = new Result();
            try
            {
                var con = new DapperConnectionManager();
                var query = new QueryEntity();
                var credentials = new CredentialsManager();

                // Check if employer is valid

                result = ValidateEmployer(entity);

                if (result.Success == false)
                {
                    return result;
                }

                // check if the listing has required fields
                if (entity.Title == null ||
                    entity.NurseType == null ||
                    entity.Suburb == null)
                {

                    entity.PublishStatus = false;
                }

                entity.ModificationDate = DateTime.Now;

                // TODO : check for each element type and length



                query.Entity = entity;
                query.Query = @"UPDATE [dbo].[JobListings] set 
                                           [EmployerId] = @EmployerId 
                                           ,[Title] = @Title
                                           ,[NurseType] = ,@NurseType
                                           ,[SpecialRequirements] = @SpecialRequirements
                                           ,[PublishStatus] = @PublishStatus
                                           ,[MinSalary] = @MinSalary
                                           ,[MaxSalary] = @MaxSalary
                                           ,[ApplicationDeadline] = @ApplicationDeadline
                                           ,[ModificationDate] = @ModificationDate
                                           ,[PublishStatus] = @PublishStatus
                                           ,[Area] = @Area
                                           ,[State] = @State
                                           ,[Country] = @Country
                                           ,[Suburb] = @Suburb
                                           ,[PostalCode] = @PostalCode
                                           ,[AddressLine1] = @AddressLine1
                                           ,[AddressLine2] = @AddressLine2
                                           ,[Completed] = @Completed
                                           ,[JobType] =@JobType
                                     WHERE JobListingId = @JobListingId";


                result = con.InsertQuery(query);
                return result;
            }
            catch (Exception ex)
            {
                if (result == null)
                {
                    result = new Result();
                }
                Logger.Log(ex);
                result.Entity = null;
                result.Success = false;
                result.Message = "An error occurred";
            }
            return result;

        }

        public Result PublishJobListing(JobListingEntity entity, EmployerEntity employer)
        {
            //Validate required fields
            var result = new Result();
            try
            {
                var con = new DapperConnectionManager();
                var query = new QueryEntity();
                var credentials = new CredentialsManager();


                // check if the listing has required fields
                if (entity.JobListingId != 0)
                {

                    entity.PublishStatus = true;
                }


                entity.ModificationDate = DateTime.Now;

                // TODO : check for each element type and length



                query.Entity = entity;
                /*                query.Query = @"UPDATE [dbo].[JobListings] set 
                                                           [EmployerId] = @EmployerId 
                                                           ,[Title] = @Title
                                                           ,[NurseType] = ,@NurseType
                                                           ,[SpecialRequirements] = @SpecialRequirements
                                                           ,[PublishStatus] = @PublishStatus
                                                           ,[MinSalary] = @MinSalary
                                                           ,[MaxSalary] = @MaxSalary
                                                           ,[ApplicationDeadline] = @ApplicationDeadline
                                                           ,[ModificationDate] = @ModificationDate
                                                           ,[Area] = @Area
                                                           ,[State] = @State
                                                           ,[Country] = @Country
                                                           ,[Suburb] = @Suburb
                                                           ,[PostalCode] = @PostalCode
                                                           ,[AddressLine1] = @AddressLine1
                                                           ,[AddressLine2] = @AddressLine2
                                                           ,[Completed] = @Completed
                                                           ,[JobType] =@JobType
                                                     WHERE JobListingId = @JobListingId";*/

                query.Query = @"UPDATE [dbo].[JobListings] set [PublishStatus] = @PublishStatus  WHERE JobListingId = @JobListingId";
                result = con.InsertQuery(query);


                return result;
            }
            catch (Exception ex)
            {
                if (result == null)
                {
                    result = new Result();
                }
                Logger.Log(ex);
                result.Entity = null;
                result.Success = false;
                result.Message = "An error occurred";
            }
            return result;




        }

        // To hide the Listing
        public Result DeleteJobListing(JobListingEntity entity, EmployerEntity employer)
        {


            var result = new Result();
            try
            {
                var con = new DapperConnectionManager();
                var query = new QueryEntity();
                var credentials = new CredentialsManager();

                // confirm if the employer is the one that deleting 
                if (employer.EmployerId != entity.EmployerId)
                {
                    result.Message = "Forbidden operation";
                    result.Success = false;
                    return result;
                }

                // create a query for hiding the listing 
                query.Entity = entity;
                query.Query = @"UPDATE JobListings set Hidden = 1 WHERE JobListingId = @JobListingId";
                con.ExecuteQuery(query);
                result.Message = result.Success ? "The listing has been deleted" : "An error has occurred";
                return result;
            }
            catch (Exception ex)
            {
                if (result == null)
                {
                    result = new Result();
                }
                Logger.Log(ex);
                result.Entity = null;
                result.Success = false;
                result.Message = "An error occurred";
            }
            return result;


        }

        public Result GetListingById(int listingId)
        {
            var result = new Result();
            try
            {

                var credentials = new CredentialsManager();

                var con = new DapperConnectionManager();
                var query = new QueryEntity
                {
                    Entity = new { JobListingId = listingId },
                    Query = @"SELECT *
                          FROM JobListings
                          where JobListingId = @JobListingId"
                };


                result = con.ExecuteGetOneItemQuery<JobListingEntity>(query);

                var listing = (JobListingEntity)result.Entity;
                var listing_cri_man = new JobListingCriteriaManager();


                var criteria = (List<JobListingCriteriaEntity>)listing_cri_man.GetCriteriaByListingId(listing.JobListingId).Entity;
                listing.JobListingCriteria = criteria;

                result.Entity = listing;

                return result;


            }
            catch (Exception ex)
            {
                if (result == null)
                {
                    result = new Result();
                }
                Logger.Log(ex);
                result.Entity = null;
                result.Success = false;
                result.Message = "An error occurred" + ex.Message;
            }
            return result;
        }

        public Result GetAllListings()
        {
            var result = new Result();
            try
            {

                var credentials = new CredentialsManager();

                var con = new DapperConnectionManager();
                var query = new QueryEntity
                {
                    Entity = new { },
                    Query = @"SELECT *
                          FROM JobListings
                    "
                };

                result = con.ExecuteQuery<JobListingEntity>(query);
                var listing_list = (List<JobListingEntity>)result.Entity;

                var listing_cri_man = new JobListingCriteriaManager();
                foreach (JobListingEntity listing in listing_list) {

                    var criteria = (List<JobListingCriteriaEntity>) listing_cri_man.GetCriteriaByListingId(listing.JobListingId).Entity;
                    listing.JobListingCriteria = criteria;
                }

                result.Entity = listing_list;
                return result;



            }
            catch (Exception ex)
            {
                if (result == null)
                {
                    result = new Result();
                }
                Logger.Log(ex);
                result.Entity = null;
                result.Success = false;
                result.Message = "An error occurred" + ex.Message;
            }
            return result;


        }

        private Result ValidateEmployer(JobListingEntity entity)
        {
            var result = new Result();
            try
            {
                var con = new DapperConnectionManager();
                var query = new QueryEntity();
                var credentials = new CredentialsManager();

                // Check if employer is valid

                query.Query = @"SELECT * FROM Employers
                            where EmployerID = @EmployerID and Active = 1";
                query.Entity = entity;
                result = con.ExecuteQuery<UserEntity>(query);

                if (!result.Success)
                {
                    result.Message = "Query Error";
                    return result;
                }
                var r = (IEnumerable<UserEntity>)result.Entity;

                var employer = r.FirstOrDefault();

                if (employer == null)
                {
                    result.Message = "Employer not exist";
                    result.Success = false;
                    result.Entity = null;
                    return result;
                }
            }
            catch (Exception ex)
            {
                if (result == null)
                {
                    result = new Result();
                }
                Logger.Log(ex);
                result.Entity = null;
                result.Success = false;
                result.Message = "An error occurred";
            }
            return result;

        }

        public Result GetPotentialApplicantsByCriteria(List<JobListingCriteriaEntity> criteria)
        {
            var result = new Result();
            try
            {
                var con = new DapperConnectionManager();
                var query = new QueryEntity();
                var credentials = new CredentialsManager();

                string query_string = "SELECT UserId FROM NurseSelfAssessmentAnswers  ";
                List<String> select_queries = new List<String>();
                int counter = 0;
                foreach (JobListingCriteriaEntity criterion in criteria)
                {
                    /*                    select_queries.Add(String.Format(" (SELECT UserId FROM NurseSelfAssessmentAnswers WHERE AspectId = {0} AND Value >= {1} ) AS T{2} ON H.UserId = T{2}.UserId "
                                            , criterion.AspectId, criterion.Value , counter));*/
                    // query_string += String.Format("INNER JOIN (SELECT UserId FROM NurseSelfAssessmentAnswers WHERE AspectId = {0} AND Value >= {1} ) AS T{2} ON H.UserId = T{2}.UserId ", criterion.AspectId, criterion.Value, counter);
                    query_string += String.Format("INTERSECT (SELECT UserId FROM NurseSelfAssessmentAnswers WHERE AspectId = {0} AND Value >= {1} ) ", criterion.AspectId, criterion.Value);

                    counter++;
                }

                query.Query = query_string;
                return con.ExecuteQuery(query);

            }
            catch (Exception ex)
            {
                if (result == null)
                {
                    result = new Result();
                }
                Logger.Log(ex);
                result.Entity = null;
                result.Success = false;
                result.Message = ex.Message;
            }
            return result;

        }


        public Result GetPotentialApplicantsByListingId(int jobListingId)
        {
            var result = new Result();
            try
            {
                var con = new DapperConnectionManager();
                var query = new QueryEntity();
                var credentials = new CredentialsManager();

                var JLM = new JobListingManager();
                var JLCM = new JobListingCriteriaManager();

                //Get Listing 

                var listing = (JobListingEntity)JLM.GetListingById(jobListingId).Entity;
                if (listing == null)
                {
                    result.Success = false;
                    result.Message = "Listing not exist";
                    return result;
                }

                
                listing.maxSalary = listing.maxSalary == 0 ? 200000 : listing.maxSalary;
                listing.minSalary = listing.minSalary == 0 ? 40000 : listing.minSalary;



                // get criteria  
                var Listing_Re = JLCM.GetCriteriaByListingId(jobListingId);
                if (!Listing_Re.Success)
                {
                    result.Success = false;
                    result.Message = Listing_Re.Message;
                    return result; 
                }
                var criteria = (List<JobListingCriteriaEntity>)Listing_Re.Entity;

                // Assemble inner join query
                string query_string = String.Format(@"WITH SRC AS (SELECT UserId FROM Users WHERE {0} > = minsalary  AND maxsalary >=  {1} AND IsLookingForJob = 1 ) SELECT DISTINCT T0.UserId FROM SRC AS T0 ", listing.maxSalary,  listing.minSalary);
                List<String> select_queries = new List<String>();
                int counter = 1;
                foreach (JobListingCriteriaEntity criterion in criteria)
                {
                    /*                    select_queries.Add(String.Format(" (SELECT UserId FROM NurseSelfAssessmentAnswers WHERE AspectId = {0} AND Value >= {1} ) AS T{2} ON H.UserId = T{2}.UserId "
                                            , criterion.AspectId, criterion.Value , counter));*/
                    //query_string += String.Format(" INNER JOIN (SELECT DISTINCT UserId FROM NurseSelfAssessmentAnswers WHERE AspectId = {0} AND Value >= {1} ) AS T{2} ON T{3}.UserId = T{2}.UserId ", criterion.AspectId, criterion.Value, counter, counter - 1);
                    query_string += String.Format("INTERSECT (SELECT UserId FROM NurseSelfAssessmentAnswers WHERE AspectId = {0} AND Value >= {1} ) ", criterion.AspectId, criterion.Value);

                    counter++;
                }

                query.Query = query_string;
                return con.ExecuteQuery<int>(query);

            }
            catch (Exception ex)
            {
                if (result == null)
                {
                    result = new Result();
                }
                Logger.Log(ex);
                result.Entity = null;
                result.Success = false;
                result.Message = ex.Message;
            }
            return result;

        }

        private enum Mode { DICTQuestionIdValue, DICTQuestionIdObject, LISTObject }
        private dynamic transformCriteria(List<JobListingCriteriaEntity> criteria, Mode mode)
        {
            switch (mode)
            {
                case Mode.DICTQuestionIdValue:


                    return criteria.ToDictionary(x => x.QuestionId, x => x.Value);
                case Mode.DICTQuestionIdObject:
                    return criteria.ToDictionary(x => x.QuestionId, x => x);
                case Mode.LISTObject:
                    return criteria;
                default:
                    return null;
            }


        }

        public Result GetAllListingsByEmployer(EmployerEntity employer) {
            var result = new Result();
            try
            {

                var credentials = new CredentialsManager();

                var con = new DapperConnectionManager();
                var query = new QueryEntity
                {
                    Entity = employer,
                    Query = @"SELECT *
                          FROM JobListings
                          WHERE EmployerId= @EmployerId
                    "
                };

                result = con.ExecuteQuery<JobListingEntity>(query);
                var listing_list = (List<JobListingEntity>)result.Entity;

                var listing_cri_man = new JobListingCriteriaManager();
                foreach (JobListingEntity listing in listing_list)
                {

                    var criteria = (List<JobListingCriteriaEntity>)listing_cri_man.GetCriteriaByListingId(listing.JobListingId).Entity;
                    listing.JobListingCriteria = criteria;
                }

                result.Entity = listing_list;
                return result;



            }
            catch (Exception ex)
            {
                if (result == null)
                {
                    result = new Result();
                }
                Logger.Log(ex);
                result.Entity = null;
                result.Success = false;
                result.Message = "An error occurred" + ex.Message;
            }
            return result;

        }


        public Result GetAllListingsByEmployerV2(EmployerEntity employer)
        {
            var result = new Result();
            try
            {

                var credentials = new CredentialsManager();

                var con = new DapperConnectionManager();
                var query = new QueryEntity
                {
                    Entity = employer,
                    Query = @"SELECT *
                          FROM JobListings
                          WHERE EmployerId= @EmployerId
                    "
                };

                result = con.ExecuteQuery<JobListingEntity>(query);
                var listing_list = (List<JobListingEntity>)result.Entity;

                var formatted_listing_list = new List<dynamic>();
                var listing_cri_man = new JobListingCriteriaManager();
                foreach (JobListingEntity listing in listing_list)
                {

                    var criteria = (List<JobListingCriteriaEntity>)listing_cri_man.GetCriteriaByListingId(listing.JobListingId).Entity;
                    var formatted_Criteria = transformCriteria(criteria, Mode.DICTQuestionIdValue);
                    listing.JobListingCriteria_Dict_QuestionID_Value = formatted_Criteria;
                }

                result.Entity = listing_list;
                return result;



            }
            catch (Exception ex)
            {
                if (result == null)
                {
                    result = new Result();
                }
                Logger.Log(ex);
                result.Entity = null;
                result.Success = false;
                result.Message = "An error occurred" + ex.Message;
            }
            return result;

        }




    }
}




