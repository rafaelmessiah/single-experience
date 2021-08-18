using System;
using System.Collections.Generic;
using System.Text;

namespace SingleExperience.Services.CartaoCredito.Models
{
    public class VerificarCartaoModel
    {
        public int CartaoCreditoId { get; set; }
        public int ClienteId { get; set; }
        public string CodigoSeguranca { get; set; }
    }
}
