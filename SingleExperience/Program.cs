using System;
using SingleExperience.Entities.BD;
using System.Linq;
using SingleExperience.Services.ProdutoService;
using SingleExperience.Services.ProdutoService.Models;
using System.Globalization;

namespace SingleExperience
{
    class Program
    {
        static void Main(string[] args)
        {

            var produtoService = new ProdutoService();

            var listaProdutos = produtoService.ListarProdutoPorRanking();

            listaProdutos.ForEach(p =>
            {
                Console.WriteLine(p.ProdutoId+" - "+p.Nome + " preço: " + p.Preco.ToString("F2", CultureInfo.InvariantCulture));
            });

            Console.WriteLine("Digite o produto que ver o detalher");
            var produtoEscolhido = produtoService.DetalheProduto(int.Parse(Console.ReadLine()));

            Console.WriteLine(produtoEscolhido.ProdutoId+"  "+produtoEscolhido.Descricao);
        }
    }
}
