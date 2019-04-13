using AutoMapper;
using MyNursingFuture.BL.Entities;
using MyNursingFuture.BL.Managers;
using MyNursingFuture.Cms.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyNursingFuture.Cms.Controllers
{
    public abstract class BaseController: Controller
    {

        protected readonly ILogChangesManager _logChangesManager;

        public BaseController(ILogChangesManager logChangesManager)
        {
            _logChangesManager = logChangesManager;
        }
        protected void StoreLog(string tableName, string operation, int? identifier)
        {
            var admin = (AdministratorEntity)Session["AdminUser"];
            var logChangeEntity = new LogChangeEntity();
            logChangeEntity.Identifier = identifier;
            logChangeEntity.Name = admin.Name;
            logChangeEntity.Username = admin.Username;
            logChangeEntity.TableName = tableName;
            logChangeEntity.Operation = operation;
            _logChangesManager.Insert(logChangeEntity);
        }

    }

    public abstract class EntityController<TEntity, TViewModel> : BaseController where TEntity : class, IEntity where TViewModel : class, IViewModel, new()
    {
        private string ControllerName { get { return RouteData.Values["controller"] as string; } }

        private string ControllerNameSingular { get { return ControllerName.Substring(0, ControllerName.Length - 1); } }

        protected virtual IManager<TEntity> Manager { get; set; }
        protected IMapper Mapper { get; set; }
         
        public EntityController(ILogChangesManager logChangesManager, IMapper mapper, IManager<TEntity> manager) : base(logChangesManager)
        {
            Mapper = mapper;
            Manager = manager;
        }

        [HttpGet]
        public virtual ActionResult Publish(int id, bool publish = true)
        {
            var result = Manager.SetPublished(id, publish);
            if (result.Success)
            {
                StoreLog(ControllerName, publish ? "Publish" : "Unpublish", id);
            }

            TempData["Result"] = result;
            return RedirectToAction("Index");
        }


        [HttpPost]
        public virtual ActionResult Delete(int id)
        {
            var result = Manager.Delete(id);

            if (result.Success)
            {
                StoreLog(ControllerName, "DELETE", id);
            }

            TempData["Result"] = result;
            return RedirectToAction("Index");
        }
        public abstract ActionResult Index();
        public virtual ActionResult Edit(int id)
        {
            var result = Manager.Get(id);
            if (!result.Success)
            {
                TempData["Error"] = ControllerNameSingular + " not found";
                return RedirectToAction("Index");
            }
            TEntity entity = result.Entity as TEntity;
            var model = Mapper.Map<TEntity, TViewModel>(entity);
            model.Operation = "E";
            return View("InsertEdit", model);
        }
        public virtual ActionResult Insert()
        {
            var model = new TViewModel();
            model.Operation = "I";
            return View("InsertEdit", model);
        
    }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult InsertEdit(TViewModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["Error"] = "Transaction error";
                return RedirectToAction("Index");
            }
            var entity = Mapper.Map<TViewModel, TEntity>(model);
            var result = model.Operation == "E" ? Manager.Update(entity) : Manager.Insert(entity);
            TempData["Result"] = result;
            if (!result.Success)
            {
                return RedirectToAction("Index");
            }
            var operation = model.Operation == "E" ? "Edit " + ControllerNameSingular : "Insert " + ControllerNameSingular;

            StoreLog(ControllerName, operation, (int)result.Entity);

            return RedirectToAction("Edit", new { id = (int)result.Entity });
        }

    }
}