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

        public void Validar()
        {
            if (this.DataVencimento.CompareTo(DateTime.Now) < 0)
            {
                throw new Exception("Data de Vencimento Invalida");
            }
        }

    }
}
