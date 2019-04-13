using MyNursingFuture.DL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNursingFuture.BL.Entities
{
    public class QuestionEntity: Question, IEntity
    {
        public List<AnswerEntity> Answers { get; set; }
        public List<SectorsQuestionsEntity> SectorsQuestions { get; set; }
        public string QuizType { get; set; }
    }
}
