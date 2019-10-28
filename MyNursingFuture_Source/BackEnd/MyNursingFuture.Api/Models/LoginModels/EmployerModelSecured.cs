using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyNursingFuture.BL.Entities;

namespace MyNursingFuture.Api.Models
{
    /// <summary>
    /// Employer Model for API return
    /// 
    /// </summary>
    public class EmployerModelSecured
    {
        public int EmployerId { get; set; }
        public string Email { get; set; }
        public string EmployerName { get; set; }
        public string Company { get; set; }
        public string Area { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string Suburb { get; set; }
        public string PostalCode { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }

    }
}