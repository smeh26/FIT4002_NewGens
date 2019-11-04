using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyNursingFuture.BL.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyNursingFuture.Api.Models;
using MyNursingFuture.BL.Entities;
using MyNursingFuture.Util;
using MyNursingFuture.Api.Filters;
using Newtonsoft.Json;
using Newtonsoft.Json.Schema;
using Newtonsoft.Json.Schema.Generation;
using System.Web.Script.Serialization;

using System.Diagnostics;
using System.Threading;
using System.ComponentModel;
using Newtonsoft.Json.Linq;


namespace ListingAPI.Tests
{
    [TestClass()]
    public class JobListingControllerTests
    {
        [TestMethod()]
        public void CreateJobListingTest()
        {
            //To Implement
            Assert.IsTrue(true);
        }
        [TestMethod()]
        public void GetJSON()
        {
            JSchemaGenerator generator = new JSchemaGenerator();

            JSchema schema = generator.Generate(typeof(JobListingEntity));
            Console.WriteLine(schema.ToString());
            Assert.IsTrue(true);
        }

        [TestMethod()]
        public void GenerateTestJSON()


        {

            FrameworkManager FM = new FrameworkManager();
            AspectsManager AM = new AspectsManager();
            AnswersManager ASWM = new AnswersManager();
            QuestionsManager QM = new QuestionsManager();

            NurseSelfAssessmentAnswersManager NSAM = new NurseSelfAssessmentAnswersManager();
            //Get Aspects
            var aspect_Resul = AM.Get();
            var aspects_List = (List<AspectEntity>)aspect_Resul.Entity;
            var aspects_Dict = aspects_List.ToDictionary(x => x.AspectId, x => x);




            //Get Answers
            var answers_result = ASWM.Get();
            var answers_List = (List<AnswerEntity>)answers_result.Entity;
            var answers_Dict = answers_List.ToDictionary(x => (x.QuestionId, x.Value), x => x);

            //Get Questions
            var questions_result = QM.Get();
            var questions_List = (List<QuestionEntity>)questions_result.Entity;
            var question_Dict = questions_List.ToDictionary(x => x.AspectId, x => x);

            var criteria = new Dictionary<int, AnswerEntity>();

            var criteria_entity = new ListingCriteriaModel();
            criteria_entity.EmmployerId = 1;
            criteria_entity.JobListingId = 3;

            decimal value = (decimal)0.66;
            for (int i = 1; i <= 30; i++)
            {
                var question = question_Dict[i];
                var answer = new AnswerEntity();
                if (answers_Dict.TryGetValue((question.QuestionId, value), out answer)) {
                    criteria.Add(i, answer);
                };

            }
            criteria_entity.Answers = criteria;

            var json = JsonConvert.SerializeObject(criteria_entity);
            Console.WriteLine(json);



            Assert.IsTrue(true);
        }
    }
}