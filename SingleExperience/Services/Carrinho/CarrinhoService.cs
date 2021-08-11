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
using Microsoft.EntityFrameworkCore;

namespace SingleExperience.Services.Carrinho
{
    public class CarrinhoService
    {
        protected readonly SingleExperience.Context.Context _context;

        public CarrinhoService()
        {
        }

        public CarrinhoService(SingleExperience.Context.Context context)
        {
            _context = context;
        }

        public List<ItemCarrinhoModel> Buscar(int clienteId)
        {
            var produtos = new List<ItemCarrinhoModel>();

            try
            {
                var carrinhos = _context.Carrinho
                .Include(a => a.Produto)
                .Where(a => a.ClienteId == clienteId && a.StatusCarrinhoProdutoEnum == StatusCarrinhoProdutoEnum.Ativo);

                if (carrinhos == null)
                    throw new Exception("Esse cliente não tem Produtos no carrinho");

                produtos = carrinhos.Select(a => new ItemCarrinhoModel
                {
                    CarrinhoId = a.CarrinhoId,
                    ProdutoId = a.ProdutoId,
                    Nome = a.Produto.Nome,
                    Preco = a.Produto.Preco,
                    Qtde = a.Qtde
                }).ToList();

            }
            catch (IOException e)
            {
                Console.WriteLine("Ocorreu um erro");
                Console.WriteLine(e);
            }

            return produtos;
        }

        public bool Adicionar(SalvarModel model)
        {
            try
            {
                var carrinho = _context.Carrinho
                .Where(a => a.ProdutoId == model.ProdutoId &&
                        a.ClienteId == model.ClienteId &&
                        a.StatusCarrinhoProdutoEnum == StatusCarrinhoProdutoEnum.Ativo)
                .FirstOrDefault();


                if (carrinho != null)
                    throw new Exception(".....");

                var produto = new Entities.Carrinho
                {
                    ProdutoId = model.ProdutoId,
                    ClienteId = model.ClienteId,
                    Qtde = model.Qtde,
                    StatusCarrinhoProdutoEnum = StatusCarrinhoProdutoEnum.Ativo,
                };

                _context.Carrinho.Add(produto);
                _context.SaveChanges();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                // o que acontece?
            }

            return true;
        }

        public bool AlterarStatus(EdicaoStatusModel model)
        {
            try
            {
                var carrinho = _context.Carrinho
                    .Where(a => a.CarrinhoId == model.CarrinhoId &&
                            a.StatusCarrinhoProdutoEnum != model.StatusEnum)
                    .FirstOrDefault();

                if (carrinho == null)
                    throw new Exception("Esse produto não pode ser alterado para o estado" + model.StatusEnum.ToString());

                carrinho.StatusCarrinhoProdutoEnum = model.StatusEnum;

                _context.Carrinho.Update(carrinho);
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine("Ocorreu um erro");
                Console.WriteLine(e);
            }

            return true;
        }

        public bool AlterarQtde(EdicaoQtdeModel model)
        {

            var carrinho = _context.Carrinho
                .Where(a => a.CarrinhoId == model.CarrinhoId &&
                        a.Qtde != model.Qtde &&
                        model.Qtde > 0 &&
                        a.StatusCarrinhoProdutoEnum == StatusCarrinhoProdutoEnum.Ativo)
                .FirstOrDefault();

            if (carrinho == null)
                throw new Exception("Esse produto não pode ser alterado para essa quantidade ");

            carrinho.Qtde = model.Qtde;

            _context.Carrinho.Update(carrinho);
            _context.SaveChanges();

            return true;
        }

        public double CalcularValorTotal(int clienteId)
        {
            var produtos = Buscar(clienteId);

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
