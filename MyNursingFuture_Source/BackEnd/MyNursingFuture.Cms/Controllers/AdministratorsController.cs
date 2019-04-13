using AutoMapper;
using MyNursingFuture.BL.Entities;
using MyNursingFuture.BL.Managers;
using MyNursingFuture.Cms.Filters;
using MyNursingFuture.Cms.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyNursingFuture.Cms.Controllers
{
    [JwtAuthorized]
    [ExceptionNullFilter]
    public class AdministratorsController : BaseController
    {
        private readonly IAdministratorsManager _administratorManager;
        private readonly IMapper _mapper;
        public AdministratorsController(IAdministratorsManager administratorsManager, IMapper mapper, ILogChangesManager logChangesManager) : base(logChangesManager)
        {
            _administratorManager = administratorsManager;
            _mapper = mapper;
        }
        // GET: Administrators
        public ActionResult Index()
        {
            ViewBag.Administrators = _administratorManager.Get().Entity;
            return View("Index");
        }

        public ActionResult Insert()
        {
            var model = new AdministratorViewModel();
            model.Operation = "I";
            return View("InsertEdit", model);
        }

        public ActionResult Edit(int id)
        {
            var result = _administratorManager.Get(id);
            if (!result.Success)
            {
                TempData["Result"] = result;
                return RedirectToAction("Index");
            }
            var entity = result.Entity as AdministratorEntity;
            var model = _mapper.Map<AdministratorEntity, AdministratorViewModel>(entity);
            model.Operation = "E";
            model.Password = null;
            return View("InsertEdit", model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult InsertEdit(AdministratorViewModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["Error"] = "Transaction error";
                return RedirectToAction("Index", "Sectors");
            }
            var entity = _mapper.Map<AdministratorViewModel, AdministratorEntity>(model);
            var result = model.Operation == "E" ? _administratorManager.Update(entity) : _administratorManager.Insert(entity);
            TempData["Result"] = result;
            if (!result.Success)
            {
                return RedirectToAction("Index");
            }
            var operation = model.Operation == "E" ? "Edit Administrator" : "Insert Administrator";
            StoreLog("Administrators", operation, (int)result.Entity);
            return RedirectToAction("Edit", new { id = (int)result.Entity });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            var result = _administratorManager.Delete(id);

            if (result.Success)
            {
                StoreLog("Administrators", "DELETE", id);
            }

            TempData["Result"] = result;
            return RedirectToAction("Index");
        }
    }
}