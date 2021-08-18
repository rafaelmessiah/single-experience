using SingleExperience.Enums;
using System;

namespace SingleExperience.Services.Compra.Models
{
    public class CompraDetalhadaModel
    {
        public int CompraId { get; set; }
        public DateTime DataCompra { get; set; }
        public FormaPagamentoEnum FormaPagamentoId { get; set; }
        public DateTime? DataPagamento { get; set; }
        public decimal ValorTotal { get; set; }

    }
}
