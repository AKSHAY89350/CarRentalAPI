﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Assess_Layer.DTO
{
    public class LoginDto
    {
       
        public string Email { get; set; }

       
        public string Password { get; set; }

       
        public string Role { get; set; }
    }
}
