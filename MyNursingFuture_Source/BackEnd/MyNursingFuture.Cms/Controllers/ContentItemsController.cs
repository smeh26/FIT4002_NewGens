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
using MyNursingFuture.Util;

namespace MyNursingFuture.Cms.Controllers
{
    [JwtAuthorized]
    [ExceptionNullFilter]
    public class ContentItemsController : BaseController
    {
        private readonly IContentItemsManager _contentItemsManager;
        private readonly ILinksManager _linksManager;
        private readonly IMapper _mapper;

        public ContentItemsController(IContentItemsManager contentItemsManager, IMapper mapper, ILinksManager linksManager, ILogChangesManager logChangesManager) : base(logChangesManager)
        {
            _contentItemsManager = contentItemsManager;
            _mapper = mapper;
            _linksManager = linksManager;
        }

        public ActionResult InsertDefault(int sectionId)
        {
            var model = new ContentItemViewModel();
            ViewBag.Previous = "SECTION";
            model.SectionId = sectionId;
            model.Type = ContentItemsType.DEFAULT.ToString();

            var resultLinks = _linksManager.Get();
            var ienumResult = (IEnumerable<LinkEntity>) resultLinks.Entity;
            model.Links = ienumResult.ToList();
            model.Operation = "I";
            return View("InsertEditDefault", model);
        }

        public ActionResult InsertVideo(int sectionId)
        {
            var model = new ContentItemViewModel();
            ViewBag.Previous = "SECTION";
            model.SectionId = sectionId;
            model.Type = ContentItemsType.VIDEOEMBED.ToString();
            model.Operation = "I";
            return View("InsertEditVideo", model);
        }

        public ActionResult InsertMarkup(int sectionId)
        {
            var model = new ContentItemViewModel();
            ViewBag.Previous = "SECTION";
            model.SectionId = sectionId;
            model.Type = ContentItemsType.MARKUP.ToString();
            model.Operation = "I";
            return View("InsertEditMarkup", model);
        }

        public ActionResult InsertAccordion(int sectionId)
        {
            var model = new ContentItemViewModel();
            ViewBag.Previous = "SECTION";
            model.SectionId = sectionId;
            model.Type = ContentItemsType.ACCORDION.ToString();
            model.Operation = "I";
            return View("InsertEditAccordion", model);
        }

        public ActionResult InsertLink(int sectionId)
        {
            var model = new ContentItemViewModel();
            ViewBag.Previous = "SECTION";
            model.SectionId = sectionId;
            model.Type = ContentItemsType.LINK.ToString();
            model.Operation = "I";
            var resultLinks = _linksManager.Get();
            var ienumResult = (IEnumerable<LinkEntity>)resultLinks.Entity;
            model.Links = ienumResult.ToList();
            return View("InsertEditSingleLineLink", model);
        }

        public ActionResult InsertHeading(int sectionId)
        {
            var model = new ContentItemViewModel();
            ViewBag.Previous = "SECTION";
            model.SectionId = sectionId;
            model.Type = ContentItemsType.HEADING.ToString();
            model.Operation = "I";
            return View("InsertEditHeading", model);
        }

        public ActionResult InsertSingleLineLink(int sectionId)
        {
            var model = new ContentItemViewModel();
            ViewBag.Previous = "SECTION";
            model.SectionId = sectionId;
            model.Type = ContentItemsType.SINGLELINELINK.ToString();
            model.Operation = "I";
            var resultLinks = _linksManager.Get();
            var ienumResult = (IEnumerable<LinkEntity>)resultLinks.Entity;
            model.Links = ienumResult.ToList();

            return View("InsertEditSingleLineLink", model);
        }

