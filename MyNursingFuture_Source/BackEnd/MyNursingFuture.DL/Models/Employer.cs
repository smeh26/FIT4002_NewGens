using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNursingFuture.DL.Models
{
    public class Employer : IModel
    {
        public int EmployerId { get; set; }
        public string Email { get; set; }
        public string EmployerName { get; set; }
        public string AgentFirstName { get; set; }
        public string AgentLastName { get; set; }
        public Nullable<Boolean> Active { get; set; }
        public string Company { get; set; }
        public string Password { get; set; }
        public string Hash { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? ModifyDate { get; set; }
        public string RecoverToken { get; set; }
        public string Area { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string Suburb { get; set; }
        public string PostalCode { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string Setting { get; set; }
        public Nullable<bool> SaveOnly { get; set; }

        // extension for membership 
        public string MembershipType { get; set; }
        public bool CanViewDetails { get; set; }
        public Nullable<DateTime> MembershipStartDate { get; set; }
        public Nullable<DateTime> MembershipEndDate { get; set; }
    }
}
