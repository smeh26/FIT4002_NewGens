using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyNursingFuture.Api.Models
{
    public class ListingCriteriaModel
    {
        public int JobListingId { get; set; }
        public int AspectId { get; set; }
        public int QuestionId { get; set; }
        public int AnswerId { get; set; }
        public decimal Value { get; set; }
        public string TextAnswerField { get; set; }
        public DateTime LastUpdate { get; set; }
    }
    
}