using SingleExperience.Enums;
using System;

namespace SingleExperience.Services.Compra.Models
{
    public class CadastroModel
    {
        public int ClienteId { get; set; }
        public decimal ValorFinal { get; set; }
        public int EnderecoId { get; set; }
        public FormaPagamentoEnum FormaPagamentoId { get; set; }

        public void Validar()
        {
            if (ClienteId < 1)
                throw new Exception("O ClienteId é obrigatorio");

            if (ValorFinal >= 0)
                throw new Exception("O valor final não pode zero ou negativo");
        }
    }
}
