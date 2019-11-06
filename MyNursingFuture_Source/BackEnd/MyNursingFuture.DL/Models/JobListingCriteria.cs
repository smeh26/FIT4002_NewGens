using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNursingFuture.DL.Models
{
    public class JobListingCriteria
    {
        public int JobListingId { get; set; }
        //public int AspectId { get; set; }
        //public int QuestionId { get; set; }
        //public int AnswerId { get; set; }
        public int AspectId { get; set; }
        public int QuestionId { get; set; }
        public int AnswerId { get; set; }
        public decimal Value { get; set; }
        public Nullable<DateTime> LastUpdate { get; set; }


    }
}
