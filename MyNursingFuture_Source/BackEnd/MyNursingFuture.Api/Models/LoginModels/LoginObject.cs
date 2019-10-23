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