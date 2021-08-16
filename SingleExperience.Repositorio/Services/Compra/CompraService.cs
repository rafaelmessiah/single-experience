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
        protected readonly SingleExperience.Context.SeContext _context;

        public CompraService(SingleExperience.Context.SeContext context)
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

            //Buscar os Produtos que estao no carrinho
            var carrinhoService = new CarrinhoService(_context);
            var produtosDoCarrinho = carrinhoService.Buscar(model.ClienteId);

            if (produtosDoCarrinho.Count == 0)
                throw new Exception("Não há produtos no carrinho");

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

            var compra = new Entities.Compra
            {

                StatusCompraEnum = StatusCompraEnum.Aberta,
                FormaPagamentoEnum = model.FormaPagamentoEnum,
                ClienteId = model.ClienteId,
                EnderecoId = model.EnderecoId,
                ListaProdutoCompras = produtosComprados,
                DataCompra = DateTime.Now,
                ValorFinal = ((decimal)carrinhoService.CalcularValorTotal(model.ClienteId)),
            };

            _context.Compra.Add(compra);

            var produtoService = new ProdutoService(_context);
            produtosDoCarrinho.ForEach(a =>
            {
                //Altera a quantide de produtos no carrinho
                var alterarQtdeModel = new AlterarQtdeModel
                {
                    ProdutoId = a.ProdutoId,
                    Qtde = a.Qtde
                };

                produtoService.Retirar(alterarQtdeModel);

                //Altera status do carrinho para Comprado
                var edicaoStatusModel = new EdicaoStatusModel
                {
                    CarrinhoId = a.CarrinhoId,
                    StatusEnum = StatusCarrinhoProdutoEnum.Comprado,
                };

                carrinhoService.AlterarStatus(edicaoStatusModel);
            });

            _context.SaveChanges();

            return true;
        }

        public CompraDetalhadaModel Obter(int compraId)
        {
            var compra = _context.Compra
             .Where(a => a.CompraId == compraId)
             .Select(b => new CompraDetalhadaModel
             {
                 CompraId = b.CompraId,
                 FormaPagamentoId = b.FormaPagamentoEnum,
                 DataCompra = b.DataCompra,
                 DataPagamento = b.DataPagamento,
                 ValorTotal = b.ValorFinal

             }).FirstOrDefault();

            if (compra == null)
                throw new Exception("Compra Não Encontrada");

            return compra;
        }

        public bool Verificar(VerificarCompraModel model)
        {
            return _context.Compra.Any(a => a.CompraId == model.CompraId &&
            a.ClienteId == model.ClienteId);

        }

        public bool Pagar(int compraId)
        {

            var compra = _context.Compra
             .Where(a => a.CompraId == compraId &&
                    a.StatusCompraEnum == StatusCompraEnum.Aberta)
             .FirstOrDefault();

            if (compra == null)
                throw new Exception("Não foi possivel econtrar essa compra");

            compra.DataPagamento = DateTime.Now;

            _context.Compra.Update(compra);
            _context.SaveChanges();

            return true;

        }

    }
}
