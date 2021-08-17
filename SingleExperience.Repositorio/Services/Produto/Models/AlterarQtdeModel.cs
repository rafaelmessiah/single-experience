using DataAnnotationsExtensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SingleExperience.Services.Produto.Models
{
    public class AlterarQtdeModel
    {
        public int ProdutoId { get; set; }
        public int Qtde { get; set; }

        public void Validar()
        {
            if (ProdutoId < 0)
                throw new Exception("O produto Id é obrigatorio para essa requisição");

            if (Qtde < 0)
                throw new Exception("A quantidade do produto não pode ser negativa");
        }
    }
}
