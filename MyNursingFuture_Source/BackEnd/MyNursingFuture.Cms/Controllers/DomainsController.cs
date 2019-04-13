using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web.Mvc;
using AutoMapper;
using MyNursingFuture.BL.Managers;
using MyNursingFuture.Cms.Models;
using MyNursingFuture.BL.Entities;
using MyNursingFuture.Cms.Filters;
using Newtonsoft.Json;
using MyNursingFuture.Util;

namespace MyNursingFuture.Cms.Controllers
{
    [JwtAuthorized]
    [ExceptionNullFilter]
    public class DomainsController : BaseController
    {
        private readonly IDomainsManager _domainsManager;
        private readonly IActionsManager _actionsManager;
        private readonly IMapper _mapper;
        public DomainsController(IDomainsManager domainsManager, IMapper mapper, IActionsManager actionsManager, ILogChangesManager logChangesManager) : base(logChangesManager)
        {
            _actionsManager = actionsManager;
            _domainsManager = domainsManager;
            _mapper = mapper;
        }
        // GET: Domain
        public ActionResult Index()
        {
            var result = _domainsManager.Get();
            ViewBag.Domains = (IEnumerable<DomainEntity>) result.Entity;
            return View("Index");
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var result = _domainsManager.Get(id);
            var entity = (DomainEntity)result.Entity;
            var model = _mapper.Map<DomainEntity, DomainViewModel>(entity);
            model.Operation = "E";
            return View("InsertEdit", model);
        }

        [HttpGet]
        public ActionResult Insert()
        {
            //check configuration if is possible to insert a new domain
            var canEdit = Boolean.Parse(ConfigurationManager.AppSettings["DomainEdition"]);
            if (!canEdit)
            {
                return RedirectToAction("Index");
            }
            var model = new DomainViewModel()
            {
                Operation = "I"
            };
            return View("InsertEdit", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult InsertEdit(DomainViewModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = "Transaction error";
                return RedirectToAction("Index");
            }

            //check configuration if is possible to insert a new domain
            if (model.Operation == "I")
            {
                var canEdit = Boolean.Parse(ConfigurationManager.AppSettings["DomainEdition"]);
                if (!canEdit)
                {
                    TempData["ErrorMessage"] = "Operation forbidden";
                    return RedirectToAction("Index");
                }
            }
            model.ActionsList = !string.IsNullOrEmpty(model.ActionsListJson) ? JsonConvert.DeserializeObject<List<ActionEntity>>(model.ActionsListJson) : new List<ActionEntity>();
            var entity = _mapper.Map<DomainViewModel, DomainEntity>(model);
            entity.ImagePath = Server.MapPath("~/Content/img/Uploads/");
            var result = model.Operation == "E" ? _domainsManager.Update(entity) : _domainsManager.Insert(entity);
            TempData["Result"] = result;
            if (!result.Success)
            {
                return RedirectToAction("Index");
            }
            var operation = model.Operation == "E" ? "Edit Domain" : "Insert Domain";
            StoreLog("Domains", operation, (int)result.Entity);
            return RedirectToAction("Edit", new{id = (int) result.Entity });
        }

        [HttpPost]
        public JsonResult GetAction(string name)
        {
            var result = _actionsManager.GetLike(name);
            return Json(result);
        }

        public ActionResult UpdatePosition(int questionId, int position)
        {
            Result result = _domainsManager.UpdatePosition(questionId, position);

            return Json(result);
        }
    }
}