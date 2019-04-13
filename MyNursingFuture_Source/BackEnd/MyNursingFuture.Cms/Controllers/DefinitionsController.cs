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
    public class DefinitionsController : BaseController
    {
        private readonly IDefinitionsManager _definitionsManager;
        private readonly IMapper _mapper;

        public DefinitionsController(IDefinitionsManager definitionsManager, IMapper mapper, ILogChangesManager logChangesManager) : base(logChangesManager)
        {
            _definitionsManager = definitionsManager;
            _mapper = mapper;
        }
        
        public ActionResult Index()
        {
            ViewBag.Definitions = _definitionsManager.Get().Entity as IEnumerable<DefinitionEntity>;
            return View("Index");
        }

        public ActionResult Insert()
        {
            var model = new DefinitionViewModel();
            model.Operation = "I";
            return View("InsertEdit", model);
        }

        public ActionResult Edit(int id)
        {
            var result = _definitionsManager.Get(id);
            if (!result.Success)
            {
                TempData["Result"] = result;
                return RedirectToAction("Index");
            }
            var entity = result.Entity as DefinitionEntity;
            var model = _mapper.Map<DefinitionEntity, DefinitionViewModel>(entity);
            model.Operation = "E";
            return View("InsertEdit", model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult InsertEdit(DefinitionViewModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["Error"] = "Transaction error";
                return RedirectToAction("Index", "Sectors");
            }
            var entity = _mapper.Map<DefinitionViewModel, DefinitionEntity>(model);
            var result = model.Operation == "E" ? _definitionsManager.Update(entity) : _definitionsManager.Insert(entity);
            TempData["Result"] = result;
            if (!result.Success)
            {
                return RedirectToAction("Index");
            }
            var operation = model.Operation == "E" ? "Edit Defitinion" : "Insert Defitinion";
            StoreLog("Definition", operation, (int)result.Entity);

            return RedirectToAction("Edit", new { id = (int)result.Entity });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            var result = _definitionsManager.Delete(id);
            if (result.Success)
            {
                StoreLog("Definitions", "DELETE", id);
            }
            
            TempData["Result"] = result;
            return RedirectToAction("Index");
        }
    }
}