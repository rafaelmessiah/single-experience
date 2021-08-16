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
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

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

        public async Task<List<ItemCompraModel>> Buscar(int clienteId)
        {
            return await _context.Compra
            .Where(a => a.ClienteId == clienteId)
            .Select(b => new ItemCompraModel
            {
                CompraId = b.CompraId,
                StatusCompra = b.StatusCompraEnum,
                DataCompra = b.DataCompra,
                ValorFinal = b.ValorFinal
            }).ToListAsync();

        }

        public async Task<bool> Cadastrar(IniciarModel model)
        {
            using (var transaction = await _context.BeginTransactionAsync())
            {
                try
                {
                    //Buscar os Produtos que estao no carrinho, seus preços e suas quantidaes
                    var produtosCarrinho = await _carrinhoService.BuscarQtde(model.ClienteId);

                    if (produtosCarrinho.Count() == 0)
                        throw new Exception("Esse cliente não tem produtos no carriho");

                    //Calcula o Valor Total da compra  com base nos produtosCompra
                    //Faz a lista de produtos comprados com base nos produtos do carrinho
                    var valorFinal = 0.0m;
                    var produtosCompra = new List<Entities.ListaProdutoCompra>();

                    produtosCarrinho.ForEach(a =>
                    {
                        valorFinal += a.Preco * a.Qtde;

                        var produtoCompra = new Entities.ListaProdutoCompra
                        {
                            ProdutoId = a.ProdutoId,
                            Qtde = a.Qtde
                        };

                        produtosCompra.Add(produtoCompra);

                    });

                    var compra = new Entities.Compra
                    {
                        StatusCompraEnum = StatusCompraEnum.Aberta,
                        FormaPagamentoEnum = model.FormaPagamentoEnum,
                        ClienteId = model.ClienteId,
                        EnderecoId = model.EnderecoId,
                        ListaProdutoCompras = produtosCompra,
                        DataCompra = DateTime.Now,
                        ValorFinal = valorFinal,
                    };

                    await _context.Compra.AddAsync(compra);

                    //Para cada produto Comprado retira a quantidade do estoque e Altera o status do carrinho
                    produtosCarrinho.ForEach(async a =>
                    {
                        //Altera a quantide de produtos no estoque
                        var alterarQtdeModel = new AlterarQtdeModel
                        {
                            ProdutoId = a.ProdutoId,
                            Qtde = a.Qtde
                        };

                        await _produtoService.Retirar(alterarQtdeModel);

                        //Altera status do carrinho para Comprado
                        var edicaoStatusModel = new EdicaoStatusModel
                        {
                            CarrinhoId = a.CarrinhoId,
                            StatusEnum = StatusCarrinhoProdutoEnum.Comprado
                        };

                        await _carrinhoService.AlterarStatus(edicaoStatusModel);
                    });

                    await _context.SaveChangesAsync();

                    transaction.Commit();
                }
                catch (Exception e)
                {
                    await transaction.RollbackAsync();
                    throw new Exception(e.ToString());
                }
                
            }
            return true;
        }

        public async Task<CompraDetalhadaModel> Obter(int compraId)
        {
            var compra = await _context.Compra
             .Where(a => a.CompraId == compraId)
             .Select(b => new CompraDetalhadaModel
             {
                 CompraId = b.CompraId,
                 FormaPagamentoId = b.FormaPagamentoEnum,
                 DataCompra = b.DataCompra,
                 DataPagamento = b.DataPagamento,
                 ValorTotal = b.ValorFinal

             }).FirstOrDefaultAsync();

            if (compra == null)
                throw new Exception("Compra Não Encontrada");

            return compra;
        }

        public async Task<bool> Verificar(VerificarCompraModel model)
        {
            return _context.Compra.Any(a => a.CompraId == model.CompraId &&
            a.ClienteId == model.ClienteId);

        }

        public async Task<bool> Pagar(int compraId)
        {

            var compra = await _context.Compra
             .Where(a => a.CompraId == compraId &&
                    a.StatusCompraEnum == StatusCompraEnum.Aberta)
             .FirstOrDefaultAsync();

            if (compra == null)
                throw new Exception("Não foi possivel encontrar essa compra");

            compra.DataPagamento = DateTime.Now;

            _context.Compra.Update(compra);
            await _context.SaveChangesAsync();

            return true;

        }

    }
}
