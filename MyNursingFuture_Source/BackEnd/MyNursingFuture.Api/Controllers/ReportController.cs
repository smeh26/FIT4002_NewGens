using MyNursingFuture.Api.Filters;
using MyNursingFuture.Api.Infrastructure;
using MyNursingFuture.BL.Entities;
using MyNursingFuture.BL.Managers;
using MyNursingFuture.Util;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web.Hosting;
using System.Web.Http;
using System.Web.Http.Cors;

namespace MyNursingFuture.Api.Controllers
{
    [ExceptionFilter]
    public class ReportController : ApiController
    {
        private readonly IReportManager _reportManager;
        private readonly ICacheManager _cacheManager;
        public ReportController(IReportManager reportManager, ICacheManager cacheManager)
        {
            _reportManager = reportManager;
            _cacheManager = cacheManager;
        }

        // POST: api/Report
        [JwtAuthorized]
        [Route("api/report")]
        public async Task<HttpResponseMessage> Post([FromBody]string value)
        {
            object objuser = null;
            Request.Properties.TryGetValue("user", out objuser);
            var user = objuser as UserEntity;
            string reportJson;
            using (var contentStream = await Request.Content.ReadAsStreamAsync())
            {
                contentStream.Seek(0, SeekOrigin.Begin);
                using (var sr = new StreamReader(contentStream))
                {
                    reportJson = sr.ReadToEnd();
                    var report = await SendReport(reportJson, user);
                    return Request.CreateResponse(HttpStatusCode.OK, report);
                }
            }
            return Request.CreateResponse(HttpStatusCode.OK, new Result(true));
        }

        [Route("api/report/anonymous")]
        public async Task<HttpResponseMessage> AnonReport([FromBody]object value)
        {
            string reportJson;
            try
            {


                //using (var contentStream = await Request.Content.ReadAsStreamAsync())
                //{
                //contentStream.Seek(0, SeekOrigin.Begin);
                //using (var sr = new StreamReader(contentStream))
                //{
                reportJson = value.ToString();
                var dinamicJson = JsonConvert.DeserializeObject<RawQuizDataEntity>(value.ToString());
                var email = dinamicJson.email?.ToString();
                var name = dinamicJson.name?.ToString();
                var saveOnly = dinamicJson.saveOnly;


                var report = await SendReportAnon(reportJson, email, name, saveOnly);
                if (report != "True")
                {
                    return Request.CreateResponse<HttpError>(HttpStatusCode.InternalServerError, new HttpError(report));
                }
                //}
                //}
                return Request.CreateResponse(HttpStatusCode.OK, new Result(true));
            }
            catch (Exception e)
            {
                return Request.CreateResponse<HttpError>(HttpStatusCode.InternalServerError, new HttpError("Internal Server Error. Reference: Server error"));
            }
        }

        [Route("api/report/saveanoncareerreport")]
        public async Task<HttpResponseMessage> AnonCareer([FromBody]string value)
        {
            string reportJson;
            try
            {


                using (var contentStream = await Request.Content.ReadAsStreamAsync())
                {
                    contentStream.Seek(0, SeekOrigin.Begin);
                    using (var sr = new StreamReader(contentStream))
                    {
                        reportJson = sr.ReadToEnd();
                        //var dinamicJson = JsonConvert.DeserializeObject<UserEntity>(reportJson);
                        //var email = dinamicJson.Email.ToString();
                        //var name = dinamicJson.Name.ToString();
                        var saved = CareerAnon(reportJson);
                        if (saved != "True")
                        {
                            return Request.CreateResponse<HttpError>(HttpStatusCode.InternalServerError, new HttpError(saved));
                        }
                    }
                }
                return Request.CreateResponse(HttpStatusCode.OK, new Result(true));
            }
            catch (Exception e)
            {
                return Request.CreateResponse<HttpError>(HttpStatusCode.InternalServerError, new HttpError("Stack Trace: " + e.StackTrace + " || Inner Exception: " + e.InnerException));
            }
        }



