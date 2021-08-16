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
            if (Qtde < 0)
            {
                throw new Exception("Quantidade não pode ser negativa");
            }
        }
    }
}
