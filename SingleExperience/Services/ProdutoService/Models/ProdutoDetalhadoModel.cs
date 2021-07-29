using System;
using System.Collections.Generic;
using System.Text;

namespace SingleExperience.Services.ProdutoService.Models
{
    class ProdutoDetalhadoModel
    {
        public int ProdutoId { get; set; }
        public string Nome { get; set; }
        public double Preco { get; set; }
        public string Descricao { get; set; }
    }
}
