using System;
using System.Collections.Generic;
using System.Text;

namespace SingleExperience.Services.Carrinho.Models
{
    public class ItemModel
    {
        public int CarrinhoId { get; set; }
        public int ProdutoId { get; set; }
        public string Nome { get; set; }
        public double Preco { get; set; }
    }
}
