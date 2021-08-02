using System;
using System.Collections.Generic;
using System.Text;
using SingleExperience.Entities.BD;
using SingleExperience.Entities.Enums;
using System.Linq;
using SingleExperience.Services.Carrinho.Models;
using SingleExperience.Services.Produto.Models;
using SingleExperience.Services.Produto;
using System.IO;

namespace SingleExperience.Services.Carrinho
{
    public class CarrinhoService
    {
        CarrinhoBD carrinhoBd = new CarrinhoBD();

        ProdutoService produtoService = new ProdutoService();

        public List<ItemCarrinhoModel> BuscarItens(int clienteId)
        {

            var carrinhos = carrinhoBd.BuscarCarrinho()
                .Where(a => a.ClienteId == clienteId && a.StatusCarrinhoProdutoId == StatusCarrinhoProdutoEnum.Ativo)
                .ToList();

            if (carrinhos == null)
                throw new Exception("Esse cliente não tem Produtos no carrinho");

            var produtos = produtoService.Buscar()
                .Where(a => carrinhos.Any(b => b.ProdutoId == a.ProdutoId))
                .Select(c => new ItemCarrinhoModel
                {
                    ProdutoId = c.ProdutoId,
                    Nome = c.Nome,
                    Preco = c.Preco
                }).ToList();

            foreach( var item in produtos)
            {
                var carrinho = carrinhos.Find(a => a.ProdutoId == item.ProdutoId);

                item.CarrinhoId = carrinho.CarrinhoId;
                item.Qtde = carrinho.Qtde;
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
                try
                {
                    carrinhoBd.Salvar(model);
                }
                catch (IOException e)
                {
                    Console.WriteLine("Ocurred an error");
                    Console.WriteLine(e.Message);
                }

                return true;
            }

            throw new Exception("Esse produto ja esta no carrinho");

        }

        public bool AlterarStatus(EdicaoStatusModel model)
        {
            var carrinho = carrinhoBd.BuscarCarrinho()
                .Where(a => a.CarrinhoId == model.CarrinhoId &&
                a.StatusCarrinhoProdutoId != model.StatusEnum)
                .FirstOrDefault();

            if (carrinho == null)
                throw new Exception("Esse produto não pode ser alterado para o estado" + model.StatusEnum.ToString());

            carrinhoBd.AlterarStatus(model);

            return true;

        }

        public bool AlterarQtde(EdicaoQtdeModel model)
        {

            var carrinho = carrinhoBd.BuscarCarrinho()
                .Where(a => a.CarrinhoId == model.CarrinhoId &&
                a.Qtde != model.Qtde &&
                model.Qtde > 0 &&
                a.StatusCarrinhoProdutoId == StatusCarrinhoProdutoEnum.Ativo)
                .FirstOrDefault();

            if (carrinho == null)
                throw new Exception("Esse produto não pode ser alterado para essa quantidade ");

            carrinhoBd.AlterarQtde(model);

            return true;
        }

        public double CalcularValorTotal(int clienteId)
        {
            var produtos = BuscarItens(clienteId);

            if (produtos == null)
            {
                throw new Exception("Não foi possivel buscar os produtos desse cliente");
            }

            var valorTotal = 0.0;

            produtos.ForEach(a =>
            {
                valorTotal += a.Preco * a.Qtde;
            });

            return valorTotal;
        }
    }
}
