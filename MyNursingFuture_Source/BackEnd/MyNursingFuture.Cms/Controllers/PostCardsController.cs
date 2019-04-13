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
    public class PostCardsController : BaseController
    {
        private readonly IPostCardsManager _postCardsManager;
        private readonly IMapper _mapper;

        public PostCardsController(IPostCardsManager postCardsManager, IMapper mapper, ILogChangesManager logChangesManager) : base(logChangesManager)
        {
            _postCardsManager = postCardsManager;
            _mapper = mapper;
        }

        // GET: PostCards
        public ActionResult Index()
        {
            var postcards =  _postCardsManager.Get().Entity as IEnumerable<PostCardEntity>;

            foreach (var item in postcards)
            {
                item.Text = item.Text.Truncate(75);
            }

            ViewBag.PostCards = postcards;
            return View("Index");
        }

        public ActionResult Insert()
        {
            var model = new PostCardViewModel();
            model.Operation = "I";
            return View("InsertEdit", model);
        }

        public ActionResult Edit(int id)
        {
            var result = _postCardsManager.Get(id);
            if (!result.Success)
            {
                TempData["Result"] = result;
                return RedirectToAction("Index");
            }
            var entity = result.Entity as PostCardEntity;
            var model = _mapper.Map<PostCardEntity, PostCardViewModel>(entity);
            model.Operation = "E";
            return View("InsertEdit", model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult InsertEdit(PostCardViewModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["Error"] = "Transaction error";
                return RedirectToAction("Index", "Sectors");
            }
            var entity = _mapper.Map<PostCardViewModel, PostCardEntity>(model);
            entity.ImagePath = Server.MapPath("~/Content/img/Uploads/");
            var result = model.Operation == "E" ? _postCardsManager.Update(entity) : _postCardsManager.Insert(entity);
            TempData["Result"] = result;
            if (!result.Success)
            {
                return RedirectToAction("Index");
            }
            var operation = model.Operation == "E" ? "Edit Postcard" : "Insert Postcard";
            StoreLog("Postcards", operation, (int)result.Entity);
            return RedirectToAction("Edit", new { id = (int)result.Entity });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            var result = _postCardsManager.Delete(id);
            if (result.Success)
            {
                StoreLog("Postcards", "DELETE", id);
            }
            TempData["Result"] = result;
            return RedirectToAction("Index");
        }
    }
}