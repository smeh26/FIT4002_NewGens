﻿using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyNursingFuture.DL.Models
{
    public class User : IModel
    {
        public int UserId { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string ApnaMemberId { get; set; }
        public Nullable<bool> ApnaUser { get; set; }
        public string Password { get; set; }
        public string Hash { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? ModifyDate { get; set; }
        public string RecoverToken { get; set; }
        public string NurseType { get; set; }
        public bool IsLookingForWork { get; set; }
        [Column("minsalary")]
        public string Salary { get; set; }
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
        public Nullable<bool> SaveOnly { get; set; }

        // V1 extension
        public Nullable<bool> IsLookingForJob { get; set; }
        public int MaxSalary { get; set; }
        public int MinSalary { get; set; }
        public int salary { get; set; }
        public int defaultQuizId { get; set; }

    }
}
