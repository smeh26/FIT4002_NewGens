﻿using System;
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
    public interface IEmployersManager
    {
        Result Register(EmployerEntity entity);
        Result Login(EmployerEntity entity);
        Result Login(string token);

        //Result LoginApna(EmployerEntity entity);
        Result GenerateRecoveringCode(EmployerEntity entity);
        Result ChangePassword(EmployerEntity entity);
        Result Delete(EmployerEntity entity);
        Result UpdateDetails(EmployerEntity entity);
        Result ResetPassword(EmployerEntity entity);
    }
    public class EmployersManager:IEmployersManager
    {
        public Result Register(EmployerEntity entity)
        {
            Result result = null;
            try
            {
                if (entity.Password.Length < 6)
                {
                    result = new Result(false);
                    result.Message = "Password length invalid";
                    return result;
                }

                var con = new DapperConnectionManager();
                var query = new QueryEntity();

                var credentials = new CredentialsManager();

                var hash = credentials.GenerateSalt();

                entity.Password = credentials.EncodePassword(entity.Password, hash);
                entity.Hash = hash;
                if (!entity.Email.Contains("@") || entity.Email.Length < 3)
                {
                    result = new Result(false);
                    result.Message = "Email invalid";
                    return result;
                }
                entity.Email = entity.Email.Trim().ToLower();

                var queryCheckEmail = new QueryEntity()
                {
                    Entity = new { Email = entity.Email },
                    Query = @"SELECT Email from Employers where Email = @Email and Active = 1"
                };
                var resultCheckEmail = con.ExecuteQuery<EmployerEntity>(queryCheckEmail);
                var dump = ObjectDumper.Dump(resultCheckEmail);
                if (!resultCheckEmail.Success)
                {
                    resultCheckEmail.Entity = null;
                    resultCheckEmail.Success = false;
                    resultCheckEmail.Message = "An error occurred with email check";
                    return resultCheckEmail;
                }
                var checkEmail = (IEnumerable<EmployerEntity>)resultCheckEmail.Entity;
                if (checkEmail.Any())
                {
                    resultCheckEmail.Entity = null;
                    resultCheckEmail.Success = false;
                    resultCheckEmail.Message = "The email is currently in use";
                    return resultCheckEmail;
                }

                entity.CreateDate = DateTime.Now;
                entity.ModifyDate = DateTime.Now;

                query.Entity = entity;
                query.Query = @"INSERT INTO Employers (Email, EmployerName, Password, Hash, CreateDate, ModifyDate) VALUES(@Email, @EmployerName, @Password, @Hash, @CreateDate, @ModifyDate)";

                result = con.InsertQuery(query);
                if (result.Success)
                {
                    entity.EmployerID = (int)result.Entity;
                    entity.Password = "";
                    entity.Hash = "";
                    entity.Token = credentials.GenerateEmployerToken(entity);
                    result.Entity = entity;

                    Task.Run(() => new EmailManager().SendEmail(entity.Email, DL.Models.EmailType.Welcome, new
                    {
                        CurrentUserName = entity.EmployerName,
                        CurrentUserEmail = entity.Email
                    }));

                }
                result.Message = result.Success ? "The employer has been created" : "An error occurred";

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
                result.Message = "An error occurred with exception";
            }
            return result;
        }

        public Result Login(EmployerEntity entity)
        {
            Result result = null;
            try
            {
                var con = new DapperConnectionManager();
                var query = new QueryEntity();
                var credentials = new CredentialsManager();
                entity.Email = entity.Email.Trim().ToLower();
                query.Query = @"SELECT * FROM Employers
                            where Email = @Email and Active = 1 and ApnaUser = 0";
                query.Entity = entity;
                result = con.ExecuteQuery<EmployerEntity>(query);

                if (!result.Success)
                {
                    result.Message = "Login error";
                    return result;
                }

                var r = (IEnumerable<EmployerEntity>)result.Entity;

                var employer = r.FirstOrDefault();

                if (employer == null)
                {
                    result.Message = "Invalid password or username";
                    result.Success = false;
                    result.Entity = null;
                    return result;
                }
                var password = credentials.EncodePassword(entity.Password, employer.Hash);
                if (password == employer.Password)
                {
                    employer.Hash = null;
                    employer.Password = null;
                    employer.Token = credentials.GenerateEmployerToken(employer);
                    result.Entity = employer;
                    return result;
                }
                result.Entity = null;
                result.Message = "Employer not found";
                result.Success = false;
            }
            catch (Exception ex)
            {
                Logger.Log(ex);
                result.Entity = null;
                result = result ?? new Result(false);
                result.Message = "An error occurred";
            }

            return result;
        }


/*        public Result LoginApna(EmployerEntity entity)
        {
            Result result = null;
            try
            {
                var con = new DapperConnectionManager();
                var query = new QueryEntity();
                var credentials = new CredentialsManager();
                entity.Email = entity.Email.Trim().ToLower();
                query.Query = @"SELECT * FROM Employers
                            where Email = @Email and Active = 1 and ApnaUser = 1";
                query.Entity = entity;
                result = con.ExecuteQuery<EmployerEntity>(query);

                if (!result.Success)
                {
                    result.Message = "Login error";
                    return result;
                }

                var r = (IEnumerable<EmployerEntity>)result.Entity;

                var user = r.FirstOrDefault();

                if (user == null)
                {
                    var resultRegisterUser = RegisterUserApna(entity, con);
                    if (!resultRegisterUser.Success)
                    {
                        return resultRegisterUser;
                    }
                    user = (EmployerEntity)resultRegisterUser.Entity;
                }
                user.Token = credentials.GenerateEmployerToken(user);
                result.Entity = user;
            }
            catch (Exception ex)
            {
                Logger.Log(ex);
                result.Entity = null;
                result = result ?? new Result(false);
                result.Message = "An error occurred";
            }

            return result;
        }
*/
/*        private Result RegisterUserApna(EmployerEntity entity, DapperConnectionManager con)
        {
            var query = new QueryEntity();
            entity.CreateDate = DateTime.Now;
            entity.ModifyDate = DateTime.Now;

            var credentials = new CredentialsManager();
            var hash = credentials.GenerateSalt();
            var hash2 = credentials.GenerateSalt();
            var password = credentials.EncodePassword(hash2, hash);

            entity.Hash = hash;
            entity.Password = password;
            entity.ApnaUser = true;

            query.Entity = entity;
            query.Query = @"INSERT INTO Employers (Email, Name, Password, Hash, CreateDate, ModifyDate, ApnaMemberId, Country, Suburb, State, PostalCode, ApnaUser) 
                            VALUES(@Email, @Name, @Password, @Hash, @CreateDate, @ModifyDate, @ApnaMemberId, @Country, @Suburb, @State, @PostalCode, @ApnaUser )";

            var result = con.InsertQuery(query);
            if (result.Success)
            {
                entity.EmployerID = (int)result.Entity;
                entity.Password = "";
                entity.Hash = "";
                entity.Token = credentials.GenerateEmployerToken(entity);
                result.Entity = entity;

                Task.Run(() => new EmailManager().SendEmail(entity.Email, DL.Models.EmailType.Welcome, new
                {
                    CurrentUserName = entity.Name,
                    CurrentUserEmail = entity.Email
                }));


            }
            return result;
        }*/

        public Result Login(string token)
        {
            var credentials = new CredentialsManager();
            var result = credentials.ValidateUserToken(token);
            var con = new DapperConnectionManager();
            var query = new QueryEntity();

            if (!result.Success)
            {
                return result;
            }

            var employer = (EmployerEntity)result.Entity;



            query.Entity = new { EmployerID = employer.EmployerID };
            query.Query = @"SELECT * FROM Employers
                            where EmployerID = @EmployerID and Active = 1 and ApnaUser = 0";

            result = con.ExecuteQuery<EmployerEntity>(query);

            if (!result.Success)
            {
                result = null;
                result.Message = "Login error";
                return result;
            }

            var r = (IEnumerable<EmployerEntity>)result.Entity;

            employer = r.FirstOrDefault();
            employer.Password = null;
            employer.Hash = null;
            result.Entity = employer;
            return result;
        }

       


        public Result GenerateRecoveringCode(EmployerEntity entity)
        {
            Result result = null;
            var con = new DapperConnectionManager();
            var query = new QueryEntity();
            var credentials = new CredentialsManager();
            entity.Email = entity.Email.Trim().ToLower();
            query.Query = @"SELECT * FROM Employers
                            where Email = @Email and Active = 1 and ApnaUser = 0";
            query.Entity = entity;
            result = con.ExecuteQuery<EmployerEntity>(query);

            if (!result.Success)
            {
                result.Entity = null;
                result.Message = "An error occurred";
                return result;
            }

            var r = (IEnumerable<EmployerEntity>)result.Entity;

            var employer = r.FirstOrDefault();

            if (employer == null)
            {
                result.Entity = null;
                result.Message = "Invalid user";
                result.Success = false;
                return result;
            }
            var Token = credentials.GenerateRecoverPasswordToken(employer);
            Token = HttpUtility.UrlEncode(Token);

            Task.Run(() => new EmailManager().SendEmail(entity.Email, DL.Models.EmailType.RecoverPassword, new
            {
                Token,
                CurrentUserName = employer.EmployerName,
                CurrentUserEmail = employer.Email,
                WebsiteUrl = ConfigurationManager.AppSettings["mnf.website"],
                ContentUrl = ConfigurationManager.AppSettings["mnf.content"]
            }));

            result.Entity = null;
            result.Message = "An email has been sent with instructions for recovering your password";
            result.Success = true;

            return result;
        }


        public Result ResetPassword(EmployerEntity entity)
        {
            Result result = null;
            try
            {
                //Double validation
                var credentials = new CredentialsManager();

                result = credentials.ValidateUserToken(entity.Token, true);
                if (!result.Success)
                {
                    return result;
                }
                var employer = (EmployerEntity)result.Entity;

                var con = new DapperConnectionManager();
                var query = new QueryEntity();

                //GET the user to check password
                query.Query = @"SELECT * FROM Employers
                            where EmployerID = @EmployerID and Active = 1";
                query.Entity = employer;

                result = con.ExecuteQuery<EmployerEntity>(query);

                if (!result.Success)
                {
                    result.Message = "Validation error";
                    return result;
                }

                var r = (IEnumerable<EmployerEntity>)result.Entity;

                employer = r.FirstOrDefault();

                if (employer == null)
                {
                    result.Message = "Validation error";
                    result.Success = false;
                    return result;
                }

                var newHash = credentials.GenerateSalt();

                entity.Hash = newHash;
                entity.Password = credentials.EncodePassword(entity.Password, newHash);

                query.Query = @"Update Employers Set Password = @Password, Hash = @Hash
                            where EmployerID = @EmployerID";
                query.Entity = new { EmployerID = employer.EmployerID, Hash = entity.Hash, Password = entity.Password };

                result = con.ExecuteQuery(query);
                result.Message = result.Success ? "The user password has been updated" : "An error has occurred";
            }
            catch (Exception ex)
            {
                Logger.Log(ex);
                result = result ?? new Result(false);
                result.Message = "An error occurred";
                throw;
            }

            return result;
        }

        public Result ChangePassword(EmployerEntity entity)
        {
            Result result = null;
            try
            {
                var credentials = new CredentialsManager();
                var con = new DapperConnectionManager();
                var query = new QueryEntity();

                //GET the user to check password
                query.Query = @"SELECT * FROM Employers
                            where EmployerID = @EmployerID and Active = 1";
                query.Entity = entity;
                result = con.ExecuteQuery<EmployerEntity>(query);

                if (!result.Success)
                {
                    result.Entity = null;
                    result.Message = "Validation error";
                    return result;
                }

                var r = (IEnumerable<EmployerEntity>)result.Entity;

                var user = r.FirstOrDefault();

                if (user == null)
                {
                    result.Entity = null;
                    result.Message = "Validation error";
                    result.Success = false;
                    return result;
                }


                var password = credentials.EncodePassword(entity.Password, user.Hash);
                if (password != user.Password)
                {
                    result.Entity = null;
                    result.Message = "Invalid password";
                    result.Success = false;
                    return result;
                }

                var newHash = credentials.GenerateSalt();

                entity.Hash = newHash;
                entity.Password = credentials.EncodePassword(entity.NewPassword, newHash);

                query.Query = @"Update Employers Set Password = @Password, Hash = @Hash
                            where EmployerID = @EmployerID";
                query.Entity = new { EmployerID = entity.EmployerID, Hash = entity.Hash, Password = entity.Password };

                result = con.ExecuteQuery<EmployerEntity>(query);
                result.Message = result.Success ? "The user password has been updated" : "An error has occurred";
            }
            catch (Exception ex)
            {
                Logger.Log(ex);
                result = result ?? new Result(false);
                result.Message = "An error occurred";
                throw;
            }

            return result;
        }

        public Result UpdateDetails(EmployerEntity entity)
        {
            Result result = null;
            try
            {
                if (!entity.Email.Contains("@") || entity.Email.Length < 3)
                {
                    result = new Result(false);
                    result.Message = "Email invalid";
                    return result;
                }

                var con = new DapperConnectionManager();
                var query = new QueryEntity();

                entity.Email = entity.Email.Trim().ToLower();


                query.Query = @"SELECT count(*) as n FROM Employers
                            where Email = @Email and Active = 1 and EmployerID <> @EmployerID";
                query.Entity = entity;
                result = con.ExecuteQuery(query);

                if (!result.Success)
                {
                    result.Entity = null;
                    result.Message = "Login error";
                    return result;
                }

                var countEmployers = (int)(((IEnumerable<dynamic>)result.Entity).First().n);
                if (countEmployers > 0)
                {
                    result.Entity = null;
                    result.Success = false;
                    result.Message = "Email already in use";
                }


                query.Query = @"Update Employers Set Name = @Name, Email = @Email
                            where EmployerID = @EmployerID and Active = 1";
                query.Entity = new { EmployerID = entity.EmployerID, Email = entity.Email, Name = entity.EmployerName };
                result = con.ExecuteQuery<EmployerEntity>(query);
                result.Message = result.Success ? "The user details has been updated" : "An error has occurred";
            }
            catch (Exception ex)
            {
                Logger.Log(ex);
                result = result ?? new Result(false);
                result.Message = "An error occurred";
                throw;
            }

            return result;
        }


        public Result Delete(EmployerEntity entity)
        {
            var credentials = new CredentialsManager();
            var result = credentials.ValidateUserToken(entity.Token);
            if (!result.Success)
            {
                return result;
            }
            var employer = (EmployerEntity)result.Entity;
            if (employer.EmployerID != entity.EmployerID)
            {
                result.Message = "Forbidden operation";
                result.Success = false;
                return result;
            }

            var con = new DapperConnectionManager();
            var query = new QueryEntity();
            entity.Email = entity.Email.Trim().ToLower();
            query.Query = @"Update Employers Set Active = 0
                            where EmployerID = @EmployerID";
            query.Entity = new { EmployerID = entity.EmployerID };

            result = con.ExecuteQuery<EmployerEntity>(query);
            result.Message = result.Success ? "The user has been deleted" : "An error has occurred";
            return result;
        }
    }
}