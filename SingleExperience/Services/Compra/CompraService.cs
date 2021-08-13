using SingleExperience.Entities.BD;
using SingleExperience.Services.Compra.Models;
using SingleExperience.Services.Carrinho;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using SingleExperience.Entities.Enums;
using SingleExperience.Services.ListaProdutoCompra;
using SingleExperience.Services.ListaProdutoCompra.Models;
using SingleExperience.Services.Carrinho.Models;
using SingleExperience.Services.Produto;
using SingleExperience.Services.Produto.Models;
using System.IO;
using System.Threading;

namespace SingleExperience.Services.Compra
{
    public class CompraService
    {
        protected readonly SingleExperience.Context.Context _context;

        public CompraService(SingleExperience.Context.Context context)
        {
            _context = context;
        }

        public List<ItemCompraModel> Buscar(int clienteId)
        {
            return _context.Compra
            .Where(a => a.ClienteId == clienteId)
            .Select(b => new ItemCompraModel
            {
                CompraId = b.CompraId,
                StatusCompra = b.StatusCompraEnum,
                DataCompra = b.DataCompra,
                ValorFinal = b.ValorFinal
            }).ToList();

        }
        public bool Cadastrar(IniciarModel model)
        {
            try
            {
                //Buscar os Produtos que estao no carrinho
                var produtosDoCarrinho = _context.Carrinho
                    .Where(a => a.ClienteId == model.ClienteId)
                    .ToList();

                var produtosComprados = new List<Entities.ListaProdutoCompra>();

                foreach (var item in produtosDoCarrinho)
                {
                    var itemComprado = new Entities.ListaProdutoCompra
                    {
                        ProdutoId = item.ProdutoId,
                        Qtde = item.Qtde
                    };

                    produtosComprados.Add(itemComprado);
                }

                var carrinhoService = new CarrinhoService(_context);

                var compra = new Entities.Compra
                {

                    StatusCompraEnum = StatusCompraEnum.Aberta,
                    FormaPagamentoEnum = model.FormaPagamentoEnum,
                    ClienteId = model.ClienteId,
                    EnderecoId = model.EnderecoId,
                    ListaProdutoCompras = produtosComprados,
                    StatusPagamento = false,
                    DataCompra = DateTime.Now,
                    ValorFinal = carrinhoService.CalcularValorTotal(model.ClienteId),
                };

                _context.Compra.Add(compra);
                _context.SaveChanges();

                produtosDoCarrinho.ForEach(a =>
                {
                    //Altera a quantidade do produto
                    var produto = _context.Produto
                    .Where(b => b.ProdutoId == a.ProdutoId)
                    .FirstOrDefault();

                    produto.QtdeEmEstoque -= a.Qtde;

                    _context.Produto.Update(produto);

                    //Altera status do carrinho para Comprado
                    var carrinho = _context.Carrinho
                    .Where(b => b.CarrinhoId == a.CarrinhoId)
                    .FirstOrDefault();

                    carrinho.StatusCarrinhoProdutoEnum = StatusCarrinhoProdutoEnum.Comprado;

                    _context.Carrinho.Update(carrinho);
                    _context.SaveChanges();

                });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                Thread.Sleep(3000);
            }

            return true;
        }
        public CompraDetalhadaModel Obter(int compraId)
        {
            var compra = new CompraDetalhadaModel();
            try
            {
                compra = _context.Compra
                .Where(a => a.CompraId == compraId)
                .Select(b => new CompraDetalhadaModel
                {
                    CompraId = b.CompraId,
                    StatusPagamento = b.StatusPagamento,
                    FormaPagamentoId = b.FormaPagamentoEnum,
                    DataCompra = b.DataCompra,
                    DataPagamento = b.DataPagamento,
                    ValorTotal = b.ValorFinal

                }).FirstOrDefault();

                if (compra == null)
                    throw new Exception("Compra Não Encontrada");

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                Thread.Sleep(3000);
            }

            return compra;
        }
        public bool Verificar(VerificarCompraModel model)
        {
            try
            {
                var compra = _context.Compra
                    .Where(a => a.CompraId == model.CompraId && a.ClienteId == model.ClienteId)
                    .FirstOrDefault();

                if (compra == null)
                    return false;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                Thread.Sleep(3000);
            }

            return true;
        }
        public bool Pagar(int compraId)
        {
            try
            {
                var compra = _context.Compra
                 .Where(a => a.CompraId == compraId &&
                        a.StatusCompraEnum == StatusCompraEnum.Aberta &&
                        a.StatusPagamento == false)
                 .FirstOrDefault();

                if (compra == null)
                    throw new Exception("Não foi possivel econtrar essa compra");

                compra.StatusPagamento = true;
                compra.DataPagamento = DateTime.Now;

                _context.Compra.Update(compra);
                _context.SaveChanges();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                Thread.Sleep(3000);
            }

            return true;

        }

    }
}
