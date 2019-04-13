using MyNursingFuture.DL;
using MyNursingFuture.Util;
using MyNursingFuture.BL.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.ComponentModel;
using MyNursingFuture.BL.Managers;

namespace MyNursingFuture.BL.Managers
{
    public interface IReportManager
    {
        Task<Result> SendReport(UserEntity user, MemoryStream data);
        Task<Result> SendReport(MemoryStream stream, string email, string name);
        Result SaveAnonReport(string rawData, string email, string name);
        Result SaveAnonCareerReport(string rawData);
    }
    public class ReportManager : IReportManager
    {
        public async Task<Result> SendReport(UserEntity user, MemoryStream data)
        {
            data.Position = 0;
            var result = new Result();
            try
            {
                var sendResult = Task.Run(() => new EmailManager().SendEmail(user.Name, user.Email, DL.Models.EmailType.Report, new
                {
                    CurrentUserName = user.Name,
                    CurrentUserEmail = user.Email,
                    Title = "My Nursing Future - Report"
                }, new System.Net.Mail.Attachment[] {
                    new System.Net.Mail.Attachment(data, "report.pdf")
                }));
                result = await sendResult;
                return result;

            }
            catch (Exception ex)
            {
                Logger.Log(ex);
                result.Entity = ex;
                result.Success = false;
                result.Message = "Failed at email send";
                return result;
            }
        }
        /// <summary>
        /// Send out anon report. Save anon report to db.
        /// </summary>
        /// <param name="data">Json data being sent</param>
        /// <param name="email">Anon user email</param>
        /// <param name="name">Anon user name</param>
        /// <returns></returns>
        public async Task<Result> SendReport(MemoryStream data, string email, string name)
        {
            var result = new Result();
            try
            {
                var sendResult = Task.Run(() => new EmailManager().SendEmail(name, email, DL.Models.EmailType.Report, new
                {
                    CurrentUserName = name,
                    CurrentUserEmail = email,
                    Title = "My Nursing Future - Report"
                }, new System.Net.Mail.Attachment[] {
                    new System.Net.Mail.Attachment(data, "report.pdf")
                }));
                result = await sendResult;
                return result;

            }
            catch (Exception ex)
            {
                Logger.Log(ex);
                result.Entity = ex;
                result.Success = false;
                result.Message = "Failed at email send";
                return result;
            }
        }      

