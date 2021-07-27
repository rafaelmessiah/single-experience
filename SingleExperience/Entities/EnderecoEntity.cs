using System;
using System.Collections.Generic;
using System.Text;

namespace SingleExperience.Entities
{
    class EnderecoEntity
    {
        public int EnderecoId { get; set; }
        public string Rua { get; set; }
        public string Numero { get; set; }
        public string Complemento { get; set; }
        public string Cep { get; set; }

    }
}
