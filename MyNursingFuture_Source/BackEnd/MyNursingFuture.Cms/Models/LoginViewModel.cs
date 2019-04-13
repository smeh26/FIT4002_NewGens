using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyNursingFuture.Cms.Models
{
    public class LoginViewModel
    {
        [Required]
        public string Username { get; set; }
        [Required]
        [StringLength(30, MinimumLength = 6, ErrorMessage = "Invalid length")]
        public string Password { get; set; }
        public bool Remember { get; set; }
    }
}