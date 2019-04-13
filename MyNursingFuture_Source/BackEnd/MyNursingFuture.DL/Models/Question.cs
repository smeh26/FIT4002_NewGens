using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNursingFuture.DL.Models
{
    public class Question:IModel
    {
        public int QuestionId { get; set; }
        public int QuizId { get; set; }
        public string Text { get; set; }
        public string SubText { get; set; }
        public string Type { get; set; }
        public int? AspectId { get; set; }
        public string Requirements { get; set; }
        public string Examples { get; set; }
        public int Position { get; set; }
        public string FieldName { get; set; }
    }
}
