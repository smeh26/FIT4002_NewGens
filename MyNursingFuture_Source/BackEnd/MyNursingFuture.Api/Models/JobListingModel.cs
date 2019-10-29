/**
 * 
 * <Author> Nguyen Pham - 27348032  </Author>
 * <copyright> The following code is the work of Nguyen Pham unless other wise specified  </copyright>
 * <remarks> This is a part of the FIT4002 project. Product owner is APNA. Project supervisor is Robyn McNamara  </remarks>
 * <date>  </date>
 * <summary> </summary>
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyNursingFuture.Api.Models
{
    public class JobListingModel
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