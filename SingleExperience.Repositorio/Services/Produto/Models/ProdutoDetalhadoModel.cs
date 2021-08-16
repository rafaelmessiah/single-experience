using System;
using System.Collections.Generic;
using System.Text;

namespace SingleExperience.Services.Produto.Models
{
    public class ProdutoDetalhadoModel
    {
        public int ProdutoId { get; set; }
        public string Nome { get; set; }
        public decimal Preco { get; set; }
        public string Descricao { get; set; }
    }
}
