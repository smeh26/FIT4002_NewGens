using MyNursingFuture.BL.Managers;
using MyNursingFuture.Cms.Models;
using System.Web.Mvc;
using MyNursingFuture.BL.Entities;
using System.Web;
using System;

namespace MyNursingFuture.Cms.Controllers
{
    public class LoginController : Controller
    {
        private readonly IAdministratorsManager _administratorsManager;
       

        public LoginController(IAdministratorsManager administratorsManager)
        {
            _administratorsManager = administratorsManager;
        }

        // GET: Login
        public ActionResult Index()
        {
            var credentials = new CredentialsManager();
            if (Session["AdminUser"] == null)
            {
                var cookie = Request.Cookies["MNFCMS"];
                if (cookie == null)
                {
                    return View();
                }
                var token = cookie["Token"];
                
                var result = credentials.ValidateAdminToken(token);
                if (!result.Success)
                    return View();
                var user = (AdministratorEntity) result.Entity;
                user.Token = token;
                Session["AdminUser"] = user;
                return RedirectToAction("Index", "Home");
            }
            else
            {
                var user = (AdministratorEntity) Session["AdminUser"];
                var result = credentials.ValidateAdminToken(user.Token);
                if (result.Success)
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            return View();
        }

        [ValidateAntiForgeryToken]
        public ActionResult LogIn(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }
            var administratorEntity = new AdministratorEntity()
            {
                Password = model.Password,
                Username = model.Username
            };
            var result = _administratorsManager.LogIn(administratorEntity);
            if (!result.Success)
            {
                TempData["Result"] = result;
                return RedirectToAction("Index");
            }
            Session["AdminUser"] = result.Entity;
            if (model.Remember)
            {
                HttpCookie cookie = new HttpCookie("MNFCMS");
                DateTime now = DateTime.Now;
                AdministratorEntity usr = (AdministratorEntity)result.Entity;
                cookie.Value = usr.Token;
                cookie.Expires = now.AddDays(30);
                Response.Cookies.Add(cookie);
            }            
            return RedirectToAction("Index","Home");
        }

        public ActionResult Logout()
        {
            Session["AdminUser"] = null;
            var cookie = Request.Cookies["MNFCMS"];
            if (cookie != null)
            {
                HttpCookie myCookie = new HttpCookie("MNFCMS");
                myCookie.Expires = DateTime.Now.AddDays(-1d);
                Response.Cookies.Add(myCookie);
            }
            Session.Abandon();
            return RedirectToAction("Index", "Login");
        }
    }
}