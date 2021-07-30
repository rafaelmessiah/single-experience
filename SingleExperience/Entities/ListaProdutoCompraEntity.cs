using System;
using System.Collections.Generic;
using System.Text;

namespace SingleExperience.Entities
{
    public class ListaProdutoCompraEntity
    {
        public int ListaProdutoCompraId { get; set; }
        public int CompraId { get; set; }
        public int ProdutoId { get; set; }
        public int Qtde { get; set; }
   
    }
}
