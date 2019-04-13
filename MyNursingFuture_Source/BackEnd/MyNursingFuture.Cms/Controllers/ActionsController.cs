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
    public class ActionsController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly IActionsManager _actionsManager;
        private readonly IEnumManager _enumerationManager;
        public ActionsController(IActionsManager actionsManager, IMapper mapper, IEnumManager enumerationManager, ILogChangesManager logChangesManager):base(logChangesManager)
        {
            _mapper = mapper;
            _actionsManager = actionsManager;
            _enumerationManager = enumerationManager;
        }
        // GET: Actions
        public ActionResult Index()
        {
            ViewBag.Actions = _actionsManager.Get().Entity as IEnumerable<ActionEntity>;
            return View();
        }

        public ActionResult Insert()
        {
            var model =  new ActionViewModel();
            var listTypes = new List<SelectListItem>();

            var listTypesStrings = _enumerationManager.GetActionsTypes();

            foreach(var item in listTypesStrings)
            {
                var sItem = new SelectListItem
                {
                    Text = item,
                    Value = item
                };
                listTypes.Add(sItem);
            }

            ViewBag.ListTypes = listTypes;

            model.Operation = "I";
            return View("InsertEdit", model);
        }

        public ActionResult Edit(int id)
        {
            var result = _actionsManager.Get(id);
            if (!result.Success)
            {
                TempData["Result"] = result;
                return RedirectToAction("Index");
            }
            var entity = result.Entity as ActionEntity;
            var model = _mapper.Map<ActionEntity, ActionViewModel>(entity);

            var listTypes = new List<SelectListItem>();

            var listTypesStrings = _enumerationManager.GetActionsTypes();

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

            model.Operation = "E";
            return View("InsertEdit", model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult InsertEdit(ActionViewModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["Error"] = "Transaction error";
                return RedirectToAction("Index", "Sectors");
            }
            var entity = _mapper.Map<ActionViewModel, ActionEntity>(model);
            var result = model.Operation == "E" ? _actionsManager.Update(entity) : _actionsManager.Insert(entity);
            TempData["Result"] = result;
            if (!result.Success)
            {
                return RedirectToAction("Index");
            }
            var operation = model.Operation == "E" ? "Edit Action" : "Insert Action";
            StoreLog("Actions",operation, (int)result.Entity);

            return RedirectToAction("Edit", new { id = (int)result.Entity });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            var result = _actionsManager.Delete(id);

            if (result.Success)
            {
                StoreLog("Actions", "DELETE", id);
            }

            TempData["Result"] = result;

            return RedirectToAction("Index");
        }

        
    }
}