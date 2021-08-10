using SingleExperience.Entities.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

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
