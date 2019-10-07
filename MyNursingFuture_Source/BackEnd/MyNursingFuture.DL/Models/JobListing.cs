using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNursingFuture.DL.Models
{
    public class JobListing: IModel
    {
        public int JobListingId { get; set; }
        public int EmployerId { get; set; }
        public string Title { get; set; }
        public string NurseType { get; set; }
        public string SpecialRequirements { get; set; }
        public bool PublishStatus { get; set; }
        public int minSalary { get; set; }
        public int maxSalary { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ApplicationDeadline { get; set; }

        public DateTime ModificationDate { get; set; }
        public string Area { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string Suburb { get; set; }
        public string PostalCode { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public bool Completed { get; set; }
        public string JobType { get; set; }

    }
}
