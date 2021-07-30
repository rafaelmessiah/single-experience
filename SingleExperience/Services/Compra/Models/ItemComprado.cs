using System;
using System.Collections.Generic;
using System.Text;

namespace SingleExperience.Services.Compra.Models
{
    public class ItemComprado
    {
        public int ListaProdutoCompraId { get; set; }
        public string Nome { get; set; }
        public double PrecoUnitario { get; set; }
        public int Qtde { get; set; }
        public double ValorItem { get; set; }
    }
}
