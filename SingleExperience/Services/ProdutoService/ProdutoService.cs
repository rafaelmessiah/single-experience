using System;
using System.Collections.Generic;
using System.Text;
using SingleExperience.Services.ProdutoService.Models;
using SingleExperience.Entities.BD;
using System.Linq;


namespace SingleExperience.Services.ProdutoService
{
    class ProdutoService
    {
        public List<ListaProdutoRankingModel> ListarProdutoPorRanking()
        {
            var ProdutoBd = new ProdutoBD();
            var produtosDoBanco = ProdutoBd.ListarProdutos();

            var produtosFiltrados = new List<ListaProdutoRankingModel>();

            produtosDoBanco.FindAll(p => p.Disponivel == true)
                .ForEach(p =>
                {
                    var produtoFiltrado = new ListaProdutoRankingModel();

                    produtoFiltrado.ProdutoId = p.ProdutoId;
                    produtoFiltrado.Nome = p.Nome;
                    produtoFiltrado.Preco = p.Preco;

                    produtosFiltrados.Add(produtoFiltrado);
                });

            return produtosFiltrados;


       }

        public DescricaoProdutoModel DetalheProduto(int produtoId)
        {
            var produtoBd = new ProdutoBD();

            var produtosDoBanco = produtoBd.ListarProdutos();

            var produtoEcontrado = produtosDoBanco.Find(p => p.ProdutoId == produtoId);
                

            var produtoFinal = new DescricaoProdutoModel();

            produtoFinal.ProdutoId = produtoEcontrado.ProdutoId;
            produtoFinal.Descricao = produtoEcontrado.Detalhe;

            return produtoFinal;

        }
    }
}
