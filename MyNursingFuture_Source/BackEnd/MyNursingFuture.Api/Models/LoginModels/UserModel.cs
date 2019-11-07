<<<<<<< HEAD:MyNursingFuture_Source/BackEnd/MyNursingFuture.Api/Models/UserModel.cs
﻿using MyNursingFuture.BL.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MyNursingFuture.Api.Models
{
    public class UserModel
    {
       
        public string Token { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public int UserId { get; set; }
        public bool ApnaUser { get; set; }
        public string NurseType { get; set; }
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

        [Column("minsalary")]
        public string  Salary{ get; set; }
        public IEnumerable<UsersQuizzesEntity> Quizzes { get; set; }
    }
=======
﻿using MyNursingFuture.BL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyNursingFuture.Api.Models
{
    public class UserModel
    {
       
        public string Token { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public int UserId { get; set; }
        public bool ApnaUser { get; set; }
        public string NurseType { get; set; }
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
        public IEnumerable<UsersQuizzesEntity> Quizzes { get; set; }
        public Nullable<bool> IsLookingForJob { get; set; }
        public int salary { get; set; }
        public int defaultQuizId { get; set; }


    }
>>>>>>> bc980297bb8da53481415626ba19c77f0623152c:MyNursingFuture_Source/BackEnd/MyNursingFuture.Api/Models/LoginModels/UserModel.cs
}