using SingleExperience.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SingleExperience.Entities
{
    public class StatusCarrinhoProduto
    {
        [Key]
        [Column("StatusCarrinhoProdutoId")]
        public StatusCarrinhoProdutoEnum StatusCarrinhoProdutoEnum { get; set; }
        public string Descricao { get; set; }
    }
}
