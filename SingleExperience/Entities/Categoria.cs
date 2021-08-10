using SingleExperience.Entities.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SingleExperience.Entities
{
    public class Categoria
    {
        [Key]
        [Column("CategoriaId")]
        public CategoriaEnum CategoriaEnum { get; set; }
        public string Descricao { get; set; }
    }
}
