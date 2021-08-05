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

namespace SingleExperience.Services.Compra
{
    public class CompraService
    {
        CompraBD compraBd = new CompraBD();
        CarrinhoService carrinhoService = new CarrinhoService();
        ListaProdutoCompraService listaProdutoCompraService = new ListaProdutoCompraService();
        ProdutoService produtoService = new ProdutoService();

        public List<ItemCompraModel> Buscar(int clienteId)
        {
            try
            {
                var compras = compraBd.BuscarCompras()
                .Where(a => a.ClienteId == clienteId)
                .Select(b => new ItemCompraModel
                {
                    CompraId = b.CompraId,
                    StatusCompra = b.StatusCompraId,
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
            //Buscar os Produtos
            var produtos = carrinhoService.Buscar(model.ClienteId);

            var cadastroModel = new CadastroModel
            {
                ClienteId = model.ClienteId,
                FormaPagamentoId = model.FormaPagamentoId,
                ValorFinal = carrinhoService.CalcularValorTotal(model.ClienteId)
            };

            var compraId = compraBd.Salvar(cadastroModel);

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
            var compra = compraBd.BuscarCompras()
                .Where(a => a.CompraId == compraId)
                .Select(b => new CompraDetalhadaModel
                {
                    CompraId = b.CompraId,
                    StatusPagamentoId = b.StatusPagamentoId,
                    FormaPagamentoId = b.FormaPagamentoId,
                    DataCompra = b.DataCompra,
                    DataPagamento = b.DataPagamento,
                    ValorTotal = b.ValorFinal

                }).FirstOrDefault();

            if (compra == null)
                throw new Exception("Compra Não Encontrada");

            return compra;
        }
        public bool Pagar(int compraId)
        {
            var compra = compraBd.BuscarCompras()
                 .Where(a => a.CompraId == compraId &&
                 a.StatusCompraId == StatusCompraEnum.Aberta &&
                 a.StatusPagamentoId == StatusPagamentoEnum.NaoConfirmado)
                 .FirstOrDefault();

            if (compra == null)
                throw new Exception("Não foi possivel econtrar essa compra");

            compraBd.Pagar(compra.CompraId);

            return true;
              
        }


    }
}
