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

namespace SingleExperience.Services.Compra
{
    public class CompraService
    {
        protected readonly SingleExperience.Context.Context _context;
        CompraBD compraBd = new CompraBD();
        CarrinhoService carrinhoService = new CarrinhoService();
        ListaProdutoCompraService listaProdutoCompraService = new ListaProdutoCompraService();
        ProdutoService produtoService = new ProdutoService();

        public CompraService()
        {
        }

        public CompraService(SingleExperience.Context.Context context)
        {
            _context = context;
        }

        public List<ItemCompraModel> Buscar(int clienteId)
        {
            try
            {
                var compras = _context.Compra
                .Where(a => a.ClienteId == clienteId)
                .Select(b => new ItemCompraModel
                {
                    CompraId = b.CompraId,
                    StatusCompra = b.StatusCompraEnum,
                    DataCompra = b.DataCompra,
                    ValorFinal = b.ValorFinal
                }).ToList();

                return compras;
            }
            catch (Exception)
            {
                 return null;
            }
        }
        public bool Cadastrar(IniciarModel model)
        {
            //Buscar os Produtos que estao no carrinho
            var produtos = carrinhoService.Buscar(model.ClienteId);

            var compra = new Entities.Compra
            {

                StatusCompraEnum = StatusCompraEnum.Aberta,
                FormaPagamentoEnum = model.FormaPagamentoEnum,
                ClienteId = model.ClienteId,
                EnderecoId = model.EnderecoId,
                StatusPagamento = false,
                DataCompra = DateTime.Now,
                ValorFinal = carrinhoService.CalcularValorTotal(model.ClienteId),
            };

            _context.Compra.Add(compra);
            _context.SaveChanges();

            //var compraId = compraBd.Salvar(cadastroModel);

            var itemCompraModel = new CadastrarItemModel();
 
            produtos.ForEach(a =>
            {
                //Altera a quantidade do produto
                var alterarQtdeModel = new AlterarQtdeModel
                {
                    ProdutoId = a.ProdutoId,
                    Qtde = a.Qtde
                };

                produtoService.Retirar(alterarQtdeModel);

                //Altera status do carrinho para Comprado
                var editarStatusModel = new EdicaoStatusModel
                {
                    CarrinhoId = a.CarrinhoId,
                    StatusEnum = StatusCarrinhoProdutoEnum.Comprado
                };

                carrinhoService.AlterarStatus(editarStatusModel);

                itemCompraModel.ProdutoId = a.ProdutoId;
                itemCompraModel.Qtde = a.Qtde;
                itemCompraModel.CompraId = compraId;

                //Cadastra o produto na Lista de Vendidos
                listaProdutoCompraService.CadastrarVenda(itemCompraModel);
            });

            return true;
        }
        public CompraDetalhadaModel Obter(int compraId)
        {
            var compra = _context.Compra
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
            catch (IOException e)
            {
                Console.WriteLine("Ocorreu um erro");
                Console.WriteLine(e.Message);
            }

            return true;
        }
        public bool Pagar(int compraId)
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

            _context.Compra.Add(compra);
            _context.SaveChanges();

            return true;
              
        }

    }
}
