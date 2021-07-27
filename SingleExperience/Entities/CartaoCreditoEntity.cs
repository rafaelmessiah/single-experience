using System;
using System.Collections.Generic;
using System.Text;

namespace SingleExperience.Entities
{
    class CartaoCreditoEntity
    {
        public int CartaoCreditoId { get; set; }
        public int ClienteId { get; set; }
        public string Numero { get; set; }
        public string Bandeira { get; set; }
    }
}