        public Result SaveAnonReport(string data, string email, string name)
        {

            try
            {


                var rawData = JsonConvert.DeserializeObject<RawQuizDataEntity>(data);

                var con = new DapperConnectionManager();
                var saveQuizQuery = new QueryEntity();
                var credentials = new CredentialsManager();
                saveQuizQuery.Query = @"INSERT INTO AnonUserQuizzes (Email, Name, QuizId, DateVal, Results ,Completed, Type, Date, NurseType, ActiveWorking, Area, Setting, Age, Country, Suburb, PostCode, State, PatientsTitle, Qualification) VALUES (";

                AnonQuizResults results = new AnonQuizResults()
                {
                    results = rawData.selfAssessmentResults,
                    answers = new Dictionary<string, float>()
                };

                foreach (var answer in rawData.aspects)
                {
                    foreach (var set in answer.Value)
                    {
                        results.answers.Add(set.aspectId.ToString(), set.answer);
                    }
                }

                string NurseType = rawData.aboutYouAnswers["32"].ToString();
                string ActiveWorking = rawData.aboutYouAnswers["67"].ToString();
                string Area = rawData.aboutYouAnswers["71"].ToString();
                string Setting = rawData.aboutYouAnswers["72"].ToString();
                string Age = rawData.aboutYouAnswers["73"].ToString();
                string Patients = rawData.aboutYouAnswers["76"].ToString();
                string Qualification = rawData.aboutYouAnswers["33"].ToString();

                var addressDetails = rawData.aboutYouAnswers["69"].ToDictionary();
                string Country = "";
                string Suburb = "";
                string Postcode = "";
                string State = "";

                foreach(var detail in addressDetails)
                {
                    switch(detail.Key.ToLower())
                    {
                        case ("country"):
                            Country = detail.Value.ToString();
                            break;
                        case ("suburb"):
                            Suburb = detail.Value.ToString();
                            break;
                        case ("postalcode"):
                            Postcode = detail.Value.ToString();
                            break;
                        case ("state"):
                            State = detail.Value.ToString();
                            break;
                        default:break;
                    }
                }

                saveQuizQuery.Query += "'" + rawData?.email + "', ";
                saveQuizQuery.Query += "'" + rawData?.name + "', ";
                saveQuizQuery.Query += 1 + ", ";
                saveQuizQuery.Query += "CAST('" + DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fff'Z'") + "' AS DATETIME), ";
                saveQuizQuery.Query += "'" + JsonConvert.SerializeObject(results) + "', ";
                saveQuizQuery.Query += 1 + ", ";
                saveQuizQuery.Query += "'ASSESSMENT'" + ", ";
                saveQuizQuery.Query += "'" + rawData.date + "'" + ", ";
                saveQuizQuery.Query += "'" + NurseType + "'" + ", ";
                saveQuizQuery.Query += "'" + ActiveWorking + "'" + ", ";
                saveQuizQuery.Query += "'" + Area + "'" + ", ";
                saveQuizQuery.Query += "'" + Setting + "'" + ", ";
                saveQuizQuery.Query += "'" + Age + "'" + ", ";
                saveQuizQuery.Query += "'" + Country + "'" + ", ";
                saveQuizQuery.Query += "'" + Suburb + "'" + ", ";
                saveQuizQuery.Query += "'" + Postcode + "'" + ", ";
                saveQuizQuery.Query += "'" + State + "'" + ", ";
                saveQuizQuery.Query += "'" + Patients + "'" + ", ";
                saveQuizQuery.Query += "'" + Qualification + "'" + ")";

                return con.ExecuteQuery<UserEntity>(saveQuizQuery);

            }
            catch(Exception e)
            {
                var res = new Result();
                res.Entity = e;
                res.Success = false;
                res.Message = "THROWN";
                return res;
            }

        }

