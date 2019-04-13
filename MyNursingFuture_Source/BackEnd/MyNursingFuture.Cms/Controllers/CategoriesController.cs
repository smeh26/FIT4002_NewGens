using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyNursingFuture.BL.Managers;
using MyNursingFuture.Cms.Filters;
using MyNursingFuture.BL.Entities;

namespace MyNursingFuture.Cms.Controllers
{
    [ExceptionNullFilter]
    [JwtAuthorized]
    public class CategoriesController : BaseController
    {
        private ICategoriesManager _categoriesManager;

        public CategoriesController(ICategoriesManager categoriesManager, ILogChangesManager logChangesManager) : base(logChangesManager)
        {
            _categoriesManager = categoriesManager;
        }
        // GET: Categories
        public ActionResult Index()
        {
            ViewBag.Categories = _categoriesManager.Get().Entity;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Insert(string name)
        {
            var entity = new CategoryEntity();
            entity.Name = name;
            var result = _categoriesManager.Insert(entity);
            if (result.Success)
            {
                StoreLog("Category", "Insert Category", (int)result.Entity);
            }
            
            TempData["Result"] = result;
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, string  name)
        {
            var entity = new CategoryEntity();
            entity.Name = name;
            entity.CategoryId = id;

            var result = _categoriesManager.Update(entity);
            if (result.Success)
            {
                StoreLog("Category", "Update Category", id);
            }

            TempData["Result"] = result;
            
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            var result = _categoriesManager.Delete(id);
            
            if (result.Success)
            {
                StoreLog("Category", "Delete", id);
            }

            TempData["Result"] = result;
            return RedirectToAction("Index");
        }

    }
}