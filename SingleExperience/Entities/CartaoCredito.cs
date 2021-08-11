using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SingleExperience.Entities
{
    public class CartaoCredito
    {
        [Key]
        public int CartaoCreditoId { get; set; }

        //FK - Cliente
        public int ClienteId { get; set; }
        [ForeignKey("ClienteId")]
        public Cliente Cliente { get; set; }

        public string Numero { get; set; }
        public string Bandeira { get; set; }
        public string CodigoSeguranca { get; set; }
        public DateTime DataVencimento { get; set; }
    }
}
