using MyNursingFuture.BL.Entities;
using MyNursingFuture.BL.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyNursingFuture.Cms.Controllers
{
    public class LogChangesController : Controller
    {
        private readonly ILogChangesManager _logChangesManager;
        public LogChangesController(ILogChangesManager logChangesManager)
        {
            _logChangesManager = logChangesManager;
        }
        // GET: Logchanges
        public ActionResult Index(int page = 1)
        {
            var result = _logChangesManager.Get(page);
            var lChanges = (LogChangeRows)result.Entity;
            ViewBag.RowCount = lChanges.Rows;
            ViewBag.Changes = lChanges.List;
            return View();
        }
    }
}