using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SingleExperience.Entities
{
    public class ListaProdutoCompra
    {
        [Key]
        public int ListaProdutoCompraId { get; set; }

        //FK - Compra
        public int CompraId { get; set; }
        [ForeignKey("CompraId")]
        public Compra Compra { get; set; }

        //FK - Produto
        public int ProdutoId { get; set; }
        [ForeignKey("ProdutoId")]
        public Produto Produto { get; set; }

        public int Qtde { get; set; }
    }
}
