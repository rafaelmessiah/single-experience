using SingleExperience.Entities.BD;
using SingleExperience.Services.ListaProdutoCompra.Models;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using SingleExperience.Services.Produto;

namespace SingleExperience.Services.ListaProdutoCompra
{
    public class ListaProdutoCompraService
    {
        ListaProdutoCompraBD listaProdutoCompraBd = new ListaProdutoCompraBD();

        ProdutoService produtoService = new ProdutoService();

        public List<ItemModel> BuscarProdutos(int compraId)
        {
            var itens = listaProdutoCompraBd.BuscarProdutosCompras()
                .Where(a => a.CompraId == compraId)
                .Select(b => new ItemModel
                {
                    ListaProdutoCompraId = b.ListaProdutoCompraId,
                    ProdutoId = b.ProdutoId,
                    Qtde = b.Qtde

                }).ToList();

            var produtos = produtoService.Buscar()
                .Where(a => itens.Any(b => b.ProdutoId == a.ProdutoId)).ToList();

            foreach (var item in itens)
            {
                var produto = produtos.Find(a => a.ProdutoId == item.ProdutoId);

                item.Nome = produto.Nome;
                item.PrecoUnitario = produto.Preco;
            }

            return itens;
        }
    }
}
