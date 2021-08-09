using System;
using System.Collections.Generic;
using System.Text;

namespace SingleExperience.Services.Endereco.Models
{
    public class CadastroEnderecoModel
    {
        public int ClienteId { get; set; }
        public string Rua { get; set; }
        public string Numero { get; set; }
        public string Complemento { get; set; }
        public string Cep { get; set; }

    }
}
