using System;
using System.Collections.Generic;
using System.Text;

namespace SingleExperience.Services.CartaoCredito.Models
{
    public class CadastroCartaoModel
    {
        public int ClienteId { get; set; }
        public string Numero { get; set; }
        public string  Bandeira { get; set; }
        public string  CodigoSeguranca { get; set; }
        public DateTime DataVencimento { get; set; }

    }
}