        public ActionResult InsertSingleButtonLink(int sectionId)
        {
            var model = new ContentItemViewModel();
            ViewBag.Previous = "SECTION";
            model.SectionId = sectionId;
            model.Type = ContentItemsType.SINGLEBUTTONLINK.ToString();
            model.Operation = "I";
            var resultLinks = _linksManager.Get();
            var ienumResult = (IEnumerable<LinkEntity>)resultLinks.Entity;
            model.Links = ienumResult.ToList();

            return View("InsertEditSingleLineLink", model);
        }

        public ActionResult InsertGenericItem(int sectionId, string type)
        {
            if (string.IsNullOrEmpty(type))
            {
                TempData["ErrorMessage"] = "Transaction error";
                return RedirectToAction("Index", "Home");
            }
            Result result = _contentItemsManager.InsertGeneric(sectionId, type);
            if (result.Success)
            {
                StoreLog("Sections", "Insert Item "+ type, sectionId);
            }
            TempData["Result"] = result;
            return RedirectToAction("Edit", "Sections", new { id = sectionId });
        }

        ///ARTICLES

        public ActionResult InsertDefaultArticle(int articleId)
        {
            var model = new ContentItemViewModel();
            ViewBag.Previous = "ARTICLE";
            model.ArticleId = articleId;
            model.Type = ContentItemsType.DEFAULT.ToString();

            var resultLinks = _linksManager.Get();
            var ienumResult = (IEnumerable<LinkEntity>)resultLinks.Entity;
            model.Links = ienumResult.ToList();
            model.Operation = "I";
            return View("InsertEditDefault", model);
        }

        public ActionResult InsertVideoArticle(int articleId)
        {
            var model = new ContentItemViewModel();
            ViewBag.Previous = "ARTICLE";
            model.ArticleId = articleId;
            model.Type = ContentItemsType.VIDEOEMBED.ToString();
            model.Operation = "I";
            return View("InsertEditVideo", model);
        }

        public ActionResult InsertMarkupArticle(int articleId)
        {
            var model = new ContentItemViewModel();
            ViewBag.Previous = "ARTICLE";
            model.ArticleId = articleId;
            model.Type = ContentItemsType.MARKUP.ToString();
            model.Operation = "I";
            return View("InsertEditMarkup", model);
        }

        public ActionResult InsertLinkArticle(int articleId)
        {
            var model = new ContentItemViewModel();
            ViewBag.Previous = "ARTICLE";
            model.ArticleId = articleId;
            model.Type = ContentItemsType.LINK.ToString();
            model.Operation = "I";
            var resultLinks = _linksManager.Get();
            var ienumResult = (IEnumerable<LinkEntity>)resultLinks.Entity;
            model.Links = ienumResult.ToList();
            return View("InsertEditSingleLineLink", model);
        }

        public ActionResult InsertHeadingArticle(int articleId)
        {
            var model = new ContentItemViewModel();
            ViewBag.Previous = "ARTICLE";
            model.ArticleId = articleId;
            model.Type = ContentItemsType.HEADING.ToString();
            model.Operation = "I";
            return View("InsertEditHeading", model);
        }

        public ActionResult InsertSingleLineLinkArticle(int articleId)
        {
            var model = new ContentItemViewModel();
            model.ArticleId = articleId;
            ViewBag.Previous = "ARTICLE";
            model.Type = ContentItemsType.SINGLELINELINK.ToString();
            model.Operation = "I";
            var resultLinks = _linksManager.Get();
            var ienumResult = (IEnumerable<LinkEntity>)resultLinks.Entity;
            model.Links = ienumResult.ToList();

            return View("InsertEditSingleLineLink", model);
        }

        public ActionResult InsertSingleButtonLinkArticle(int articleId)
        {
            var model = new ContentItemViewModel();
            ViewBag.Previous = "ARTICLE";
            model.ArticleId = articleId;
            model.Type = ContentItemsType.SINGLEBUTTONLINK.ToString();
            model.Operation = "I";
            var resultLinks = _linksManager.Get();
            var ienumResult = (IEnumerable<LinkEntity>)resultLinks.Entity;
            model.Links = ienumResult.ToList();
            return View("InsertEditSingleLineLink", model);
        }

