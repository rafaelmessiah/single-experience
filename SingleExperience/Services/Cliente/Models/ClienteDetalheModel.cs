using System;
using System.Collections.Generic;
using System.Text;

namespace SingleExperience.Services.Cliente.Models
{
    public class ClienteDetalheModel
    {
        public int ClienteId { get; set; }
        public string  Cpf { get; set; }
        public string Nome { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Telefone { get; set; }
    }
}
