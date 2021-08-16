using System;
using System.Collections.Generic;
using System.Text;
using SingleExperience.Entities.Enums;
using System.Linq;
using SingleExperience.Services.Carrinho.Models;
using SingleExperience.Services.Produto.Models;
using SingleExperience.Services.Produto;
using System.IO;
using Microsoft.EntityFrameworkCore;
using System.Threading;

namespace SingleExperience.Services.Carrinho
{
    public class CarrinhoService
    {
        protected readonly SingleExperience.Context.SeContext _context;

        public CarrinhoService(SingleExperience.Context.SeContext context)
        {
            _context = context;
        }

        public List<ItemCarrinhoModel> Buscar(int clienteId)
        {
                return _context.Carrinho
                .Include(a => a.Produto)
                .Where(a => a.ClienteId == clienteId && a.StatusCarrinhoProdutoEnum == StatusCarrinhoProdutoEnum.Ativo)
                .Select(a => new ItemCarrinhoModel
                {
                    CarrinhoId = a.CarrinhoId,
                    ProdutoId = a.ProdutoId,
                    Nome = a.Produto.Nome,
                    Preco = a.Produto.Preco,
                    Qtde = a.Qtde
                }).ToList();

        }

        public bool Adicionar(SalvarModel model)
        {
                model.Validar();

                var carrinho = _context.Carrinho
                .Where(a => a.ProdutoId == model.ProdutoId &&
                        a.ClienteId == model.ClienteId &&
                        a.StatusCarrinhoProdutoEnum == StatusCarrinhoProdutoEnum.Ativo)
                .FirstOrDefault();

                if (carrinho != null)
                    throw new Exception("Esse produto já esta no seu carrinho carrinho");

                var produto = new Entities.Carrinho
                {
                    ProdutoId = model.ProdutoId,
                    ClienteId = model.ClienteId,
                    Qtde = model.Qtde,
                    StatusCarrinhoProdutoEnum = StatusCarrinhoProdutoEnum.Ativo,
                };

                _context.Carrinho.Add(produto);
                _context.SaveChanges();

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
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                Thread.Sleep(3500);
            }

            return true;
        }

        public bool AlterarQtde(EdicaoQtdeModel model)
        {
            try
            {
                model.Validar();

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
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                Thread.Sleep(3000);
            }
            
            return true;
        }

        public decimal CalcularValorTotal(int clienteId)
        {
            var valorTotal = 0.00m;
            try
            {
                var produtos = Buscar(clienteId);

                if (produtos == null)
                {
                    throw new Exception("Não foi possivel buscar os produtos desse cliente");
                }

                produtos.ForEach(a =>
                {
                    valorTotal += a.Preco * a.Qtde;
                });

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                Thread.Sleep(3000);
            }

            return valorTotal;
        }
    }
}
