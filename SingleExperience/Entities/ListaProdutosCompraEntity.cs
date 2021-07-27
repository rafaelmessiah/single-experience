using System;
using System.Collections.Generic;
using System.Text;

namespace SingleExperience.Entities
{
    class ListaProdutosCompraEntity
    {
        public int ListaProdutosCompraId { get; set; }
        public int CompraId { get; set; }
        public int ProdutoId { get; set; }
        public int Qtde { get; set; }
        public double Valor { get; set; }

    }
}
