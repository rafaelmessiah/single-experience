using SingleExperience.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Text;

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
