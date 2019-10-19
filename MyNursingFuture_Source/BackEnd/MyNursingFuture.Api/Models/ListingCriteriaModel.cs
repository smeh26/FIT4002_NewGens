using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyNursingFuture.BL.Entities;

namespace MyNursingFuture.Api.Models
{
    public class ListingCriteriaModel
    {
        public int JobListingId { get; set; }
        public int EmmployerId { get; set; }
        public Dictionary<int, AnswerEntity> Answers { get; set; } // Aspect ID / AnswerId

    }
    
}