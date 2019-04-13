using System.Collections.Generic;
using System.Web.Mvc;
using AutoMapper;
using MyNursingFuture.BL.Entities;
using MyNursingFuture.BL.Managers;
using MyNursingFuture.Cms.Filters;
using MyNursingFuture.Cms.Models;

namespace MyNursingFuture.Cms.Controllers
{
    [JwtAuthorized]
    [ExceptionNullFilter]
    public class EmailsController : EntityController<EmailEntity, EmailViewModel>
    {
        private readonly IEmailManager emailManager;


        public EmailsController(IEmailManager emailManager, IMapper mapper, ILogChangesManager logChangesManager) : base(logChangesManager, mapper, emailManager)
        {

        }

        // GET: Emails
        public override ActionResult Index()
        {
            ViewBag.Emails = Manager.Get().Entity as IEnumerable<EmailEntity>;
            return View();
        }
    }
}