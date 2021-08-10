using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using SingleExperience.Entities.Enums;

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
        public FormaPagamento FormaPagamento { get; set; }

        //FK - Cliente
        public int ClienteId { get; set; }
        [ForeignKey("ClienteId")]
        public Cliente Cliente { get; set; }

        //FK - Endereco
        public int EnderecoId { get; set; }
        [ForeignKey("EnderecoId")]
        public Endereco Endereco { get; set; }

        public bool StatusPagamento { get; set; }
        public DateTime DataCompra { get; set; }
        public DateTime DataPagamento { get; set; }
        public double ValorFinal { get; set; }
    }
}
