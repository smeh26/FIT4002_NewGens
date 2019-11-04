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
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MyNursingFuture.Api.Models
{/// <summary>
///  Used for loging in
/// </summary>
    public class LoginObject
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }

    }
}