        public ActionResult InsertGenericItemArticle(int articleId, string type)
        {
            if (string.IsNullOrEmpty(type))
            {
                TempData["ErrorMessage"] = "Transaction error";
                return RedirectToAction("Index", "Home");
            }
            Result result = _contentItemsManager.InsertGenericArticle(articleId, type);
            if (result.Success)
            {
                StoreLog("Articles", "Insert Item " + type, articleId);
            }
            TempData["Result"] = result;
            return RedirectToAction("Edit", "Articles", new { id = articleId });
        }

        public ActionResult Edit(int id, string previous)
        {
            var result = _contentItemsManager.Get(id);
            if (!result.Success)
            {
                return RedirectToAction("Index", "Home");
            }
            var entity = (ContentItemEntity) result.Entity;

            ViewBag.Previous = previous;
            var model = _mapper.Map<ContentItemEntity, ContentItemViewModel>(entity);
            model.Operation = "E";
            var resultLinks = _linksManager.Get();
            var ienumResult = (IEnumerable<LinkEntity>)resultLinks.Entity;
            model.Links = ienumResult.ToList();
            switch (entity.Type)
            {
                case "DEFAULT":
                    return View("InsertEditDefault", model);
                case "SINGLELINELINK":
                    return View("InsertEditSingleLineLink", model);
                case "SINGLEBUTTONLINK":
                    return View("InsertEditSingleLineLink", model);
                case "MARKUP":
                    return View("InsertEditMarkup",model);
                case "ACCORDION":
                    return View("InsertEditAccordion", model);
                case "VIDEOEMBED":
                    return View("InsertEditVideo", model);
                case "LINK":
                    return View("InsertEditSingleLineLink", model);
                case "HEADING":
                    return View("InsertEditHeading", model);
            }
            return View("InsertEditDefault", model);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult InsertEdit(ContentItemViewModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = "Transaction error";
                if (model.ArticleId != null)
                {
                    return RedirectToAction("Edit", "Articles", new { id = model.ArticleId });
                }
                return RedirectToAction("Edit", "Sections", new { id = model.SectionId });
            }
            var entity = _mapper.Map<ContentItemViewModel, ContentItemEntity> (model);
            entity.ImagePath = Server.MapPath("~/Content/img/Uploads/");
            Result result = model.Operation == "I" ? _contentItemsManager.Insert(entity) : _contentItemsManager.Update(entity);

            TempData["Result"] = result;
            var operation = model.Operation == "E" ? "Edit Section Item " : "Insert Section Item ";
            var tableName = "Sections";
            var id = model.SectionId;
            if (model.ArticleId != null)
            {
                tableName = "Articles";
                operation = model.Operation == "E" ? "Edit Articles Item " : "Insert Articles Item ";
                id = model.ArticleId;
                return RedirectToAction("Edit", "Articles", new { id = model.ArticleId });
            }
            StoreLog(tableName, operation + model.Type, id);
            return RedirectToAction("Edit", "Sections", new {id = model.SectionId });
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, int sectionId)
        {
            var result = _contentItemsManager.Delete(id);
            if (result.Success)
            {
                StoreLog("Sections", "DELETE Item", sectionId);
            }
            TempData["result"] = result;
            return RedirectToAction("Edit", "Sections", new {id = sectionId});
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteFromArticle(int id, int articleId)
        {
            var result = _contentItemsManager.Delete(id);
            if (result.Success)
            {
                StoreLog("Articles", "DELETE Item", articleId);
            }
            TempData["result"] = result;
            return RedirectToAction("Edit", "Articles", new { id = articleId });
        }

        public ActionResult UpdatePosition(int contentItemId, int position)
        {
            Result  result = _contentItemsManager.UpdatePosition(contentItemId, position);
            return Json(result);
        }
    }
}