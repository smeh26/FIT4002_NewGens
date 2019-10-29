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
    public interface IJobApplicationManager
    {
        Result CreateJobApplication(JobApplicationEntity jobApplication);
        Result UpdateJobApplication(JobApplicationEntity jobApplication);
        Result GetJobApplicationByUserId(int userId);
        Result GetJobApplicationByListingId(int listingId);
        Result GetJobApplicationByApplicationId(int applicationId);


    }
    public class JobApplicationManager: IJobApplicationManager
    {


        public Result CreateJobApplication(JobApplicationEntity jobApplication)
        {
            Result result = new Result();
            try
            {
                var con = new DapperConnectionManager();
                var query = new QueryEntity();
                query.Entity = jobApplication;
                query.Query = @"
                BEGIN TRAN
                IF EXISTS (SELECT * FROM JobApplications WHERE JobListingId = @JobListingId and UserId = @UserId )
                BEGIN
                    UPDATE JobApplications SET  Summary = @Summary ,
                                                EmployerId = @EmployerId,
                                                IsDraft = @IsDraft, 
                                                ApplicationStatus = @ApplicationStatus , 
                                                AppliedDate = @AppliedDate , 
                                                LastModifiedDate= @LastModifiedDate
                    WHERE JobListingId = @JobListingId and UserId = @UserId
                END 
                ELSE
                BEGIN 
                    INSERT INTO JobApplications  (JobListingId, UserId, EmployerId, Summary, IsDraft, ApplicationStatus, AppliedDate, LastModifiedDate)
                                            VALUES  (@JobListingId,  @UserId, @EmployerId,  @Summary, @IsDraft, @ApplicationStatus, @AppliedDate, @LastModifiedDate)
                END
                SELECT * FROM JobApplications WHERE JobListingId = @JobListingId and UserId = @UserId
                COMMIT TRAN
                "; 
                result = con.ExecuteGetOneItemQuery<JobApplicationEntity>(query);

                return result;
            }
            catch (Exception ex)
            {
                Logger.Log(ex);
                result.Success = false;
                result.Message = ex.Message;
            }

            return result;
        }
        public Result UpdateJobApplication(JobApplicationEntity jobApplication)
        {
            Result result = new Result();
            try
            {
                var con = new DapperConnectionManager();
                var query = new QueryEntity();
                query.Entity = jobApplication;
                query.Query = @"
                BEGIN TRAN
                IF EXISTS (SELECT * FROM JobApplications WHERE JobListingId = @JobListingId and UserId = @UserId )
                BEGIN
                    UPDATE JobApplications SET  Summary = @Summary , 
                                                IsDraft = @IsDraft, 
                                                ApplicationStatus = @ApplicationStatus , 
                                                LastModifiedDate= @LastModifiedDate
                    WHERE JobListingId = @JobListingId and UserId = @UserId
                END 
                ELSE
                BEGIN 
                    INSERT INTO JobApplications  (JobListingId, UserId, Summary, IsDraft, ApplicationStatus, LastModifiedDate)
                                            VALUES  (@JobListingId, 
                                                        @UserId, 
                                                        @Summary, 
                                                        @IsDraft, 
                                                        @ApplicationStatus,
                                                        @LastModifiedDate
)
                END 
                COMMIT TRAN
                ";
                result = con.ExecuteQuery(query);

                return result;
            }
            catch (Exception ex)
            {
                Logger.Log(ex);
                result.Success = false;
                result.Message = ex.Message;
            }

            return result;
        }
        public Result GetJobApplicationByUserId( int userId)
        {
            Result result = new Result();
            try
            {
                var con = new DapperConnectionManager();
                var query = new QueryEntity();
                query.Entity = new  {UserId = userId };
                query.Query = @"
                SELECT * FROM JobApplications WHERE UserId = @UserId
                ";
                result = con.ExecuteQuery<JobApplicationEntity>(query);

                return result;
            }
            catch (Exception ex)
            {
                Logger.Log(ex);
                result.Success = false;
                result.Message = ex.Message;
            }

            return result;
        }

        public Result GetJobApplicationByListingId(int listingId)
        {
            Result result = new Result();
            try
            {
                var con = new DapperConnectionManager();
                var query = new QueryEntity();
                query.Entity = new { JobListingId = listingId };
                query.Query = @"
                SELECT * FROM JobApplications WHERE JobListingId = @JobListingId
                ";
                result = con.ExecuteQuery<JobApplicationEntity>(query);

                return result;
            }
            catch (Exception ex)
            {
                Logger.Log(ex);
                result.Success = false;
                result.Message = ex.Message;
            }

            return result;
        }
        public Result GetJobApplicationByApplicationId(int applicationId)
        {
            Result result = new Result();
            try
            {
                var con = new DapperConnectionManager();
                var query = new QueryEntity();
                query.Entity = new { ApplicationId = applicationId };
                query.Query = @"
                SELECT * FROM JobApplications WHERE ApplicationId = @ApplicationId
                ";
                result = con.ExecuteGetOneItemQuery<JobApplicationEntity>(query);

                return result;
            }
            catch (Exception ex)
            {
                Logger.Log(ex);
                result.Success = false;
                result.Message = ex.Message;
            }

            return result;
        }

        public Result ShortListApplication(int applicationId)
        {
            Result result = new Result();
            try
            {
                var con = new DapperConnectionManager();
                var query = new QueryEntity();
                query.Entity = new { ApplicationId = applicationId,
                                    ApplicationStatus = "SHORTLISTED",
                                    IsShortlisted = true,
                                    ShortListedDate = DateTime.Now,
                                    IsDeclined = false
                };
                query.Query = @"
                Update JobApplications SET 
                                            IsShortlisted = @IsShortlisted,
                                            ShortListedDate = @ShortListedDate,
                                            IsDeclined = @IsDeclined
                WHERE ApplicationId = @ApplicationId
                ";
                result = con.ExecuteGetOneItemQuery<JobApplicationEntity>(query);

                return result;
            }
            catch (Exception ex)
            {
                Logger.Log(ex);
                result.Success = false;
                result.Message = ex.Message;
            }

            return result;

        }

    }
}
