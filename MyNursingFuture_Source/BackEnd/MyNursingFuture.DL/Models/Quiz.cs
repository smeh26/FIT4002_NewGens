using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNursingFuture.DL.Models
{
    public class Quiz:IModel
    {
        public int QuizId { get; set; }
        public int? DomainId { get; set; }
        public string Name { get; set; }
        public bool Active { get; set; }
        public string Type { get; set; }
    }
}
