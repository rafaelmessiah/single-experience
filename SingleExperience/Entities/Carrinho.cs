using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using SingleExperience.Entities.Enums;

namespace SingleExperience.Entities
{
    public class Carrinho
    {
        [Key]
        public int CarrinhoId { get; set; }

        //FK - Produto
        public int ProdutoId { get; set; }
        [ForeignKey("ProdutoId")]
        public Produto Produto { get; set; }

        //Fk - Cliente
        public int ClienteId { get; set; }
        [ForeignKey("ClienteId")]
        public Cliente Cliente { get; set; }

        public int Qtde { get; set; }

        //Fk - Status Carrinho Produto
        [Column("StatusCarrinhoProdutoId")]
        public StatusCarrinhoProdutoEnum StatusCarrinhoProdutoEnum { get; set; }
        [ForeignKey("StatusCarrinhoProdutoEnum")]
        public StatusCarrinhoProduto StatusCarrinhoProduto { get; set; }
    }
}
