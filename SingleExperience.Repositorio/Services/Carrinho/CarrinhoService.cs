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
using System.Threading.Tasks;
using SingleExperience.Services.Compra.Models;

namespace SingleExperience.Services.Carrinho
{
    public class CarrinhoService
    {
        protected readonly SingleExperience.Context.Context _context;

        public CarrinhoService(SingleExperience.Context.Context context)
        {
            _context = context;
        }

        public async Task<List<ItemCarrinhoModel>> Buscar(int clienteId)
        {
            return await _context.Carrinho
            .Include(a => a.Produto)
            .Where(a => a.ClienteId == clienteId && a.StatusCarrinhoProdutoEnum == StatusCarrinhoProdutoEnum.Ativo)
            .Select(a => new ItemCarrinhoModel
            {
                CarrinhoId = a.CarrinhoId,
                ProdutoId = a.ProdutoId,
                Nome = a.Produto.Nome,
                Preco = a.Produto.Preco,
                Qtde = a.Qtde
            }).ToListAsync();

        }

        public async Task<bool> Adicionar(SalvarModel model)
        {
            model.Validar();

            var carrinho = await _context.Carrinho
            .Where(a => a.ProdutoId == model.ProdutoId &&
                    a.ClienteId == model.ClienteId &&
                    a.StatusCarrinhoProdutoEnum == StatusCarrinhoProdutoEnum.Ativo)
            .FirstOrDefaultAsync();

            if (carrinho != null)
                throw new Exception("Esse produto já esta no seu carrinho carrinho");

            var produto = new Entities.Carrinho
            {
                ProdutoId = model.ProdutoId,
                ClienteId = model.ClienteId,
                Qtde = model.Qtde,
                StatusCarrinhoProdutoEnum = StatusCarrinhoProdutoEnum.Ativo,
            };

            await _context.Carrinho.AddAsync(produto);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> AlterarStatus(EdicaoStatusModel model)
        {

            var carrinho = await _context.Carrinho
                .Where(a => a.CarrinhoId == model.CarrinhoId &&
                        a.StatusCarrinhoProdutoEnum != model.StatusEnum)
                .FirstOrDefaultAsync();

            if (carrinho == null)
                throw new Exception("Esse produto não pode ser alterado para o estado" + model.StatusEnum.ToString());

            carrinho.StatusCarrinhoProdutoEnum = model.StatusEnum;

            _context.Carrinho.Update(carrinho);
            await _context.SaveChangesAsync();

            return true;

        }

        public async Task<bool> AlterarQtde(EdicaoQtdeModel model)
        {

            model.Validar();

            var carrinho = await _context.Carrinho
            .Where(a => a.CarrinhoId == model.CarrinhoId &&
                    a.Qtde != model.Qtde &&
                    model.Qtde > 0 &&
                    a.StatusCarrinhoProdutoEnum == StatusCarrinhoProdutoEnum.Ativo)
            .FirstOrDefaultAsync();

            if (carrinho == null)
                throw new Exception("Esse produto não pode ser alterado para essa quantidade ");

            carrinho.Qtde = model.Qtde;

            _context.Carrinho.Update(carrinho);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<decimal> CalcularValorTotal(int clienteId)
        {
            var produtos = await _context.Carrinho
            .Include(a => a.Produto)
            .Where(a => a.ClienteId == clienteId && a.StatusCarrinhoProdutoEnum == StatusCarrinhoProdutoEnum.Ativo)
            .ToListAsync();

            if (produtos.Count() == 0)
                throw new Exception("O cliente não tem produtos no carrinho");

            var valorTotal = 0.0m;

            produtos.ForEach(a => 
            {
                valorTotal += a.Produto.Preco * a.Qtde;
            });

            return valorTotal;
        }

        public  async  Task<ProdutoQtdeModel> BuscarQtde(int clienteId )
        {
            return await _context.Carrinho
                .Where(a => a.ClienteId == clienteId)
                .Select(a => new Entities.ListaProdutoCompra
                {
                    ProdutoId = a.ProdutoId,
                    Qtde = a.Qtde
                }).ToList();
        }

        #region Internos

        
        #endregion
    }
}
