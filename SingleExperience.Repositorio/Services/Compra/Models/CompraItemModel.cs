using SingleExperience.Enums;
using System;

namespace SingleExperience.Services.Compra.Models
{
    public class CompraItemModel
    {
        public int CompraId { get; set; }
        public decimal ValorFinal { get; set; }
        public DateTime DataCompra { get; set; }
        public StatusCompraEnum StatusCompra { get; set; }
    }
}
