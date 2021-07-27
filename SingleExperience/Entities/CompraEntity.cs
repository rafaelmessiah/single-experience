using System;
using System.Collections.Generic;
using System.Text;
using SingleExperience.Entities.Enums;

namespace SingleExperience.Entities
{
    class CompraEntity
    {
        public int CompraId { get; set; }
        public int SituacaoCompraId { get; set; }
        public int FormaPagamentoId { get; set; }
        public SituacaoPagamentoEnum SituacaoPagamentoId { get; set; }
        public DateTime DataCompra { get; set; }
        public DateTime DataPagamento { get; set; }
        public double ValorFinal { get; set; }
    }
}
