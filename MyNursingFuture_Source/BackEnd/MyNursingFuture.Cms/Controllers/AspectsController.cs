using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using MyNursingFuture.BL.Entities;
using MyNursingFuture.BL.Managers;
using MyNursingFuture.Cms.Filters;
using MyNursingFuture.Cms.Models;
using Newtonsoft.Json;
using MyNursingFuture.Util;

namespace MyNursingFuture.Cms.Controllers
{
    [JwtAuthorized]
    [ExceptionNullFilter]
    public class AspectsController : BaseController
    {
        private readonly IAspectsManager _aspectsManager;
        private readonly IDomainsManager _domainsManager;
        private readonly IActionsManager _actionsManager;
        private readonly IMapper _mapper;
        private readonly IEnumManager _enumManager;

        public AspectsController(IAspectsManager aspectsManager, IDomainsManager domainsManager, IActionsManager actionsManager, IMapper mapper, IEnumManager enumManager, ILogChangesManager logChangesManager) : base(logChangesManager)
        {
            _aspectsManager = aspectsManager;
            _actionsManager = actionsManager;
            _domainsManager = domainsManager;
            _mapper = mapper;
            _enumManager = enumManager;
        }

        // GET: Aspects
        public ActionResult Index(int page = 1)
        {
            var result = _aspectsManager.Get();
            var list = (IEnumerable<AspectEntity>) result.Entity;
            ViewBag.Aspects = list.OrderBy(x=>x.Framework).ThenBy(x=>x.Title).ThenBy(x=>x.DomainName);
            return View();
        }

        // GET: Aspects
        public ActionResult Insert()
        {
            var model = new AspectViewModel();
            model.Operation = "I";
            var resultD = _domainsManager.Get();
            ViewBag.Domains = (IEnumerable<DomainEntity>)resultD.Entity;

            var listTypes = new List<SelectListItem>();
            var listTypesStrings = _enumManager.GetActionsTypes();
            foreach (var item in listTypesStrings)
            {
                var sItem = new SelectListItem
                {
                    Text = item,
                    Value = item
                };
                listTypes.Add(sItem);
            }

            ViewBag.ListTypes = listTypes;


            return View("InsertEdit", model);
        }

        // GET: Aspects
        public ActionResult Edit(int id)
        {
            var result = _aspectsManager.Get(id);
            if (!result.Success)
            {
                TempData["Error"] = "Aspect of practice not found";
                return RedirectToAction("Index");
            }
            var entity = (AspectEntity)result.Entity;

            var model = _mapper.Map<AspectEntity, AspectViewModel>(entity);
            model.Operation = "E";
            var resultD = _domainsManager.Get();
            ViewBag.Domains = (IEnumerable<DomainEntity>)resultD.Entity;
            var listTypes = new List<SelectListItem>();
            var listTypesStrings = _enumManager.GetActionsTypes();
            foreach (var item in listTypesStrings)
            {
                var sItem = new SelectListItem
                {
                    Text = item,
                    Value = item
                };
                listTypes.Add(sItem);
            }
            ViewBag.ListTypes = listTypes;
            return View("InsertEdit", model);
        }

        // GET: Aspects
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult InsertEdit(AspectViewModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = "Transaction error";
                return RedirectToAction("Index");
            }
            model.ActionsList = !string.IsNullOrEmpty(model.ActionsListJson)? JsonConvert.DeserializeObject<List<ActionEntity>>(model.ActionsListJson) : new List<ActionEntity>();
            var entity = _mapper.Map<AspectViewModel, AspectEntity>(model);
            var result = model.Operation == "E" ? _aspectsManager.Update(entity) : _aspectsManager.Insert(entity);
            TempData["Result"] = result;
            if (!result.Success)
            {
                return RedirectToAction("Index");
            }
            var operation = model.Operation == "E" ? "Edit Aspect" : "Insert Aspect";
            StoreLog("Aspects", operation, (int)result.Entity);
            return RedirectToAction("Edit", new { id = (int)result.Entity });
        }
        [HttpPost]
        public JsonResult GetAction(string name)
        {
            var result = _actionsManager.GetLike(name);
            return Json(result);
        }

        [HttpGet]
        public ActionResult SetPublished(int id)
        {
            var result = _aspectsManager.SetPublished(id);

            if (result.Success)
            {
                StoreLog("Aspects", "Publish", id);
            }

            TempData["Result"] = result;
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            var result = _aspectsManager.Delete(id);

            if (result.Success)
            {
                StoreLog("Articles", "DELETE", id);
            }

            TempData["Result"] = result;
            return RedirectToAction("Index");
        }

        public ActionResult UpdatePosition(int questionId, int position)
        {
            Result result = _aspectsManager.UpdatePosition(questionId, position);

            return Json(result);
        }
    }
}