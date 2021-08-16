using System;
using System.Collections.Generic;
using System.Text;

namespace SingleExperience.Services.Carrinho.Models
{
    public class ItemCarrinhoModel
    {
        public int CarrinhoId { get; set; }
        public int ProdutoId { get; set; }
        public string Nome { get; set; }
        public decimal Preco { get; set; }
        public int Qtde { get; set; }

        public void Validar()
        {
            if (Qtde < 0)
            {
                throw new Exception("A quantidade do produto não pode ser negativa");
            }

            if (Preco < 0)
            {
                throw new Exception("O preço do produto não pode ser negativa");
            }
        }
    }
}
