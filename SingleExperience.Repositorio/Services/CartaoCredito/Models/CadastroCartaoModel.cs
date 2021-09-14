using System;
using System.Collections.Generic;
using System.Text;

namespace SingleExperience.Services.CartaoCredito.Models
{
    public class CadastroCartaoModel
    {
        public int ClienteId { get; set; }
        public string Numero { get; set; }
        public string Bandeira { get; set; }
        public string CodigoSeguranca { get; set; }
        public DateTime DataVencimento { get; set; }

        public void Validar()
        {
            if (ClienteId < 1)
                throw new Exception("O ClienteId é obrigatório para esse cadastro");

            if (DataVencimento.CompareTo(DateTime.Now) < 0)
                throw new Exception("Data de Vencimento Invalida");

            if (Numero == null)
                throw new Exception("O numero é obrigatorio");

            if (Numero.Length > 20)
                throw new Exception("O numero ultrapassou o limite de caracteres");

            if (Bandeira == null)
                throw new Exception("A bandeira é obrigatoria");

            if (Bandeira.Length > 20)
                throw new Exception("A bandeira ultrapassou o limite de caracteres");

            if (CodigoSeguranca == null)
                throw new Exception("O codigo de segurança é obrigatorio");

            if (CodigoSeguranca.Length > 20)
                throw new Exception("A bandeira ultrapassou o limite de caracteres");

            if (DataVencimento == null)
                throw new Exception("A data de vencimento é obrigatoria");
        }

    }
}
