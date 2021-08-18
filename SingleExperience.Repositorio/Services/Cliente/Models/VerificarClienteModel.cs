using System;
using System.Collections.Generic;
using System.Text;

namespace SingleExperience.Services.Cliente.Models
{
    public class VerificarClienteModel
    {
        public int ClienteId { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
    }
}
