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
    public class SectorViewsController : BaseController
    {
        private readonly ISectorViewsManager _sectorViewsManager;
        private readonly IMapper _mapper;
        public SectorViewsController(ISectorViewsManager sectorViewsManager, IMapper mapper, ILogChangesManager logChangesManager) : base(logChangesManager)
        {
            _sectorViewsManager = sectorViewsManager;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var result = _sectorViewsManager.Get(id);
            if (!result.Success)
            {
                return RedirectToAction("Index", "Home");
            }
            var entity = result.Entity as SectorViewEntity;

            var model = _mapper.Map<SectorViewEntity, SectorViewViewModel>(entity);

            return View("Edit", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(SectorViewViewModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["Error"] = "Transaction error";
                return RedirectToAction("Edit", "Sectors", new {id = model.SectorId});
            }
            var entity = _mapper.Map<SectorViewViewModel, SectorViewEntity>(model);
            var result = _sectorViewsManager.Update(entity);
            if (result.Success)
            {
                StoreLog("Sectors", "Edit " + model.Framework + " View", model.SectorViewId);
            }
            TempData["Result"] = result;
            return RedirectToAction("Edit", new{id = model.SectorViewId});
        }

        [HttpGet]
        public ActionResult SetActive(int id, int sectorId)
        {
            var result = _sectorViewsManager.SetActive(id);
            if (result.Success)
            {
                StoreLog("Sectors", "Set View Active", sectorId);
            }
            TempData["Result"] = result;
            return RedirectToAction("Edit", "Sectors", new { id = sectorId });
        }

        [HttpGet]
        public ActionResult SetInactive(int id, int sectorId)
        {
            var result = _sectorViewsManager.SetActive(id, false);
            if (result.Success)
            {
                StoreLog("Sectors", "Set View Inactive", sectorId);
            }
            TempData["Result"] = result;
            return RedirectToAction("Edit", "Sectors", new { id = sectorId });
        }
    }
}