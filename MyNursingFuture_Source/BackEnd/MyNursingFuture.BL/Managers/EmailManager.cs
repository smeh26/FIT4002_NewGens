using MyNursingFuture.BL.Entities;
using MyNursingFuture.DL;
using MyNursingFuture.DL.Models;
using MyNursingFuture.Util;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using RazorEngine;
using RazorEngine.Templating;

namespace MyNursingFuture.BL.Managers
{
    public interface IEmailManager : IManager<EmailEntity>
    {
        SmtpClient MailClient { get; }
    }
    public class EmailManager : IEmailManager
    {
        public SmtpClient MailClient { get; private set; }

        public EmailManager()
        {
            try
            {
                MailClient = new SmtpClient()
                {
                    EnableSsl = Convert.ToBoolean(ConfigurationManager.AppSettings["configuration.email.ssl"] ?? "False"),
                    Host = ConfigurationManager.AppSettings["configuration.email.smtpServer"],
                    Port = Convert.ToInt16(ConfigurationManager.AppSettings["configuration.email.port"] ?? "25"),
                    Credentials = new NetworkCredential(ConfigurationManager.AppSettings["configuration.email.user"],
                                                        ConfigurationManager.AppSettings["configuration.email.password"])
                };

                var emailLayoutPath = System.Web.Hosting.HostingEnvironment.MapPath("~/~/CMS/Views/Emails/EmailLayout.cshtml");
                var emailLayoutContent = File.ReadAllText(emailLayoutPath, System.Text.Encoding.UTF8);

                var config = new RazorEngine.Configuration.TemplateServiceConfiguration() {
                    AllowMissingPropertiesOnDynamic = true
                };

                Engine.Razor = RazorEngineService.Create(config);

                Engine.Razor.AddTemplate("EmailLayout", emailLayoutContent);

            }
            catch (Exception ex)
            {
                Logger.Log(ex);
            }
        }

        public Result Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Result Get()
        {
            var con = new DapperConnectionManager();
            var query = new QueryEntity
            {
                Entity = new { },
                Query = @"SELECT * FROM Emails ORDER BY Type ASC"
            };
            return con.ExecuteQuery<EmailEntity>(query);
        }

        public Result Get(int id)
        {
            var con = new DapperConnectionManager();
            var query = new QueryEntity();

            query.Query = @"SELECT * FROM Emails
                            where EmailId = @EmailId";
            query.Entity = new { EmailId = id };

            var result = con.ExecuteQuery<EmailEntity>(query);

            if (!result.Success)
            {
                result.Message = "Email not found";
                return result;
            }

            result.Entity = ((IEnumerable<EmailEntity>)result.Entity).FirstOrDefault();

            return result;
        }

        public Result RenderEmail(EmailEntity email, object data)
        {
            var result = new Result();
            try
            {
                var emailLayoutPath = System.Web.Hosting.HostingEnvironment.MapPath("~/../CMS/Views/Emails/EmailLayout.cshtml");
                var emailLayoutContent = File.ReadAllText(emailLayoutPath, System.Text.Encoding.UTF8);

                Engine.Razor.AddTemplate("EmailLayout", emailLayoutContent);


                result.Message =  Engine.Razor.RunCompile($@"@{{ Layout = ""EmailLayout""; }}{email.Body}", $"Email{email.Type}", null, data);
                result.Success = true;
                return result;
            }
            catch(Exception e)
            {
                result.Success = false;
                result.Message = "Render Fail ||| Data:"+ e.Data + "||| Message: "+e.Message+" ||| Source: " +e.Source+"  ||| Inner Exception : " + e.InnerException + " ||| Stack Trace: " + e.StackTrace ;
                return result;
            }
        }

        public Result Insert(EmailEntity entity)
        {
            var con = new DapperConnectionManager();
            var query = new QueryEntity();

            query.Entity = entity;
            query.Query = @"INSERT INTO Emails (Type, Title, Body) 
                                             VALUES(@Type, @Title, @Body)";

            return con.InsertQuery(query);
        }

        public Result SetPublished(int id, bool published = true)
        {
            throw new NotImplementedException();
        }

        public Result Update(EmailEntity entity)
        {
            var con = new DapperConnectionManager();

            var query = new QueryEntity();
            query.Entity = entity;
            query.Query = @"UPDATE Emails set 
                                      Title=@Title,
                                      Type = @Type,
                                      Body = @Body
                                      WHERE EmailId = @EmailId";
            var result = con.ExecuteQuery(query);
            result.Message = result.Success ? "The email has been updated" : "An error occurred";
            result.Entity = entity.EmailId;
            return result;
        }
        public Result SendEmail(string to, EmailType type, object model, IEnumerable<Attachment> attachments = null)
        {
            return SendEmail(null, new string[] { to }, type, model, attachments);
        }
        public Result SendEmail(IEnumerable<string> tos, EmailType type, object model, IEnumerable<Attachment> attachments = null)
        {
            return SendEmail(null, tos, type, model, attachments);
        }

        public Result SendEmail(string from, string to, EmailType type, object model, IEnumerable<Attachment> attachments = null)
        {
            return SendEmail(from, new string[] { to }, type, model, attachments);
        }