        [Route("api/report/download")]
        public async Task<HttpResponseMessage> Download([FromBody]string value)
        {

            string reportJson;
            using (var contentStream = await Request.Content.ReadAsStreamAsync())
            {
                contentStream.Seek(0, SeekOrigin.Begin);
                using (var sr = new StreamReader(contentStream))
                {
                    reportJson = sr.ReadToEnd();
                    var dinamicJson = JsonConvert.DeserializeObject<UserEntity>(reportJson);
                    var url = ConfigurationManager.AppSettings["report.url"];
                    using (var request = new HttpRequestMessage(HttpMethod.Post, url))
                    {
                        request.Content = new StringContent(reportJson, Encoding.UTF8, "application/json"); ;
                        using (Stream stream = await (await HttpClientManager.Client.SendAsync(request)).Content.ReadAsStreamAsync())
                        {
                            var memoryStream = new MemoryStream();
                            stream.CopyTo(memoryStream);
                            var result = new HttpResponseMessage(HttpStatusCode.OK)
                            {
                                Content = new ByteArrayContent(memoryStream.GetBuffer())
                            };
                            result.Content.Headers.ContentDisposition =
                                new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment")
                                {
                                    FileName = "Report.pdf"
                                };
                            result.Content.Headers.ContentType =
                                new MediaTypeHeaderValue("application/octet-stream");

                            return result;
                        }
                    }
                }
            }
        }

        private async Task<Result> SendReport(string content, UserEntity user)
        {
            try
            {

                var url = ConfigurationManager.AppSettings["report.url"];
                using (var request = new HttpRequestMessage(HttpMethod.Post, url))
                {
                    request.Content = new StringContent(content, Encoding.UTF8, "application/json"); ;
                    using (Stream contentStream = await (await HttpClientManager.Client.SendAsync(request)).Content.ReadAsStreamAsync())
                    {

                        var memoryStream = new MemoryStream();
                        contentStream.CopyTo(memoryStream);
                        return await _reportManager.SendReport(user, memoryStream);
                    }
                }

            }
            catch (Exception ex)
            {
                Logger.Log(ex);
                var result = new Result();

                return new Result(false);
            }

            return new Result(true);
        }





        private string CareerAnon(string content)
        {
            try
            {
                var saved = _reportManager.SaveAnonCareerReport(content);
                if (saved.Success)
                {
                    return "True";
                }
                else
                {
                    return content;
                }
            }
            catch (Exception ex)
            {
                Logger.Log(ex);
                return ex.StackTrace + " || Inner exception: " + ex.InnerException + " || Content: " + content;
            }
        }

        private async Task<string> SendReportAnon(string content, string email, string name, bool? saveOnly)
        {
            try
            {
                var url = ConfigurationManager.AppSettings["report.url"];
                using (var request = new HttpRequestMessage(HttpMethod.Post, url))
                {
                    
                        try
                        {
                            if (saveOnly.HasValue && saveOnly == true)
                            {
                                var saved = _reportManager.SaveAnonReport(content, email, name);
                                if (saved.Success)
                                {
                                    return "True";
                                }
                                else
                                {
                                    
                                    
                                    if(saved.Message == "THROWN")
                                    {
                                        var e = saved.Entity as Exception;
                                        return "Crashed at save " + e.StackTrace + " || Parameters Passed: " + email + " | " + name + " | " + saveOnly + " ||Inner exception: " + e.InnerException + " || Content: " + content;
                                    }
                                    else
                                    {
                                        return "Some other error: Message = " + saved.Message + " || Entity = " + saved.Entity + " || Content :" + content;
                                    }
                                    
                                    //return content;
                                }
                            }
                            else
                            {
                                Result sent = new Result(false);
                                using (var sendRequest = new HttpRequestMessage(HttpMethod.Post, url))
                                {
                                    sendRequest.Content = new StringContent(content, Encoding.UTF8, "application/json"); ;
                                    using (Stream sendStream = await (await HttpClientManager.Client.SendAsync(sendRequest)).Content.ReadAsStreamAsync())
                                    {

                                        var sendMemoryStream = new MemoryStream();
                                        sendStream.CopyTo(sendMemoryStream);
                                        var tempUser = new UserEntity();
                                        tempUser.Name = name;
                                        tempUser.Email = email;
                                        sent = await _reportManager.SendReport(tempUser, sendMemoryStream);
                                    }
                                }
                                
                                
                                var saved = _reportManager.SaveAnonReport(content, email, name);
                                if (sent.Success && saved.Success)
                                {
                                    return "True";
                                }
                                else
                                {
                                    return content;
                                }
                            }


                        }
                        catch (Exception ex)
                        {
                            Logger.Log(ex);
                            return "Made it past line 162" + ex.StackTrace + " || Parameters Passed: "+ email +" | "+ name +" | "+ saveOnly+" ||Inner exception: " + ex.InnerException + " || Content: " + content;
                        }
                    
                }

            }
            catch (Exception ex)
            {
                Logger.Log(ex);
                return ex.StackTrace + " || Inner exception: " + ex.InnerException + " || Content: " + content;
            }
        }
    }
}
