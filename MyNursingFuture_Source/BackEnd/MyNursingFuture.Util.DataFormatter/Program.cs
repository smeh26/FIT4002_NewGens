using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyNursingFuture.BL.Entities;
using MyNursingFuture.BL.Managers;
using MyNursingFuture.Util;
using System.Diagnostics;
using System.Threading;
using System.ComponentModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


/*
 * This program is created for recontructing the database to the new format 
 */

namespace MyNursingFuture.Util.DataFormatter
{

    class Program

    {

        static void Main(string[] args)
        {

            Console.WriteLine("//Reconstructing database //");
            FrameworkManager FM = new FrameworkManager();
            AspectsManager AM = new AspectsManager();
            AnswersManager ASWM = new AnswersManager();
            QuestionsManager QM = new QuestionsManager();

            NurseSelfAssessmentAnswersManager NSAM = new NurseSelfAssessmentAnswersManager();

            //Get Aspects
            var aspect_Resul = AM.Get();
            var aspects_List = (List<AspectEntity>)aspect_Resul.Entity;
            var aspects_Dict = aspects_List.ToDictionary(x => x.AspectId, x => x);


            //Get User Quizz Answers

            var NSAM_Resul = NSAM.GetAllQuizz_OldDB();
            var answ_List = (List<UsersQuizzesEntity>)NSAM_Resul.Entity;

            //Get Answers
            var answers_result = ASWM.Get();
            var answers_List = (List<AnswerEntity>)answers_result.Entity;
            var answers_Dict = answers_List.ToDictionary(x => (x.QuestionId, x.Value), x => x);

            //Get Questions
            var questions_result = QM.Get();
            var questions_List = (List<QuestionEntity>)questions_result.Entity;
            var question_Dict = questions_List.ToDictionary(x => x.AspectId, x => x);



            List<Object> results = new List<Object>();
            int error_counter = 0;

            //Parse data
            foreach (UsersQuizzesEntity entity in answ_List)
            {
                //Console.WriteLine(entity.Results);
                JObject parent_json = JObject.Parse(entity.Results);
                var answer = parent_json.Value<JObject>("answers").Properties();
                if (answer != null)
                {
                    var nurse_answer_dict = answer.ToDictionary(k => Int32.Parse(k.Name), v => Decimal.Parse(v.Value.ToString()));

                    foreach (KeyValuePair<int, decimal> ans in nurse_answer_dict)
                    {
                        NurseSelfAssessmentAnswersEntity ans_entity = new NurseSelfAssessmentAnswersEntity();
                        ans_entity.AspectId = ans.Key;
                        ans_entity.Value = ans.Value;
                        ans_entity.LastUpdate = entity.DateVal;

                        QuestionEntity question_entity = null;
                        if (question_Dict.TryGetValue(ans.Key, out question_entity))
                        {
                            ans_entity.QuestionId = question_entity.QuestionId;

                            AnswerEntity answer_entity = null;
                            if (answers_Dict.TryGetValue((ans_entity.QuestionId, ans_entity.Value), out answer_entity))
                            {
                                ans_entity.AnswerId = answer_entity.AnswerId;
                            }
                        }
                        // insert answer into database
                        var result = NSAM.InsertAnswer(entity.UserId, ans_entity);

                        if (!result.Success)
                        {
                            Console.WriteLine(result.Message);
                            error_counter++;
                        }




                    }


                }
            }


            Console.WriteLine(String.Format("process completed with {0} error", error_counter));


            System.Threading.Thread.Sleep(5000);







        }


    }
}

