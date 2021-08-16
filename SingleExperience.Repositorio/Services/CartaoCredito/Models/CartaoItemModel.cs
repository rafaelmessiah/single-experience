using System;
using System.Collections.Generic;
using System.Text;

namespace SingleExperience.Services.CartaoCredito.Models
{
    public class CartaoItemModel
    {
        public int CartaoCreditoId { get; set; }
        public int ClienteId { get; set; }
        public string Numero { get; set; }
    }
}
