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
    public class ListingCriteriaModel
    {
        public int JobListingId { get; set; }
        public int EmmployerId { get; set; }
        public Dictionary<int, AnswerEntity> Answers { get; set; } // Aspect ID / AnswerId

    }
    
}