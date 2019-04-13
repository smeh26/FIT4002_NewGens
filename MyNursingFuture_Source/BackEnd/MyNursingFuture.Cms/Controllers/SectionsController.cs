using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
    public class SectionsController : EntityController<SectionEntity, SectionViewModel>
    {
        private readonly IEnumManager _enumManager;
        public SectionsController(ISectionsManager sectionManager, IEnumManager enumManager, IMapper mapper, ILogChangesManager logChangesManager) : base(logChangesManager, mapper, sectionManager)
        {
            _enumManager = enumManager;
        }

        // GET: Section
        public override ActionResult Index()
        {
            var result = Manager.Get();
            ViewBag.Sections = result.Entity as List<SectionEntity>;
            return View("Index");
        }

        public override ActionResult Insert()
        {
            ViewBag.ContentItemTypes = _enumManager.GetContentItemList();
            return base.Insert();
        }
    
        
    }
}