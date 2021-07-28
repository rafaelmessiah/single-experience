using System;
using System.Collections.Generic;
using System.Text;

namespace SingleExperience.Services.Produto.Models
{
    class ProdutoRankingModel
    {
        public int ProdutoId { get; set; }
        public string Nome { get; set; }
        public double Preco { get; set; }
        public string Ranking { get; set; }

    }
}
