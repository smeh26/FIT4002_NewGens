using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyNursingFuture.BL.Entities;

namespace MyNursingFuture.Api.Models
{
    public class EmployerModel
    {
        public string Token { get; set; }
        public int EmployerID { get; set; }
        public string Email { get; set; }
        public string EmployerName { get; set; }
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
        public bool SaveOnly { get; set; }
    }
}