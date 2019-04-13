using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNursingFuture.DL.Models
{
    public class UserDataQuestions : IModel
    {
        public int UserDataQuestionId { get; set; }
        public int QuestionId { get; set; }
        public string FieldName { get; set; }
    }
}
