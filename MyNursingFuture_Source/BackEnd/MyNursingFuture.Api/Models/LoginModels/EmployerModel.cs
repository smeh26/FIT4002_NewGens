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
using MyNursingFuture.BL.Entities;

namespace MyNursingFuture.Api.Models
{
    /// <summary>
    /// Employer Model for API return
    /// 
    /// </summary>
    public class EmployerModel
    {
        public string Token { get; set; }
        public int EmployerId { get; set; }
        public string Email { get; set; }
        public string EmployerName { get; set; }
        public string Company { get; set; }
        public string Password { get; set; }
 //       public string Hash { get; set; }
//        public DateTime? CreateDate { get; set; }
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
        public bool CanViewDetails { get; set; }
        public DateTime MembershipStartDate { get; set; }
        public DateTime MembershipEndDate { get; set; }
    }
}