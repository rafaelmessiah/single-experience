using System;
using System.Collections.Generic;
using System.Text;
using SingleExperience.Entities.Enums;

namespace SingleExperience.Entities
{
    class ProdutoEntity
    {
        public int ProdutoId { get; set; }
        public CategoriaEnum CategoriaId { get; set; }
        public StatusProdutoEnum StatusProdutoId { get; set; }
        public string Nome { get; set; }
        public double Preco { get; set; }
        public string Detalhe { get; set; }
        public int QtdeEmEstoque { get; set; }
        public string Ranking { get; set; }
        public bool Disponivel { get; set; }
    }
}
