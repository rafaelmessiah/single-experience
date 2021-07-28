using System;
using System.Collections.Generic;
using System.Text;
using SingleExperience.Entities.Enums;

namespace SingleExperience.Entities
{
    class CarrinhoEntity
    {
        public int CarrinhoId { get; set; }
        public int Produtoid { get; set; }
        public int ClienteId { get; set; }
        public StatusCarrinhoProdutoEnum StatusCarrinhoProdutoId { get; set; }

    }
}
