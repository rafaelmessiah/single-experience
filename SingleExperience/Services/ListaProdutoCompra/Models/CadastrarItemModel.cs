using System;
using System.Collections.Generic;
using System.Text;

namespace SingleExperience.Services.ListaProdutoCompra.Models
{
    public class CadastrarItemModel
    {
        public int CompraId { get; set; }
        public int ProdutoId { get; set; }
        public int Qtde { get; set; }
    }
}
