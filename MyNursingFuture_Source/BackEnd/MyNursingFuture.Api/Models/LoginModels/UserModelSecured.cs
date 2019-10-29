using MyNursingFuture.BL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyNursingFuture.Api.Models
{
    public class UserModelSecured
    {
       
        public string Email { get; set; }
        public string Name { get; set; }
        public int UserId { get; set; }
        public string NurseType { get; set; }
        public string Area { get; set; }
        public string State { get; set; }
        public string ActiveWorking { get; set; }
        public string Country { get; set; }
        public string Suburb { get; set; }
        public string PostalCode { get; set; }
        public UsersQuizzesEntity Quizzes { get; set; }
        public int salary { get; set; }
        public int defaultQuizId { get; set; }


    }
}