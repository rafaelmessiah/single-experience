using System;
using System.Collections.Generic;
using System.Text;
using SingleExperience.Entities.Enums;

namespace SingleExperience.Entities
{
    public class CompraEntity
    {
        public int CompraId { get; set; }
        public StatusCompraEnum StatusCompraId { get; set; }
        public FormaPagamentoEnum FormaPagamentoId { get; set; }
        public int ClienteId { get; set; }
        public int EnderecoId { get; set; }
        public bool StatusPagamento { get; set; }
        public DateTime DataCompra { get; set; }
        public DateTime DataPagamento { get; set; }
        public double ValorFinal { get; set; }
    }
}
