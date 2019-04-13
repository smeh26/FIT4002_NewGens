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
using Newtonsoft.Json;

namespace MyNursingFuture.Cms.Controllers
{
    [JwtAuthorized]
    [ExceptionNullFilter]
    public class MenusController : BaseController
    {
        private readonly IMenuManager _menuManager;
        private readonly ILinksManager _linksManager;

        public MenusController(IMenuManager menuManager, ILinksManager linksManager, ILogChangesManager logChangesManager) : base(logChangesManager)
        {
            _menuManager = menuManager;
            _linksManager = linksManager;
        }

        // GET: Menus
        public ActionResult Index()
        {
            return View("Index");
        }

        [HttpGet]
        public ActionResult Edit(string menuType)
        {
            var model = new MenuViewModel();
            ViewBag.Menus = _menuManager.GetByType(menuType).Entity as IEnumerable<MenuEntity>;
            ViewBag.MenuType = menuType;

            var resultLinks = _linksManager.Get();
            var ienumResult = (IEnumerable<LinkEntity>)resultLinks.Entity;
            model.Links = ienumResult.ToList();
            return View("Edit", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(string menuType, string menus)
        {
            if (string.IsNullOrEmpty(menuType) || string.IsNullOrEmpty(menus))
            {
                TempData["Error"] = "Transaction error";
                return RedirectToAction("Index");
            }
            var listMenus = JsonConvert.DeserializeObject<List<MenuEntity>>(menus);
            var result = _menuManager.Update(listMenus, menuType);
            if (result.Success)
            {
                StoreLog("Menus", "Modify Menu "+ menuType, (int)result.Entity);
            }
            
            TempData["Result"] = result;
            return RedirectToAction("Edit", new {menuType});
        }
    }
}