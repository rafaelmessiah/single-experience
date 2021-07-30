using System;
using System.Collections.Generic;
using System.Text;
using SingleExperience.Entities.BD;
using SingleExperience.Entities.Enums;
using System.Linq;


namespace SingleExperience.Services.CarrinhoService
{
    class CarrinhoService
    {
        CarrinhoBD carrinhoBd = new CarrinhoBD();

        public bool Adicionar(int produtoId, int clienteId)
        {
            var carrinho = carrinhoBd.ListarCarrinho()
                .Where(a => a.Produtoid == produtoId &&
                a.ClienteId == clienteId &&
                a.StatusCarrinhoProdutoId == StatusCarrinhoProdutoEnum.Inserido)
                .FirstOrDefault();

            if (carrinho == null)
            {
                carrinhoBd.Salvar(clienteId, produtoId);

                return true;
            }

            throw new Exception("Esse produto ja esta no carrinho");

        }

        public bool Alterar(int produtoId, int clientId, StatusCarrinhoProdutoEnum statusCarrinhoProdutoId)
        {
            var carrinho = carrinhoBd.ListarCarrinho()
                .Where(a => a.ClienteId == clientId &&
                a.Produtoid == produtoId &&
                a.StatusCarrinhoProdutoId != statusCarrinhoProdutoId)
                .FirstOrDefault();

            if (carrinho == null)
                throw new Exception("Esse produto não pode ser alterado para o estado" + statusCarrinhoProdutoId.ToString());



        }
    }
}
