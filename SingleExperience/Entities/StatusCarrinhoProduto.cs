using SingleExperience.Entities.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

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
