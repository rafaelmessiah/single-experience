using SingleExperience.Enums;
using System;

namespace SingleExperience.Services.Compra.Models
{
    public class IniciarModel
    {
        public int ClienteId { get; set; }
        public int EnderecoId { get; set; }
        public FormaPagamentoEnum FormaPagamentoEnum { get; set; }

        public void Validar()
        {
            if (ClienteId < 1)
                throw new Exception("O cliente é obrigatório");

            if (EnderecoId < 1)
                throw new Exception("O endereço é obrigatório");

            if (FormaPagamentoEnum == 0)
                throw new Exception("A forma de pagamento é obrigatória");

        }
    }
}
