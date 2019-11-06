using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyNursingFuture.DL.Models;


namespace MyNursingFuture.BL.Entities
{
    public class JobApplicationEntity: JobApplication, IEntity
    {

        public List<NurseSelfAssessmentAnswersEntity> PreferedQuizz { get; set; }
    }
}
