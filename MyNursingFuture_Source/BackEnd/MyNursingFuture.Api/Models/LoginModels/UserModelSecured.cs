/**
 * 
 * <Author> Nguyen Pham - 27348032  </Author>
 * <copyright> The following code is the work of Nguyen Pham unless other wise specified  </copyright>
 * <remarks> This is a part of the FIT4002 project. Product owner is APNA. Project supervisor is Robyn McNamara  </remarks>
 * <date>  </date>
 * <summary> </summary>
 */
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