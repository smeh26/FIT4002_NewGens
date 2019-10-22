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
    public interface IUsersManager
    {
        Result Register(UserEntity entity);
        Result Login(UserEntity entity);
        Result Login(string token);
        Result LoginApna(UserEntity entity);
        Result GenerateRecoveringCode(UserEntity entity);
        Result ChangePassword(UserEntity entity);
        Result Delete(UserEntity entity);
        Result GetQuizzes(int userId, string type, bool complete);
        Result GetQuizzes(int userId);
        Result SaveQuiz(UsersQuizzesEntity entity);
        Result SaveQuiz(UsersQuizzesEntity quiz, Dictionary<int, object> questionsAnswers);
        Result UpdateDetails(UserEntity entity);
        Result ResetPassword(UserEntity entity);
    }
    public class UsersManager: IUsersManager
    {
        public Result Register(UserEntity entity)
        {
            Result result = null;
            try
            {
                if(entity.Password.Length < 6)
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
                    Entity = new {Email = entity.Email},
                    Query = @"SELECT Email from Users where Email = @Email and Active = 1"
                };
                var resultCheckEmail = con.ExecuteQuery<UserEntity>(queryCheckEmail);
                if (!resultCheckEmail.Success)
                {
                    resultCheckEmail.Entity = null;
                    resultCheckEmail.Success = false;
                    resultCheckEmail.Message = "An error occurred";
                    return resultCheckEmail;
                }
                var checkEmail = (IEnumerable<UserEntity>) resultCheckEmail.Entity;
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
                query.Query = @"INSERT INTO Users (Email, Name, Password, Hash, CreateDate, ModifyDate) VALUES(@Email, @Name, @Password, @Hash, @CreateDate, @ModifyDate)";

                result = con.InsertQuery(query);
                if (result.Success)
                {
                    entity.UserId = (int) result.Entity;
                    entity.Password = "";
                    entity.Hash = "";
                    entity.Token = credentials.GenerateUserToken(entity);
                    result.Entity = entity;

                    Task.Run(() => new EmailManager().SendEmail(entity.Email, DL.Models.EmailType.Welcome, new {
                        CurrentUserName = entity.Name,
                        CurrentUserEmail = entity.Email
                    }));

                }
                result.Message = result.Success ? "The user has been created" : "An error occurred";
               
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

        public Result Login(UserEntity entity)
        {
            Result result = null;
            try
            {
                var con = new DapperConnectionManager();
                var query = new QueryEntity();
                var credentials = new CredentialsManager();
                entity.Email = entity.Email.Trim().ToLower();
                query.Query = @"SELECT * FROM Users
                            where Email = @Email and Active = 1 and ApnaUser = 0";
                query.Entity = entity;
                result = con.ExecuteQuery<UserEntity>(query);

                if (!result.Success)
                {
                    result.Message = "Login error";
                    return result;
                }

                var r = (IEnumerable<UserEntity>)result.Entity;

                var user = r.FirstOrDefault();

                if (user == null)
                {
                    result.Message = "Invalid password or user";
                    result.Success = false;
                    result.Entity = null;
                    return result;
                }
                var password = credentials.EncodePassword(entity.Password, user.Hash);
                if (password == user.Password)
                {
                    user.Hash = null;
                    user.Password = null;
                    user.Token = credentials.GenerateUserToken(user);
                    result.Entity = user;
                    return result;
                }
                result.Entity = null;
                result.Message = "User not found";
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


        public Result LoginApna (UserEntity entity)
        {
            Result result = null;
            try
            {
                var con = new DapperConnectionManager();
                var query = new QueryEntity();
                var credentials = new CredentialsManager();
                entity.Email = entity.Email.Trim().ToLower();
                query.Query = @"SELECT * FROM Users
                            where Email = @Email and Active = 1 and ApnaUser = 1";
                query.Entity = entity;
                result = con.ExecuteQuery<UserEntity>(query);

                if (!result.Success)
                {
                    result.Message = "Login error";
                    return result;
                }

                var r = (IEnumerable<UserEntity>)result.Entity;

                var user = r.FirstOrDefault();

                if (user == null)
                {
                    var resultRegisterUser = RegisterUserApna(entity, con);
                    if (!resultRegisterUser.Success)
                    {
                        return resultRegisterUser;
                    }
                    user = (UserEntity)resultRegisterUser.Entity;
                }
                user.Token = credentials.GenerateUserToken(user);
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

        private Result RegisterUserApna(UserEntity entity, DapperConnectionManager con)
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
            query.Query = @"INSERT INTO Users (Email, Name, Password, Hash, CreateDate, ModifyDate, ApnaMemberId, Country, Suburb, State, PostalCode, ApnaUser) 
                            VALUES(@Email, @Name, @Password, @Hash, @CreateDate, @ModifyDate, @ApnaMemberId, @Country, @Suburb, @State, @PostalCode, @ApnaUser )";

            var result = con.InsertQuery(query);
            if (result.Success)
            {
                entity.UserId = (int)result.Entity;
                entity.Password = "";
                entity.Hash = "";
                entity.Token = credentials.GenerateUserToken(entity);
                result.Entity = entity;

                Task.Run(() => new EmailManager().SendEmail(entity.Email, DL.Models.EmailType.Welcome, new
                {
                    CurrentUserName = entity.Name,
                    CurrentUserEmail = entity.Email
                }));


            }
            return result;
        }

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

            var user = (UserEntity)result.Entity;



            query.Entity = new { UserId = user.UserId };
            query.Query = @"SELECT * FROM Users
                            where UserId = @UserId and Active = 1 and ApnaUser = 0";

            result = con.ExecuteQuery<UserEntity>(query);

            if (!result.Success)
            {
                result = null;
                result.Message = "Login error";
                return result;
            }

            var r = (IEnumerable<UserEntity>)result.Entity;

            user = r.FirstOrDefault();
            user.Password = null;
            user.Hash = null;
            result.Entity = user;
            return result;
        }

        public Result SaveQuiz(UsersQuizzesEntity quiz)
        {
            try
            {
                var con = new DapperConnectionManager();

                var query = new QueryEntity();
                query.Entity = new { UserQuizId = quiz.UserQuizId };
                query.Query = @"SELECT * FROM UsersQuizzes WHERE UserQuizId = @UserQuizId";
                var result = con.ExecuteQuery<UsersQuizzesEntity>(query);
                if (!result.Success)
                {
                    return result;
                }

                quiz.DateVal = DateTime.Now;
                quiz.Date = quiz.DateVal.ToString("d MMM yyyy");

                var q = ((IEnumerable<UsersQuizzesEntity>)result.Entity).FirstOrDefault();

                if (q != null)
                {
                    query.Entity = quiz;
                    query.Query = @"Update UsersQuizzes set Date = @Date , DateVal = @DateVal , Results = @Results , Completed = @Completed where UserQuizId = @UserQuizId";
                    result = con.ExecuteQuery(query);
                    return result;
                }
                
                query.Entity = quiz;
                query.Query = @"INSERT INTO UsersQuizzes (UserId ,QuizId ,Date, DateVal, Results ,Completed, Type) VALUES (@UserId ,@QuizId ,@Date, @DateVal ,@Results ,@Completed, @Type)";
                result = con.InsertQuery(query);
                return result;
            }catch(Exception ex)
            {
                Logger.Log(ex);
            }
            return new Result(false);
        }

        public Result SaveIsLookingForWork(int userId, bool isLookingForWork)
        {
            try
            {
                var con = new DapperConnectionManager();

                var query = new QueryEntity();

                query.Query = @"Update users
                            set isLookingForJobs = @isLookingForJobs
                            where UserId = @UserId";
                query.Entity = new
                {
                    UserId = userId,
                    isLookingForJobs = isLookingForWork
                };
                var result = con.ExecuteQuery<UserEntity>(query);
            }
            catch (Exception ex)
            {
                Logger.Log(ex);
            }
            return new Result(false);
        }

        public Result SaveMinReqSalaryk(int userId, int MinSalaryReq)
        {
            try
            {
                var con = new DapperConnectionManager();

                var query = new QueryEntity();

                query.Query = @"Update users
                            set MinSalaryExpectation = @MinSalaryExp
                            where UserId = @UserId";
                query.Entity = new
                {
                    UserId = userId,
                    MinSalaryExp = MinSalaryReq
                };
                var result = con.ExecuteQuery<UserEntity>(query);
            }
            catch (Exception ex)
            {
                Logger.Log(ex);
            }
            return new Result(false);
        }
        public Result SaveQuiz(UsersQuizzesEntity quiz, Dictionary<int, object> questionsAnswers)
        {
            try
            {
                var con = new DapperConnectionManager();

                var query = new QueryEntity();

                query.Query = @"SELECT * FROM Users
                            where UserId = @UserId";
                query.Entity = new { UserId = quiz.UserId };
                var result = con.ExecuteQuery<UserEntity>(query);

                if (!result.Success)
                {
                    result.Message = "An error occured";
                    return result;
                }

                var r = (IEnumerable<UserEntity>)result.Entity;

                var userEntity= r.FirstOrDefault();

                if(userEntity == null)
                {
                    result.Message = "An error occurred";
                    return result;
                }

                foreach (var item in questionsAnswers)
                {
                    query.Entity = new { QuestionId = item.Key };
                    query.Query = @"SELECT * FROM UserDataQuestions WHERE QuestionId = @QuestionId";
                    var resultUserData = con.ExecuteQuery<UserDataQuestionsEntity>(query);
                    if (!resultUserData.Success)
                    {
                        resultUserData.Message = "An error occurred";
                        return resultUserData;
                    }
                    var data = ((IEnumerable<UserDataQuestionsEntity>)resultUserData.Entity).FirstOrDefault();
                    if(data == null)
                    {
                        continue;
                    }

                    var itemData = data.FieldName.ToString();

                    switch (itemData)
                    {
                        case "NurseType":
                            userEntity.NurseType = item.Value.ToString();
                            break;
                        case "Area":
                            userEntity.Area = item.Value.ToString();
                            break;
                       
                        case "ActiveWorking":
                            userEntity.ActiveWorking = item.Value.ToString();
                            break;
                        case "PostalCode":
                            userEntity.PostalCode = item.Value.ToString();
                            break;
                        case "Address":
                            var adress = JsonConvert.DeserializeObject<AddressEntity>(item.Value.ToString());
                            userEntity.State = adress.State;
                            userEntity.PostalCode = adress.PostalCode;
                            userEntity.Country = adress.Country;
                            userEntity.Suburb = adress.Suburb;
                            break;
                        case "Patients":
                            userEntity.Patients = item.Value.ToString();
                            break;
                        case "PatientsTitle":
                            userEntity.PatientsTitle = item.Value.ToString();
                            break;
                        case "Qualification":
                            userEntity.Qualification = item.Value.ToString();
                            break;
                        case "Setting":
                            userEntity.Setting = item.Value.ToString();
                            break;
                        case "Age":
                            userEntity.Age = item.Value.ToString();
                            break;
                    }
                }
                using(var scope = new TransactionScope())
                {
                    query.Entity = new { UserQuizId = quiz.UserQuizId };
                    query.Query = @"SELECT * FROM UsersQuizzes WHERE UserQuizId = @UserQuizId";
                    result = con.ExecuteQueryUnScoped<UsersQuizzesEntity>(query);
                    if (!result.Success)
                    {
                        return result;
                    }

                    quiz.DateVal = DateTime.Now;
                    quiz.Date = quiz.DateVal.ToString("d MMM yyyy");

                    var q = ((IEnumerable<UsersQuizzesEntity>)result.Entity).FirstOrDefault();

                    if (q != null)
                    {
                        query.Entity = quiz;
                        query.Query = @"Update UsersQuizzes set Date = @Date , DateVal = @DateVal , Results = @Results , Completed = @Completed, Survey = @Survey where UserQuizId = @UserQuizId";
                        result = con.ExecuteQueryUnScoped(query);
                    }
                    else
                    {
                        query.Entity = quiz;
                        query.Query = @"INSERT INTO UsersQuizzes (UserId ,QuizId ,Date, DateVal, Results ,Completed, Type, Survey) VALUES (@UserId ,@QuizId ,@Date, @DateVal ,@Results ,@Completed, @Type, @Survey)";
                        result = con.InsertQueryUnScoped(query);
                    }

                    var queryUser = new QueryEntity()
                    {
                        Entity = userEntity,
                        Query = @"UPDATE Users set NurseType = @NurseType, ActiveWorking = @ActiveWorking, Area = @Area, Age = @Age, Country = @Country, Suburb = @Suburb, PostalCode = @PostalCode, State = @State, Patients = @Patients, PatientsTitle =@PatientsTitle, Qualification = @Qualification, Setting = @Setting where UserId = @UserId "
                    };
                    result = con.ExecuteQueryUnScoped(queryUser);
                    result.Entity = null;
                    scope.Complete();
                    return result;
                }
                
            }
            catch (Exception ex)
            {
                Logger.Log(ex);
            }
            return new Result(false);
        }


        public Result GetQuizzes(int userId)
        {
            try
            {
                var con = new DapperConnectionManager();
                var query = new QueryEntity();
                query.Entity = new { UserId = userId } ;
                query.Query = @"SELECT * FROM UsersQuizzes WHERE UserId = @UserId";
                var result = con.ExecuteQuery<UsersQuizzesEntity>(query);
                return result;
            }
            catch (Exception ex)
            {
                Logger.Log(ex);
            }
            return new Result(false);
        }

        public Result GetQuizzes(int userId, string type, bool complete)
        {
            try
            {
                var con = new DapperConnectionManager();
                var query = new QueryEntity();
                query.Entity = new { UserId = userId, Type = type, Completed = complete };
                query.Query = @"SELECT * FROM UsersQuizzes WHERE UserId = @UserId and Type = @Type and Completed = @Completed";
                var result = con.ExecuteQuery<UsersQuizzesEntity>(query);
                return result;
            }
            catch (Exception ex)
            {
                Logger.Log(ex);
            }
            return new Result(false);
        }


        public Result GenerateRecoveringCode(UserEntity entity)
        {
            Result result = null;
            var con = new DapperConnectionManager();
            var query = new QueryEntity();
            var credentials = new CredentialsManager();
            entity.Email = entity.Email.Trim().ToLower();
            query.Query = @"SELECT * FROM Users
                            where Email = @Email and Active = 1 and ApnaUser = 0";
            query.Entity = entity;
            result = con.ExecuteQuery<UserEntity>(query);

            if (!result.Success)
            {
                result.Entity = null;
                result.Message = "An error occurred";
                return result;
            }

            var r = (IEnumerable<UserEntity>)result.Entity;

            var user = r.FirstOrDefault();

            if (user == null)
            {
                result.Entity = null;
                result.Message = "Invalid user";
                result.Success = false;
                return result;
            }
            var Token = credentials.GenerateRecoverPasswordToken(user);
            Token = HttpUtility.UrlEncode(Token);

            Task.Run(() => new EmailManager().SendEmail(entity.Email, DL.Models.EmailType.RecoverPassword, new {
                Token,
                CurrentUserName = user.Name,
                CurrentUserEmail = user.Email,
                WebsiteUrl = ConfigurationManager.AppSettings["mnf.website"],
                ContentUrl = ConfigurationManager.AppSettings["mnf.content"]
            }));

            result.Entity = null;
            result.Message = "An email has been sent with instructions for recovering your password";
            result.Success = true;

            return result;
        }


        public Result ResetPassword(UserEntity entity)
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
                var user = (UserEntity)result.Entity;

                var con = new DapperConnectionManager();
                var query = new QueryEntity();

                //GET the user to check password
                query.Query = @"SELECT * FROM Users
                            where UserId = @UserId and Active = 1";
                query.Entity = user;

                result = con.ExecuteQuery<UserEntity>(query);

                if (!result.Success)
                {
                    result.Message = "Validation error";
                    return result;
                }

                var r = (IEnumerable<UserEntity>)result.Entity;

                user = r.FirstOrDefault();

                if (user == null)
                {
                    result.Message = "Validation error";
                    result.Success = false;
                    return result;
                }
                
                var newHash = credentials.GenerateSalt();

                entity.Hash = newHash;
                entity.Password = credentials.EncodePassword(entity.Password, newHash);

                query.Query = @"Update Users Set Password = @Password, Hash = @Hash
                            where UserId = @UserId";
                query.Entity = new { UserId = user.UserId, Hash = entity.Hash, Password = entity.Password };

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

        public Result ChangePassword(UserEntity entity)
        {
            Result result = null;
            try
            {
                var credentials = new CredentialsManager();
                var con = new DapperConnectionManager();
                var query = new QueryEntity();

                //GET the user to check password
                query.Query = @"SELECT * FROM Users
                            where UserId = @UserId and Active = 1";
                query.Entity = entity;
                result = con.ExecuteQuery<UserEntity>(query);

                if (!result.Success)
                {
                    result.Entity = null;
                    result.Message = "Validation error";
                    return result;
                }

                var r = (IEnumerable<UserEntity>)result.Entity;

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
                
                query.Query = @"Update Users Set Password = @Password, Hash = @Hash
                            where UserId = @UserId";
                query.Entity = new { UserId = entity.UserId, Hash = entity.Hash, Password = entity.Password };

                result = con.ExecuteQuery<UserEntity>(query);
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

        public Result UpdateDetails(UserEntity entity)
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


                query.Query = @"SELECT count(*) as n FROM Users
                            where Email = @Email and Active = 1 and UserId <> @UserId";
                query.Entity = entity;
                result = con.ExecuteQuery(query);

                if (!result.Success)
                {
                    result.Entity = null;
                    result.Message = "Login error";
                    return result;
                }

                var countUsers =(int)(((IEnumerable<dynamic>)result.Entity).First().n);
                if (countUsers > 0)
                {
                    result.Entity = null;
                    result.Success = false;
                    result.Message = "Email already in use";
                }
                

                query.Query = @"Update Users Set Name = @Name, Email = @Email, minsalary = @Salary
                            where UserId = @UserId and Active = 1";
                query.Entity = new { UserId = entity.UserId, Email = entity.Email, Name = entity.Name, Salary = entity.Salary};
                result = con.ExecuteQuery<UserEntity>(query);
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


        public Result Delete(UserEntity entity)
        {
            var credentials = new CredentialsManager();
            var result = credentials.ValidateUserToken(entity.Token);
            if (!result.Success)
            {
                return result;
            }
            var user =(UserEntity)result.Entity;
            if(user.UserId != entity.UserId)
            {
                result.Message = "Forbidden operation";
                result.Success = false;
                return result;
            }

            var con = new DapperConnectionManager();
            var query = new QueryEntity();
            entity.Email = entity.Email.Trim().ToLower();
            query.Query = @"Update Users Set Active = 0
                            where UserId = @UserId";
            query.Entity = new { UserId = entity.UserId };

            result = con.ExecuteQuery<UserEntity>(query);
            result.Message = result.Success ? "The user has been deleted" : "An error has occurred";
            return result;
        }
    }
}
