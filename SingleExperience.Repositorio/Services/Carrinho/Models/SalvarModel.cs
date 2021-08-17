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

        public void Validar()
        {
            if (ProdutoId < 1)
                throw new Exception("O ProdutoId é obrigatorio");

            if (ClienteId < 1)
                throw new Exception("O ClienteId é obrigatorio");

            if (Qtde < 0)
                throw new Exception("A quantidade do produto não pode ser negativa");
        }
    }
}
