using System;
using System.Collections.Generic;
using System.Text;

namespace SingleExperience.Services.Carrinho.Models
{
    public class SalvarModel
    {
        public int ProdutoId { get; set; }
        public int ClienteId { get; set; }
        public int Qtde { get; set; }
    }
}
