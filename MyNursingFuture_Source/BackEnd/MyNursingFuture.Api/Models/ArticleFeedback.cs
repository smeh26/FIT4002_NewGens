using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyNursingFuture.Api.Models
{
    public class ArticleFeedback
    {
        public int ArticleId { get; set; }
        public string Title { get; set; }
        public string Feedback { get; set; }
        public bool Positive { get; set; }
    }
}