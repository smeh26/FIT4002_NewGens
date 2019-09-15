﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyNursingFuture.DL.Models;


namespace MyNursingFuture.BL.Entities
{
    public class EmployerEntity : Employer
    {

        public string Token { get; set; }
        public string NewPassword { get; set; }
        public bool PasswordChanged { get; set; }
        
    }
}
