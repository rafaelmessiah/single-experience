using System;
using System.Collections.Generic;
using System.Text;

namespace SingleExperience.Services.ListaProdutoCompra.Models
{
    public class ItemModel
    {
        public int ListaProdutoCompraId { get; set; }
        public int ProdutoId { get; set; }
        public string Nome { get; set; }
        public double PrecoUnitario { get; set; }
        public int Qtde { get; set; }
    }
}
