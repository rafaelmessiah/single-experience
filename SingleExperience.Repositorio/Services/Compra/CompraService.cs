using Microsoft.EntityFrameworkCore;
using SingleExperience.Enums;
using SingleExperience.Services.Carrinho;
using SingleExperience.Services.Carrinho.Models;
using SingleExperience.Services.CartaoCredito;
using SingleExperience.Services.CartaoCredito.Models;
using SingleExperience.Services.Compra.Models;
using SingleExperience.Services.Endereco;
using SingleExperience.Services.Endereco.Models;
using SingleExperience.Services.ListaProdutoCompra;
using SingleExperience.Services.Produto;
using SingleExperience.Services.Produto.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SingleExperience.Services.Compra
{
    public class CompraService
    {
        protected readonly SingleExperience.Context.Context _context;
        protected readonly CarrinhoService _carrinhoService;
        protected readonly ProdutoService _produtoService;
        protected readonly EnderecoService _enderecoService;
        protected readonly ListaProdutoCompraService _listaProdutoCompraService;
        protected readonly CartaoCreditoService _cartaoCreditoService;

        public CompraService(SingleExperience.Context.Context context)
        {
            _context = context;
            _carrinhoService = new CarrinhoService(context);
            _produtoService = new ProdutoService(context);
            _enderecoService = new EnderecoService(context);
            _listaProdutoCompraService = new ListaProdutoCompraService(context);
            _cartaoCreditoService = new CartaoCreditoService(context);
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
            model.Validar();

            using (var transaction = await _context.BeginTransactionAsync())
            {
                try
                {
                    var verificarEnderecoModel = new VerificarEnderecoModel
                    {
                        ClienteId = model.ClienteId,
                        EnderecoId = model.EnderecoId
                    };

                    //Valida o endereço
                    if (!await _enderecoService.Verificar(verificarEnderecoModel))
                        throw new Exception("Esse endereço não pertence a esse cliente");
                    
                    //Verifica se não esta sendo passado um cartaoCreditoId com outra forma de pagamento
                    if (model.FormaPagamentoEnum != FormaPagamentoEnum.Cartao && model.CartaoCreditoId != null)
                        throw new Exception("Não é possivel passar um cartão de credito quando a forma de pagamento não é por cartao");

                    //Valida o Cartao
                    if(model.FormaPagamentoEnum == FormaPagamentoEnum.Cartao)
                    {
                        var verificarCartaoModel = new VerificarCartaoModel
                        {
                            ClienteId = model.ClienteId,
                            CartaoCreditoId = model.CartaoCreditoId
                        };

                        if (!await _cartaoCreditoService.Verificar(verificarCartaoModel))
                            throw new Exception("Esse cartão não pertence a esse cliente");
                    }

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
                        CartaoCreditoId = model.CartaoCreditoId,
                        ListaProdutoCompras = produtosCompra,
                        DataCompra = DateTime.Now,
                        ValorFinal = valorFinal,
                    };

                    await _context.Compra.AddAsync(compra);
                    await _context.SaveChangesAsync();

                    //Para cada produto Comprado retira a quantidade do estoque e Altera o status do carrinho
                    foreach (var item in produtosCarrinho)
                    {
                        //Altera a quantide de produtos no estoque
                        var alterarQtdeModel = new AlterarQtdeModel
                        {
                            ProdutoId = item.ProdutoId,
                            Qtde = item.Qtde
                        };

                        await _produtoService.Retirar(alterarQtdeModel);

                        //Altera status do carrinho para Comprado
                        var edicaoStatusModel = new EdicaoStatusModel
                        {
                            StatusEnum = StatusCarrinhoProdutoEnum.Comprado
                        };

                        var carrinhoId = item.CarrinhoId;

                        await _carrinhoService.AlterarStatus(carrinhoId, edicaoStatusModel);
                    }

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

            compra.ItensComprados = await _listaProdutoCompraService.Buscar(compraId);

            return compra;
        }

        public async Task<bool> Verificar(VerificarCompraModel model)
        {
            return await _context.Compra.AnyAsync(a => a.CompraId == model.CompraId &&
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
