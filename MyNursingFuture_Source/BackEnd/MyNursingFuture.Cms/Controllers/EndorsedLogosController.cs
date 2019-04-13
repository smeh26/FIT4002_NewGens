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
    public class EndorsedLogosController : BaseController
    {
        private readonly IEndorsedLogosManager _endorsedLogosManager;
        private readonly IMapper _mapper;

        public EndorsedLogosController(IEndorsedLogosManager endorsedLogosManager, IMapper mapper, ILogChangesManager logChangesManager) : base(logChangesManager)
        {
            _endorsedLogosManager = endorsedLogosManager;
            _mapper = mapper;
        }

        // GET: EndorsedLogos
        public ActionResult Index()
        {
            var endorsedLogos = _endorsedLogosManager.Get().Entity as IEnumerable<EndorsedLogoEntity>;
            
            ViewBag.EndorsedLogos = endorsedLogos;
            return View("Index");
        }

        public ActionResult Insert()
        {
            var model = new EndorsedLogoViewModel();
            model.Operation = "I";
            return View("InsertEdit", model);
        }

        public ActionResult Edit(int id)
        {
            var result = _endorsedLogosManager.Get(id);
            if (!result.Success)
            {
                TempData["Result"] = result;
                return RedirectToAction("Index");
            }
            var entity = result.Entity as EndorsedLogoEntity;
            var model = _mapper.Map<EndorsedLogoEntity, EndorsedLogoViewModel>(entity);
            model.Operation = "E";
            return View("InsertEdit", model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult InsertEdit(EndorsedLogoViewModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["Error"] = "Transaction error";
                return RedirectToAction("Index", "Sectors");
            }
            var entity = _mapper.Map<EndorsedLogoViewModel, EndorsedLogoEntity>(model);
            entity.ImagePath = Server.MapPath("~/Content/img/Uploads/");
            var result = model.Operation == "E" ? _endorsedLogosManager.Update(entity) : _endorsedLogosManager.Insert(entity);
            TempData["Result"] = result;
            if (!result.Success)
            {
                return RedirectToAction("Index");
            }
            var operation = model.Operation == "E" ? "Edit Logo" : "Insert Logo";
            StoreLog("EndorsedLogos", operation, (int)result.Entity);
            return RedirectToAction("Edit", new { id = (int)result.Entity });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            var result = _endorsedLogosManager.Delete(id);
            if (result.Success)
            {
                StoreLog("EndorsedLogos", "DELETE", id);
            }
            TempData["Result"] = result;
            return RedirectToAction("Index");
        }
    }
}