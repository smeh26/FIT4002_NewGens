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
    public class SectorsController : EntityController<SectorEntity, SectorViewModel>
    {
        private readonly ISectorsManager _sectorsManager;


        public SectorsController(ISectorsManager sectorsManager, IMapper mapper, ILogChangesManager logChangesManager) : base(logChangesManager, mapper, sectorsManager)
        {

        }

        // GET: Sectors
        public override ActionResult Index()
        {
            ViewBag.Sectors = Manager.Get().Entity as IEnumerable<SectorEntity>;
            return View();
        }
        
    }
}