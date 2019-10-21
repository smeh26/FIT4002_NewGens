using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNursingFuture.DL.Models
{
    public class JobApplication:IModel
    {
        public int UserId { set; get; }
        public int JobApplicationId { set; get; }
        public int JobListingId { set; get; }
        public string Summary { set; get; }
        public bool IsDraft { set; get; }
        public string ApplicationStatus { set; get; }
        public string FeedbackFromEmployer { get; set; }
        public string FeedbackFromNurse { get; set; }
        public DateTime AppliedDate { get; set; }
        public bool IsShortlisted { set; get; }
        public DateTime ShortListedDate { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public string Name { get; set; }
        public string EmailAddress { get; set; }
        public int PhoneNumber { get; set; }
        public string Country { get; set; }
        public string Suburb { get; set; }
        public string PostalCode { get; set; }
        public string Qualification { get; set; }



    }
}
