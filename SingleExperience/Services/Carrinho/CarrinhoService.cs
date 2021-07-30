using System;
using System.Collections.Generic;
using System.Text;
using SingleExperience.Entities.BD;
using SingleExperience.Entities.Enums;
using System.Linq;
using SingleExperience.Services.Carrinho.Models;
using SingleExperience.Services.Produto.Models;
using SingleExperience.Services.Produto;
namespace SingleExperience.Services.CarrinhoService
{
    public class CarrinhoService
    {
        CarrinhoBD carrinhoBd = new CarrinhoBD();

        ProdutoService produtoService = new ProdutoService();

        public List<ItemModel> BuscarItens(int clienteId)
        {

            var carrinhos = carrinhoBd.BuscarCarrinho()
                .Where(a => a.ClienteId == clienteId && a.StatusCarrinhoProdutoId == StatusCarrinhoProdutoEnum.Ativo)
                .ToList();

            if (carrinhos == null)
                throw new Exception("Esse cliente não tem Produtos no carrinho");

            var produtos = produtoService.Buscar()
                .Where(a => carrinhos.Any(b => b.ProdutoId == a.ProdutoId))
                .Select(c => new ItemModel
                {
                    ProdutoId = c.ProdutoId,
                    Nome = c.Nome,
                    Preco = c.Preco
                }).ToList();

            foreach( var item in produtos)
            {
                var carrinho = carrinhos.Find(a => a.ProdutoId == item.ProdutoId);

                item.CarrinhoId = carrinho.CarrinhoId;
            }

            return produtos;
        }

        public bool Adicionar(SalvarModel model)
        {
            var carrinho = carrinhoBd.BuscarCarrinho()
                .Where(a => a.ProdutoId == model.ProdutoId &&
                a.ClienteId == model.ClienteId &&
                a.StatusCarrinhoProdutoId == StatusCarrinhoProdutoEnum.Ativo)
                .FirstOrDefault();

            if (carrinho == null)
            {
                carrinhoBd.Salvar(model);

                return true;
            }

            throw new Exception("Esse produto ja esta no carrinho");

        }

        public bool Alterar(EdicaoModel model)
        {
            var carrinho = carrinhoBd.BuscarCarrinho()
                .Where(a => a.CarrinhoId == model.CarrinhoId &&
                a.StatusCarrinhoProdutoId != model.StatusEnum)
                .FirstOrDefault();

            if (carrinho == null)
                throw new Exception("Esse produto não pode ser alterado para o estado" + model.StatusEnum.ToString());

            carrinhoBd.Alterar(model);

            return true;

        }
    }
}
