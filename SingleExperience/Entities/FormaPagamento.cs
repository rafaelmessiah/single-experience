using SingleExperience.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SingleExperience.Entities
{
    public class FormaPagamento
    {
        [Key]
        [Column("FormaPagamentoId")]
        public FormaPagamentoEnum FormaPagamentoEnum { get; set; }
        public string Descricao { get; set; }
    }
}
