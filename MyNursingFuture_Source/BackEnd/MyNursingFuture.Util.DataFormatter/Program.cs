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
/*
            Console.WriteLine("//Reconstructing database //");
            FrameworkManager FM = new FrameworkManager();
            AspectsManager AM = new AspectsManager();
            NurseSelfAssessmentAnswersManager NSAM = new NurseSelfAssessmentAnswersManager();
            
            //Get Aspects
            var aspect_Resul = AM.Get();
            var aspects_List = (List<AspectEntity>)aspect_Resul.Entity;
            var aspects_Dict = aspects_List.ToDictionary(x => x.AspectId, x => x);


            //Get Answers

            var answ_Resul = NSAM.GetAllQuizz_OldDB();
            var answ_List = (List<UsersQuizzesEntity>)answ_Resul.Entity;
            List<Object> results = new List<Object>();

            //Parse data
            foreach (UsersQuizzesEntity entity in answ_List)
            {
                //Console.WriteLine(entity.Results);
                Object ressult_obj = JsonConvert.DeserializeObject(entity.Results);
                Object answer = ressult_obj.GetType().GetProperty("Answer");
                if (answer != null)
                {
                    //var answer_dict = answer.ToDictionary(x => x.);



                }
                    

            }



            System.Threading.Thread.Sleep(5000);







        }

        Dictionary<int, string> GetAnswer(string json)
        {
            JObject json_object = JObject.Parse(json);
            //IDictionary<JToken> 
        }*/

    }
}
