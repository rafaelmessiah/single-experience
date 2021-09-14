using SingleExperience.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SingleExperience.Entities
{
    public class Compra
    {
        [Key]
        public int CompraId { get; set; }

        //FK - Status Compra
        [Column("StatusCompraId")]
        public StatusCompraEnum StatusCompraEnum { get; set; }
        [ForeignKey("StatusCompraEnum")]
        public StatusCompra StatusCompra { get; set; }

        //Fk - Forma Pagamento
        [Column("FormaPagamentoId")]
        public FormaPagamentoEnum FormaPagamentoEnum { get; set; }
        [ForeignKey("FormaPagamentoEnum")]
        public FormaPagamento FormaPagamento { get; set; }

        //FK - Cliente
        public int ClienteId { get; set; }
        [ForeignKey("ClienteId")]
        public Cliente Cliente { get; set; }

        //FK - Endereco
        public int EnderecoId { get; set; }
        [ForeignKey("EnderecoId")]
        public Endereco Endereco { get; set; }

        //FK - Credito Credito
        public int? CartaoCreditoId { get; set; }
        [ForeignKey("CartaoCreditoId")]
        public CartaoCredito CartaoCredito { get; set; }

        public List<ListaProdutoCompra> ListaProdutoCompras { get; set; }
        public DateTime DataCompra { get; set; }
        public DateTime? DataPagamento { get; set; }
        public decimal ValorFinal { get; set; }
    }
}
