using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyNursingFuture.BL.Entities;
using MyNursingFuture.BL.Managers;

namespace MyNursingFuture.Test.BL.Manager
{
    [TestClass]
    public class QuestionsManagerTest
    {
        [Ignore]
        [TestMethod]
        public void TestInsert()
        {
            var m = new QuestionsManager();

            var answer1 = new AnswerEntity
            {
                Value = 0,
                Text = "No way"
            };

            var answer2 = new AnswerEntity
            {
                Value = 0.5m,
                Text = "May way"
            };

            var answer3 = new AnswerEntity
            {
                Value = 1,
                Text = "yes way"
            };

            var list = new List<AnswerEntity>();
            list.Add(answer1);
            list.Add(answer2);
            list.Add(answer3);
            var entity = new QuestionEntity
            {
                QuizId = 1,
                Answers = list,
                Type = "RANGE",
                Text = "if you were a panda, would you kill the others pandas?",
                SubText = "SUBTEXTX"
            };

            var result = m.Insert(entity);
            Assert.IsTrue(result.Success);

        }
        [Ignore]
        [TestMethod]
        public void TestUpdate()
        {
            var m = new QuestionsManager();

            var answer1 = new AnswerEntity
            {
                Value = 0,
                Text = "No wayx"
            };

            var answer2 = new AnswerEntity
            {
                Value = 0.5m,
                Text = "May way"
            };

            var answer3 = new AnswerEntity
            {
                Value = 1,
                Text = "yes waye"
            };

            var list = new List<AnswerEntity>();
            list.Add(answer1);
            list.Add(answer2);
            list.Add(answer3);
            var entity = new QuestionEntity
            {
                QuestionId = 7,
                Answers = list,
                Type = "RANGE",
                Text = "if you were a pandax, would you kill the others pandas?",
                SubText = "SUBTEXTX"
            };
            
            var result = m.Update(entity);
            Assert.IsTrue(result.Success);

        }

        [Ignore]
        [TestMethod]
        public void TestGet()
        {
            var m = new QuestionsManager();
            var r = m.Get(7);
            Assert.IsTrue(r.Success);
        }
    }
}
