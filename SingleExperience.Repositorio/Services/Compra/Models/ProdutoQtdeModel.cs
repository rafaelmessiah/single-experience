namespace SingleExperience.Services.Compra.Models
{
    public class ProdutoQtdeModel
    {
        public int CarrinhoId { get; set; }
        public int ProdutoId { get; set; }
        public decimal Preco { get; set; }
        public int Qtde { get; set; }
    }
}
