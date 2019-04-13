using MyNursingFuture.BL.Managers;
using MyNursingFuture.Cms.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyNursingFuture.Cms.Controllers
{
    [JwtAuthorized]
    [ExceptionNullFilter]
    public class ImagesController : Controller
    {
        private readonly IImageManager _imagesManager;
        public ImagesController(IImageManager imagesManager)
        {
            _imagesManager = imagesManager;
        }
        // GET: Images


            
        public ActionResult Index(int row)
        {
            var result = _imagesManager.GetImages(row);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}