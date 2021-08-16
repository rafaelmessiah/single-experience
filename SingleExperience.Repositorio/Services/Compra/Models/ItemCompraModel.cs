using SingleExperience.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace SingleExperience.Services.Compra.Models
{
    public class ItemCompraModel
    {
        public int CompraId { get; set; }
        public decimal ValorFinal { get; set; }
        public DateTime DataCompra { get; set; }
        public StatusCompraEnum StatusCompra { get; set; }
    }
}
