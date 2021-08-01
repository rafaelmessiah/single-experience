using SingleExperience.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace SingleExperience.Services.Carrinho.Models
{
    public class EdicaoStatusModel
    {
        public int CarrinhoId { get; set; }
        public StatusCarrinhoProdutoEnum StatusEnum { get; set; }
    }
}