        public Result SaveAnonCareerReport(string data)
        {

            try
            {
                data = data.Replace("careerPathwaysCurrentAnswers","answers");
                data = data.Replace("careerPathwaysResults", "results");


                var rawData = JsonConvert.DeserializeObject<RawCareerQuizDataEntity>(data);

                var con = new DapperConnectionManager();
                var saveQuizQuery = new QueryEntity();
                var credentials = new CredentialsManager();
                saveQuizQuery.Query = @"INSERT INTO AnonUserQuizzes (Email, Name, QuizId, DateVal, Results ,Completed, Type, Date, NurseType, ActiveWorking, Area, Setting, Age, Country, Suburb, PostCode, State, PatientsTitle, Qualification) VALUES (";

                string NurseType = rawData.aboutYouAnswers["32"].ToString();
                string ActiveWorking = rawData.aboutYouAnswers["67"].ToString();
                string Area = rawData.aboutYouAnswers["71"].ToString();
                string Setting = rawData.aboutYouAnswers["72"].ToString();
                string Age = rawData.aboutYouAnswers["73"].ToString();
                string Patients = rawData.aboutYouAnswers["76"].ToString();
                string Qualification = rawData.aboutYouAnswers["33"].ToString();

                var addressDetails = rawData.aboutYouAnswers["69"].ToDictionary();
                string Country = "";
                string Suburb = "";
                string Postcode = "";
                string State = "";

                foreach (var detail in addressDetails)
                {
                    switch (detail.Key.ToLower())
                    {
                        case ("country"):
                            Country = detail.Value.ToString();
                            break;
                        case ("suburb"):
                            Suburb = detail.Value.ToString();
                            break;
                        case ("postalcode"):
                            Postcode = detail.Value.ToString();
                            break;
                        case ("state"):
                            State = detail.Value.ToString();
                            break;
                        default: break;
                    }
                }


                var results = new CareerResults() {
                    results = rawData.results,
                    answers = rawData.answers
                };

                QuizResults newResult = new QuizResults();
                newResult.score = new Dictionary<string, float>();
                newResult.scorePositives = new Dictionary<string, List<string>>();
                newResult.scorePercentages = new Dictionary<string, int>();
                newResult.date = results.results.date;

                foreach(var score in results.results.score)
                {
                    if(int.Parse(score.Key) <= 10)
                    {
                        newResult.score.Add(score.Key, score.Value);
                    }
                }

                foreach (var scorePositive in results.results.scorePositives)
                {
                    if (int.Parse(scorePositive.Key) <= 10)
                    {
                        newResult.scorePositives.Add(scorePositive.Key, scorePositive.Value);
                    }
                }

                foreach (var scorePercentage in results.results.scorePercentages)
                {
                    if (int.Parse(scorePercentage.Key) <= 10)
                    {
                        newResult.scorePercentages.Add(scorePercentage.Key, scorePercentage.Value);
                    }
                }

                results.results = newResult;


                saveQuizQuery.Query += "'" + "anon@anon.com" + "', ";
                saveQuizQuery.Query += "'" + "anon" + "', ";
                saveQuizQuery.Query += 2 + ", ";
                saveQuizQuery.Query += "CAST('" + DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fff'Z'") + "' AS DATETIME), ";
                saveQuizQuery.Query += "'" + JsonConvert.SerializeObject(results) + "', ";
                saveQuizQuery.Query += 1 + ", ";
                saveQuizQuery.Query += "'PATHWAY'" + ", ";
                saveQuizQuery.Query += "'" + results.results.date + "'" + ", ";
                saveQuizQuery.Query += "'" + NurseType + "'" + ", ";
                saveQuizQuery.Query += "'" + ActiveWorking + "'" + ", ";
                saveQuizQuery.Query += "'" + Area + "'" + ", ";
                saveQuizQuery.Query += "'" + Setting + "'" + ", ";
                saveQuizQuery.Query += "'" + Age + "'" + ", ";
                saveQuizQuery.Query += "'" + Country + "'" + ", ";
                saveQuizQuery.Query += "'" + Suburb + "'" + ", ";
                saveQuizQuery.Query += "'" + Postcode + "'" + ", ";
                saveQuizQuery.Query += "'" + State + "'" + ", ";
                saveQuizQuery.Query += "'" + Patients + "'" + ", ";
                saveQuizQuery.Query += "'" + Qualification + "'" + ")";

                return con.ExecuteQuery<UserEntity>(saveQuizQuery);

            }
            catch (Exception e)
            {
                return null;
            }

        }
    }

    public static class ObjectToDictionaryHelper
    {
        public static IDictionary<string, object> ToDictionary(this object source)
        {
            return source.ToDictionary<object>();
        }

        public static IDictionary<string, T> ToDictionary<T>(this object source)
        {
            if (source == null)
                ThrowExceptionWhenSourceArgumentIsNull();

            var dictionary = new Dictionary<string, T>();
            foreach (PropertyDescriptor property in TypeDescriptor.GetProperties(source))
                AddPropertyToDictionary<T>(property, source, dictionary);
            return dictionary;
        }

        private static void AddPropertyToDictionary<T>(PropertyDescriptor property, object source, Dictionary<string, T> dictionary)
        {
            object value = property.GetValue(source);
            if (IsOfType<T>(value))
                dictionary.Add(property.Name, (T)value);
        }

        private static bool IsOfType<T>(object value)
        {
            return value is T;
        }

        private static void ThrowExceptionWhenSourceArgumentIsNull()
        {
            throw new ArgumentNullException("source", "Unable to convert object to a dictionary. The source object is null.");
        }
    }
}
