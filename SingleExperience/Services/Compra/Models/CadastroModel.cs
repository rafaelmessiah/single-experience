using SingleExperience.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace SingleExperience.Services.Compra.Models
{
    public class CadastroModel
    {
        public int ClienteId { get; set; }
        public decimal ValorFinal { get; set; }
        public int EnderecoId { get; set; }
        public FormaPagamentoEnum FormaPagamentoId { get; set; }
    }
}
