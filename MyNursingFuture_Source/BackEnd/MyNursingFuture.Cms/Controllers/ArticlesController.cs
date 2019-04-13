using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using MyNursingFuture.BL.Managers;
using MyNursingFuture.BL.Entities;
using MyNursingFuture.Cms.Models;
using MyNursingFuture.Cms.Filters;
using Newtonsoft.Json;

namespace MyNursingFuture.Cms.Controllers
{
    [JwtAuthorized]
    [ExceptionNullFilter]
    public class ArticlesController : BaseController
    {
        private readonly IArticlesManager _articlesManager;
        private readonly IMapper _mapper;
        private ICategoriesManager _categoriesManager;
        private readonly IEnumManager _enumManager;
        public ArticlesController(IArticlesManager articlesManager, IMapper mapper, ICategoriesManager categoriesManager, IEnumManager enumManager, ILogChangesManager logChangesManager) : base(logChangesManager)
        {
            _articlesManager = articlesManager;
            _mapper = mapper;
            _categoriesManager = categoriesManager;
            _enumManager = enumManager;
        }
        
        public ActionResult Index()
        {
            var result = _articlesManager.Get();
            ViewBag.Articles = result.Entity as List<ArticleEntity>;
            return View("Index");
        }

        // GET: Articles
        public ActionResult Insert()
        {
            var model = new ArticleViewModel();
            model.Operation = "I";
            model.Type = "ARTICLE";
            ViewBag.ContentItemTypes = _enumManager.GetContentItemList();
            ViewBag.Categories = _categoriesManager.Get().Entity;
            return View("InsertEdit", model);
        }

        // GET: Articles
        public ActionResult Edit(int id)
        {
            var result = _articlesManager.Get(id);
            if (!result.Success)
            {
                TempData["ErrorMessage"] = "Articles not found";
                return RedirectToAction("Index");
            }
            var entity = (ArticleEntity)result.Entity;
            var model = _mapper.Map<ArticleEntity, ArticleViewModel>(entity);            
            ViewBag.ContentItemTypes = _enumManager.GetContentItemList();
            ViewBag.Categories = _categoriesManager.Get().Entity;
            model.Operation = "E";
            return View("InsertEdit", model);
        }

        // GET: Articles
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult InsertEdit(ArticleViewModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = "Transaction error";
                return RedirectToAction("Index");
            }
            var entity = _mapper.Map<ArticleViewModel, ArticleEntity>(model);
            var result = model.Operation == "I" ? _articlesManager.Insert(entity) : _articlesManager.Update(entity);
            TempData["Result"] = result;
            if (!result.Success)
            {
                return RedirectToAction("Index");
            }
            var operation = model.Operation == "E" ? "Edit Articles" : "Insert Articles";
            StoreLog("Articles", operation, (int)result.Entity);

            return RedirectToAction("Edit", new { id = (int)result.Entity });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            var result = _articlesManager.Delete(id);

            if (result.Success)
            {
                StoreLog("Articles", "DELETE", id);
            }

            TempData["Result"] = result;
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult SetPublished(int id)
        {
            var result = _articlesManager.SetPublished(id);

            if (result.Success)
            {
                StoreLog("Articles", "Set Published", id);
            }

            TempData["Result"] = result;
            
            return RedirectToAction("Index");
        }

        public ActionResult ArticleReasons()
        {
            var result = _articlesManager.Reasons();
            if (!result.Success)
            {
                TempData["ErrorMessage"] = "Articles not found";
                return RedirectToAction("Index");
            }
            ViewBag.Reasons = (IEnumerable<ReasonEntity>)result.Entity;
            return View("Reasons");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditReasons(string reasons)
        {
            var list = !string.IsNullOrEmpty(reasons)?JsonConvert.DeserializeObject<List<ReasonEntity>>(reasons):new List<ReasonEntity>();
            var result = _articlesManager.UpdateReasons(list);
            if (result.Success)
            {
                StoreLog("Reasons", "Reasons Modified", 0);
            }
            TempData["Result"] = result;
            return RedirectToAction("ArticleReasons");
        }
    }
}