using SingleExperience.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SingleExperience.Entities
{
    public class StatusCompra
    {
        [Key]
        [Column("StatusCompraId")]
        public StatusCompraEnum StatusCompraEnum { get; set; }
        public string Descricao { get; set; }
    }
}
