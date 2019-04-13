using MyNursingFuture.BL.Entities;
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
    public class QuizzesController : Controller
    {
        private readonly IQuizzesManager _quizzesManager;
        private readonly IDomainsManager _domainManager;
        private readonly IAspectsManager _aspectsManager;
        private readonly IEnumManager _enumManager;
        public QuizzesController(IQuizzesManager quizzesManager, IDomainsManager domainManager, IAspectsManager aspectsManager, IEnumManager enumManager)
        {
            _quizzesManager = quizzesManager;
            _domainManager = domainManager;
            _aspectsManager = aspectsManager;
            _enumManager = enumManager;
        }
        
        public ActionResult CareerPathWays()
        {
            var result = _quizzesManager.GetQuizByType(QuizTypes.PATHWAY);
            if (!result.Success)
            {
                TempData["ErrorMessage"] = "An error occurred";
                return RedirectToAction("Index", "Home");
            }
            var quiz = result.Entity as QuizEntity;
            quiz.Questions = quiz.Questions ?? new List<QuestionEntity>();
            ViewBag.Quiz = quiz;
            return View();
        }

        public ActionResult SelfAssessment()
        {
            ViewBag.Domains = _domainManager.Get().Entity;
            return View();
        }

        public ActionResult SelfAssessmentQuestions(int domainId)
        {
            var result = _quizzesManager.GetQuizByDomain(domainId);
            if (!result.Success)
            {
                TempData["ErrorMessage"] = "An error occurred";
                return RedirectToAction("Index", "Home");
            }
            var quiz = result.Entity as QuizEntity;
            quiz.Questions = quiz.Questions ?? new List<QuestionEntity>();
            ViewBag.Quiz = quiz;
            ViewBag.Aspects = _aspectsManager.GetByDomain(domainId).Entity;
            return View();
        }

        public ActionResult Aboutyou()
        {

            ViewBag.UserDataFields = _enumManager.GetUserDataFields();
            var result = _quizzesManager.GetQuizByType(QuizTypes.ABOUT);
            if (!result.Success)
            {
                TempData["ErrorMessage"] = "An error occurred";
                return RedirectToAction("Index", "Home");
            }
            var quiz = result.Entity as QuizEntity;
            quiz.Questions = quiz.Questions ?? new List<QuestionEntity>();
            ViewBag.Quiz = quiz;
            return View();
        }
    }
}