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
    public class RolesController : BaseController
    {
        private readonly IRolesManager _rolesManager;
        private readonly IMapper _mapper;

        public RolesController(IRolesManager rolesManager, IMapper mapper, ILogChangesManager logChangesManager) : base(logChangesManager)
        {
            _rolesManager = rolesManager;
            _mapper = mapper;
        }
        
        public ActionResult Index()
        {
            ViewBag.Roles = _rolesManager.Get().Entity as IEnumerable<RoleEntity>;
            return View("Index");
        }
        
        public ActionResult Insert()
        {
            var model = new RoleViewModel();
            model.Operation = "I";
            return View("InsertEdit", model);
        }

        public ActionResult Edit(int id)
        {
            var result = _rolesManager.Get(id);
            if (!result.Success)
            {
                TempData["Error"] = "Role not found";
                return RedirectToAction("Index");
            }

            var entity = (RoleEntity) result.Entity;

            var model = _mapper.Map<RoleEntity, RoleViewModel >(entity);
            model.Operation = "E";
            return View("InsertEdit", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult InsertEdit(RoleViewModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["Error"] = "Transaction error";
                return RedirectToAction("Index");
            }
            var entity = _mapper.Map<RoleViewModel, RoleEntity>(model);
            var result = model.Operation == "I" ? _rolesManager.Insert(entity) : _rolesManager.Update(entity);
            TempData["Result"] = result;
            if (!result.Success)
            {
                return RedirectToAction("Index");
            }

            var operation = model.Operation == "E" ? "Edit Role " : "Insert Role ";
            StoreLog("Roles", operation, (int)result.Entity);

            return RedirectToAction("Edit", new { id = (int)result.Entity });
        }

        [HttpGet]
        public ActionResult SetPublished(int id)
        {
            var result = _rolesManager.SetPublished(id);
            if (result.Success)
            {
                StoreLog("Roles", "Publish", id);
            }
            TempData["Result"] = result;
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            var result = _rolesManager.Delete(id);
            if (result.Success)
            {
                StoreLog("Roles", "DELETE", id);
            }
            TempData["Result"] = result;
            return RedirectToAction("Index");
        }

    }
}