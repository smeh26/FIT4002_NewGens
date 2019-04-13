using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyNursingFuture.Cms.Models
{
    public class DataExtractModel
    {
        [DisplayName("Extract Type")]
        public string ExtractType { get; set; }
        [DisplayName("Start Date")]

        public DateTime DateStart { get; set; }
        [DisplayName("End Date")]
        public DateTime DateEnd { get; set; }
    }
}