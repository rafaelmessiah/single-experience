using System;
using System.Collections.Generic;
using System.Text;

namespace SingleExperience.Entities
{
    class ClienteEntity
    {
        public int ClienteId { get; set; }
        public string Cpf { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Telefone { get; set; }


    }
}
