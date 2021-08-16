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
        protected readonly CarrinhoService _carrinhoService;
        protected readonly ProdutoService _produtoService;


        public CompraService(SingleExperience.Context.Context context)
        {
            _context = context;
            _carrinhoService = new CarrinhoService(_context);
            _produtoService = new ProdutoService(_context);
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
            var produtosComprados = _context.Carrinho
                .Where(a => a.ClienteId == model.ClienteId)
                .Select(a => new Entities.ListaProdutoCompra
                {
                    ProdutoId = a.ProdutoId,
                    Qtde = a.Qtde
                }).ToList();

            var compra = new Entities.Compra
            {
                StatusCompraEnum = StatusCompraEnum.Aberta,
                FormaPagamentoEnum = model.FormaPagamentoEnum,
                ClienteId = model.ClienteId,
                EnderecoId = model.EnderecoId,
                ListaProdutoCompras = produtosComprados,
                DataCompra = DateTime.Now,
                ValorFinal = _carrinhoService.CalcularValorTotal(model.ClienteId).Result,
            };

            _context.Compra.Add(compra);
            
            //Para cada produto Comprado retira a quantidade do estoque e Altera o status do carrinho
            produtosComprados.ForEach(a =>
            {
                //Altera a quantide de produtos no estoque
                var produto = _context.Produto
                .Where(b => b.ProdutoId == a.ProdutoId).FirstOrDefault();

                produto.QtdeEmEstoque -= a.Qtde;

                _context.Produto.Update(produto);

                //Altera status do carrinho para Comprado
                var carrinho = _context.Carrinho
                .Where(c => c.ProdutoId == a.ProdutoId &&
                       c.ClienteId == model.ClienteId &&
                       c.StatusCarrinhoProdutoEnum == StatusCarrinhoProdutoEnum.Ativo)
                .FirstOrDefault();

                carrinho.StatusCarrinhoProdutoEnum = StatusCarrinhoProdutoEnum.Comprado;

                _context.Carrinho.Update(carrinho);
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
