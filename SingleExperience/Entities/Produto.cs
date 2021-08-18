using SingleExperience.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SingleExperience.Entities
{
    public class Produto
    {
        [Key]
        public int ProdutoId { get; set; }

        //FK - Categoria
        [Column("CategoriaId")]
        public CategoriaEnum CategoriaEnum { get; set; }
        [ForeignKey("CategoriaEnum")]
        public Categoria Categoria { get; set; }

        public string Nome { get; set; }
        public decimal Preco { get; set; }
        public string Detalhe { get; set; }
        public int QtdeEmEstoque { get; set; }
        public int Ranking { get; set; }
        public bool Disponivel { get; set; }
    }
}
