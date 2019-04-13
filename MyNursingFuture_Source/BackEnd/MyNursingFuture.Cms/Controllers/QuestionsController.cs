using AutoMapper;
using MyNursingFuture.BL.Entities;
using MyNursingFuture.BL.Managers;
using MyNursingFuture.Cms.Filters;
using MyNursingFuture.Cms.Models;
using MyNursingFuture.Util;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyNursingFuture.Cms.Controllers
{
    [JwtAuthorized]
    [ExceptionNullFilter]
    public class QuestionsController : BaseController
    {
        private readonly IQuestionsManager _questionsManager;
        private readonly ISectorsManager _sectorsManager;
        private readonly IMapper _mapper;
        public QuestionsController(IQuestionsManager questionsManager, ISectorsManager sectorsManager, IMapper mapper, ILogChangesManager logChangesManager) : base(logChangesManager)
        {
            _questionsManager = questionsManager;
            _sectorsManager = sectorsManager;
            _mapper = mapper;
        }

        public ActionResult InsertPathway(string type, int quizId)
        {

            ViewBag.Sectors = (IEnumerable<SectorEntity>)_sectorsManager.Get().Entity;
            var model = new QuestionViewModel();
            model.Operation = "I";
            model.Type = type;
            model.QuizId = quizId;
            model.QuizType = "PATHWAY";
            model.SectorsQuestions = new List<SectorsQuestionsEntity>();
            model.Answers = new List<AnswerEntity>();

            ViewBag.TitleQuiz = "Career Pathways";
            return View(model);
        }
        public ActionResult InsertAssessment(string type, int quizId, int aspectId)
        {
            QuestionViewModel model;

            var result = _questionsManager.GetByAspect(aspectId);
            if (result.Success)
            {
                var entity = result.Entity as QuestionEntity;
                model = _mapper.Map<QuestionEntity, QuestionViewModel>(entity);
                model.Operation = "E";
                model.Answers =entity.Answers;
            }
            else
            {
                model = new QuestionViewModel();
                model.Operation = "I";
                model.Answers = new List<AnswerEntity>();
            }
            model.AspectId = aspectId;
            model.Type = type;
            model.QuizId = quizId;
            model.QuizType = "ASSESSMENT";
            model.SectorsQuestions = new List<SectorsQuestionsEntity>();
           
            ViewBag.TitleQuiz = "Self Assassment";
            return View("InsertEdit",model);
        }

        
        public ActionResult InsertAbout(string type, int quizId, string quizType)
        {

            var model = new QuestionViewModel();
            model.Operation = "I";
            model.QuizId = quizId;
            model.QuizType = "ABOUT";
            model.Type = type;
            model.SectorsQuestions = new List<SectorsQuestionsEntity>();
            model.Answers = new List<AnswerEntity>();
            ViewBag.TitleQuiz = "About you";
            return View("InsertEdit",model);
        }

        
        public ActionResult Edit(int id, string type, string quizType)
        {
            ViewBag.Sectors = (IEnumerable<SectorEntity>)_sectorsManager.Get().Entity;
            var entity = _questionsManager.Get(id).Entity as QuestionEntity;
            var model = _mapper.Map<QuestionEntity, QuestionViewModel>(entity);
            model.Operation = "E";
            model.QuizType = quizType;
            model.Type = type;
            switch (quizType)
            {
                case "PATHWAY":
                    ViewBag.Sectors = (IEnumerable<SectorEntity>)_sectorsManager.Get().Entity;
                    ViewBag.TitleQuiz = "Career Pathways";
                    return View("InsertEdit", model);
                case "ABOUT":
                    ViewBag.TitleQuiz = "About you";
                    return View("InsertEdit", model);
            }
            TempData["ErrorMessage"] = "An error occurred";
            return RedirectToAction("Index","Home");
        }

        [ValidateAntiForgeryToken]
        public ActionResult InsertEdit(QuestionViewModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["Error"] = "Transaction error";
                return RedirectToAction("Index", "Sectors");
            }
            var entity = _mapper.Map<QuestionViewModel, QuestionEntity>(model);
            entity.Answers = JsonConvert.DeserializeObject<List<AnswerEntity>>(model.AnswersJson);
            entity.SectorsQuestions = String.IsNullOrEmpty(model.SectorsJson)?new List<SectorsQuestionsEntity>(): JsonConvert.DeserializeObject<List<SectorsQuestionsEntity>>(model.SectorsJson);
            var result = model.Operation == "E" ? _questionsManager.Update(entity) : _questionsManager.Insert(entity);
            TempData["Result"] = result;
            if (!result.Success)
            {
                return RedirectToAction("Index","Home");
            }

            var operation = model.Operation == "E" ? "Edit Question " : "Insert Question ";
            StoreLog("Questions", operation + model.QuizType, (int)result.Entity);

            if (model.QuizType == "ASSESSMENT")
            {
                return RedirectToAction("InsertAssessment", new { quizId = (int)result.Entity, aspectId = model.AspectId, type = model.Type });
            }
            return RedirectToAction("Edit", new { id = (int)result.Entity, quizType = model.QuizType, type= model.Type });
        }


        public ActionResult GetQuestions(int id, string name)
        {
            var result = _questionsManager.GetQuestionsQuizAndText(id, name);
            return Json(result);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, string quizType, int domainId = 0)
        {
            var result = _questionsManager.Delete(id);
            if (result.Success)
            {
                StoreLog("Questions", "DELETE Question " + quizType, id);
            }
            TempData["Result"] = result;
            switch (quizType)
            {
                case "ASSESSMENT":
                    return RedirectToAction("SelfAssessmentQuestions", "Quizzes", new { domainId = domainId });
                case "ABOUT":
                    return RedirectToAction("Aboutyou", "Quizzes");
                case "PATHWAY":
                    return RedirectToAction("CareerPathWays", "Quizzes");
            }
            return RedirectToAction("Index","Home");
        }

        public ActionResult UpdatePosition(int questionId, int position, string fieldName = null, bool updateField = false)
        {
            Result result;
            if (!updateField)
            {
                result = _questionsManager.UpdatePosition(questionId, position);
            }
            else
            {
                result = _questionsManager.UpdateFieldNameAndPosition(questionId, position, fieldName);
            }
           
            return Json(result);
        }

    }
}