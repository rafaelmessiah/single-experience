using System;
using System.Collections.Generic;
using System.Text;

namespace SingleExperience.Services.ListaProdutoCompra.Models
{
    public class ItemProdutoCompraModel
    {
        public int ListaProdutoCompraId { get; set; }
        public int CompraId { get; set; }
        public int ProdutoId { get; set; }
        public string Nome { get; set; }
        public decimal PrecoUnitario { get; set; }
        public int Qtde { get; set; }

    }
}
