using SingleExperience.Enums;
using System;

namespace SingleExperience.Services.Carrinho.Models
{
    public class EdicaoStatusModel
    {
        public int CarrinhoId { get; set; }
        public StatusCarrinhoProdutoEnum StatusEnum { get; set; }

        public void Validar()
        {
            if (CarrinhoId < 1)
                throw new Exception("A carrinho id é obrigatorio para essa");

            if (StatusEnum == 0)
                throw new Exception("O StatusEnum é Obrigatorio");
        }
    }
}
