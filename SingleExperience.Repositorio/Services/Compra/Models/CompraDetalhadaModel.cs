using SingleExperience.Enums;
using SingleExperience.Services.ListaProdutoCompra.Models;
using System;
using System.Collections.Generic;

namespace SingleExperience.Services.Compra.Models
{
    public class CompraDetalhadaModel
    {
        public int CompraId { get; set; }
        public DateTime DataCompra { get; set; }
        public string FormaPagamento { get; set; }
        public DateTime? DataPagamento { get; set; }
        public decimal ValorTotal { get; set; }
        public List<ItemProdutoCompraModel> ItensComprados { get; set; }
    }
}
