using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SingleExperience.Services.Cliente.Models
{
    public class LoginModel
    {
        public string Email { get; set; }
        public string Senha { get; set; }
    }
}
