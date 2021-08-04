using SingleExperience.Entities.BD;
using SingleExperience.Services.ListaProdutoCompra.Models;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using SingleExperience.Services.Produto;
using System;

namespace SingleExperience.Services.ListaProdutoCompra
{
    public class ListaProdutoCompraService
    {
        ListaProdutoCompraBD listaProdutoCompraBd = new ListaProdutoCompraBD();
        ProdutoService produtoService = new ProdutoService();

        public List<ItemProdutoCompraModel> Buscar(int compraId)
        {
            var itens = listaProdutoCompraBd.BuscarProdutosCompras()
                .Where(a => a.CompraId == compraId)
                .Select(b => new ItemProdutoCompraModel
                {
                    ListaProdutoCompraId = b.ListaProdutoCompraId,
                    CompraId = b.CompraId,
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

        public bool CadastrarVenda(CadastrarItemModel model)
        {
            var item = Buscar(model.CompraId)
                .Where(a => a.ProdutoId == model.ProdutoId &&
                a.CompraId == model.CompraId)
                .FirstOrDefault();

            if(item != null)
                throw new Exception("Esse produto ja esta Associado a essa compra");

            try
            {
                listaProdutoCompraBd.Salvar(model);
            }
            catch (Exception)
            {
                throw new Exception("Não foi possível cadastrar esse produto nesta compra");
            }

            return true;
        }
    }
}
