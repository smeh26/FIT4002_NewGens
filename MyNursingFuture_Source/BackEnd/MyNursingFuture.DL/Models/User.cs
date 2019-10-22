using System;

namespace MyNursingFuture.DL.Models
{
    public class User : IModel
    {
        public int UserId { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string ApnaMemberId { get; set; }
        public bool ApnaUser { get; set; }
        public string Password { get; set; }
        public string Hash { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? ModifyDate { get; set; }
        public string RecoverToken { get; set; }
        public string NurseType { get; set; }
        public bool IsLookingForWork { get; set; }
        public string MinSalaryReq { get; set; }
        public string Area { get; set; }
        public string State { get; set; }
        public string ActiveWorking { get; set; }
        public string Age { get; set; }
        public string Country { get; set; }
        public string Suburb { get; set; }
        public string PostalCode { get; set; }
        public string Patients { get; set; }
        public string PatientsTitle { get; set; }
        public string Qualification { get; set; }
        public string Setting { get; set; }
        public bool SaveOnly { get; set; }
    }
}
