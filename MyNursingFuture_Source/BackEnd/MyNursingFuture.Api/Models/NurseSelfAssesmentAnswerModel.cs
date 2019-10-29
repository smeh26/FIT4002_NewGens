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
    public class NurseSelfAssesmentAnswerModel
    {
        public int UserId { get; set; }
        public int AspectId { get; set; }
        public int QuestionId { get; set; }
        public int AnswerId { get; set; }
        public decimal Value { get; set; }
        public string TextAnswerField { get; set; }
        public DateTime LastUpdate { get; set; }
    }

}