
using System;
using System.Collections.Generic;
using System.Text;

namespace SingleExperience.Services.CartaoCredito.Models
{
    public class CartaoDetalhadoModel
    {
        public int CartaoCreditoId { get; set; }
        public int ClienteId { get; set; }
        public string Numero { get; set; }
        public DateTime DataVencimento { get; set; }
    }
}