        public Result SendEmail(string from, IEnumerable<string> tos, EmailType type, object model, IEnumerable<Attachment> attachments = null)
        {
            EmailEntity entity = new EmailEntity();
            try
            {
                var message = new MailMessage()
                {
                    From = new MailAddress(ConfigurationManager.AppSettings["configuration.email.from"], ConfigurationManager.AppSettings["configuration.email.fromName"] ?? ConfigurationManager.AppSettings["configuration.email.from"]),
                    IsBodyHtml = true,
                };

                foreach (var to in tos)
                {
                    message.To.Add(to);
                }

                if (attachments != null)
                {
                    foreach (var attachment in attachments)
                    {
                        message.Attachments.Add(attachment);
                    }
                }

                var con = new DapperConnectionManager();
                var query = new QueryEntity();

                query.Query = @"SELECT * FROM Emails
                            where Type = @Type";
                query.Entity = new { Type = type.ToString() };

                var result = con.ExecuteQuery<EmailEntity>(query);

                if (!result.Success)
                {
                    return result;
                }

                entity = ((IEnumerable<EmailEntity>)result.Entity).FirstOrDefault();


                
                

                dynamic renderModel = new ExpandoObject();

                AddProperty(renderModel, "SiteUrl", ConfigurationManager.AppSettings["mnf.website"] ?? System.Web.HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority));
                AddProperty(renderModel, "AssetUrl", string.Join("/", ConfigurationManager.AppSettings["mnf.content"] ?? System.Web.HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority), "assets"));

                AddProperty(renderModel, "EmailType", entity.Type);

                AddProperty(renderModel, "EmailTo", string.Join(";", tos.FirstOrDefault()));
                AddProperty(renderModel, "EmailFrom", message.From);

                var currentUserToken = System.Web.HttpContext.Current?.Request?.Cookies?["MNFCMS"]?.Value ?? System.Web.HttpContext.Current?.Request?.Headers?["Authorization"] ?? string.Empty;

                var credentialsManager = new CredentialsManager();
                var currentUserResult = credentialsManager.ValidateUserToken(currentUserToken);
                if (!currentUserResult.Success)
                {
                    currentUserResult = credentialsManager.ValidateAdminToken(currentUserToken);
                }

                if (currentUserResult.Success)
                {
                    AddProperty(renderModel, "UserId", (currentUserResult.Entity as UserEntity)?.UserId ?? (currentUserResult.Entity as AdministratorEntity)?.AdministratorId);
                    AddProperty(renderModel, "UserName", (currentUserResult.Entity as UserEntity)?.Name ?? (currentUserResult.Entity as AdministratorEntity)?.Name ?? (currentUserResult.Entity as AdministratorEntity)?.Username);
                    AddProperty(renderModel, "UserEmail", (currentUserResult.Entity as UserEntity)?.Email);
                }
                

                foreach (var prop in model.GetType().GetProperties())
                {
                    AddProperty(renderModel, prop.Name, prop.GetValue(model));
                }

                var renderResult = RenderEmail(entity, renderModel) as Result;

                if (renderResult.Success)
                {
                    message.Body = renderResult.Message;
                    message.Subject = entity.Title;

                    foreach (var prop in renderModel as IDictionary<string, object>)
                    {
                        message.Subject = message.Subject.Replace($"@Model.{prop.Key}", prop.Value?.ToString());
                    }

                    MailClient.Send(message);
                }
                else
                {
                    //renderResult.Message += " ||| UserId: " + renderModel["UserId"] + " ||| UserName: " + renderModel["UserName"] + "  ||| UserEmail: " + renderModel["UserEmail"] ;

                    foreach(var prop in renderModel as IDictionary<string, object>)
                    {
                        renderResult.Message += " ||| " + prop.Key + prop.Value?.ToString();
                    }

                    return renderResult;
                }
            }
            catch (Exception e)
            {
                Logger.Log(e);
                var res = new Result();
                res.Entity = e;
                res.Message = "Failed in SendMail - Email Manager ||| " + e.InnerException + " ||| " + e.StackTrace + " ||| BODY: " + entity.Body + " ||| TITLE: " + entity.Title + " ||| TYPE: " + entity.Type + " ||| EMAILID: " + entity.EmailId;
                res.Success = false;
                return res;
            }

            return new Result(true);
        }

        public bool SendEmail(string toList, string body, string subject, MemoryStream ms = null, string fileName = null)
        {
            try
            {
                var message = new MailMessage();
                message.Subject = subject;
                message.IsBodyHtml = true;
                message.From = new MailAddress(ConfigurationManager.AppSettings["configuration.email.from"]);
                message.Body = body;
                if (ms != null)
                {
                    //System.Net.Mime.ContentType ct = new System.Net.Mime.ContentType(System.Net.Mime.MediaTypeNames.Application.Pdf);
                    //System.Net.Mail.Attachment attach = new System.Net.Mail.Attachment(ms, ct);
                    //attach.ContentDisposition.FileName = fileName;
                    ms.Position = 0;
                    message.Attachments.Add(new Attachment(ms, fileName));
                }


                var listEmail = toList.Split(';').ToList();
                if (listEmail.Any())
                {
                    foreach (var itemEmail in listEmail)
                    {
                        message.To.Add(itemEmail.Trim());
                    }
                }

                MailClient.Send(message);
            }
            catch (Exception ex)
            {
                Logger.Log(ex);
                return false;
            }
            if (ms != null)
            {
                ms.Dispose();
            }
            return true;
        }

            public static void AddProperty(ExpandoObject expando, string propertyName, object propertyValue)
            {
                // ExpandoObject supports IDictionary so we can extend it like this
                var expandoDict = expando as IDictionary<string, object>;
                if (expandoDict.ContainsKey(propertyName))
                    expandoDict[propertyName] = propertyValue;
                else
                    expandoDict.Add(propertyName, propertyValue);
            }

    }
}
