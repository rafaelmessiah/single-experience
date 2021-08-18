using DataAnnotationsExtensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SingleExperience.Services.Carrinho.Models
{
    public class EdicaoQtdeModel
    {
        public int CarrinhoId { get; set; }
        public int Qtde { get; set; }

        public void Validar()
        {
            if (CarrinhoId < 1)
                throw new Exception("A carrinho id é obrigatorio para essa");

            if (Qtde < 0)
                throw new Exception("A quantidade não pode ser negativa");
        }
    }
}
