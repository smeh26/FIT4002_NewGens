/**
 * 
 * <Author> Nguyen Pham - 27348032  </Author>
 * <copyright>  The following code is the work of Nguyen Pham unless otherwise specified  </copyright>
 * <date> 29/10/2019 </date>
 * <summary> </summary>
 */
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
        public int EmployerId { set; get; }
        public int JobApplicationId { set; get; }
        public int JobListingId { set; get; }
        public string Summary { set; get; }
        public bool IsDraft { set; get; }
        public string ApplicationStatus { set; get; }
        public string FeedbackFromEmployer { get; set; }
        public string FeedbackFromNurse { get; set; }
        public DateTime AppliedDate { get; set; }
        public Nullable<bool> IsShortlisted { set; get; }
        public Nullable<DateTime> ShortListedDate { get; set; }
        public Nullable<bool> IsDeclined { set; get; }
        public Nullable<DateTime> DeclinedDate { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public int ExpectedSalary { get; set; }
        public Nullable<DateTime> MakeContactDeadline { get; set; }

    }
}
