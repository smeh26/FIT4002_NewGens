using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNursingFuture.DL.Models
{
    public class AnonUserQuizzes: IModel
    {
        public int UserQuizId { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public int QuizId { get; set; }
        public DateTime DateVal { get; set; }
        public string Date { get; set; }
        public string Results { get; set; }
        public string Answers { get; set; }
        public bool Completed { get; set; }
        public string Type { get; set; }
        public string Survey { get; set; }
    }
}
