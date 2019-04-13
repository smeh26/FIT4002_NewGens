using System.Web.Mvc;
using MyNursingFuture.BL.Managers;
using MyNursingFuture.Cms.Filters;

namespace MyNursingFuture.Cms.Controllers
{
    [JwtAuthorized]
    [ExceptionNullFilter]
    public class LinksController : Controller
    {
        private readonly ILinksManager _linksManager;

        public LinksController(ILinksManager linksManager)
        {
            _linksManager = linksManager;
        }

        [HttpPost]
        public ActionResult Index()
        {
            var result = _linksManager.Get();
            return Json(result);
        }
    }
}