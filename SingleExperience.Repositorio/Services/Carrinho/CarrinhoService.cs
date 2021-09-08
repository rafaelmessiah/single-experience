using Microsoft.EntityFrameworkCore;
using SingleExperience.Enums;
using SingleExperience.Repositorio.Services.Carrinho.Models;
using SingleExperience.Services.Carrinho.Models;
using SingleExperience.Services.Compra.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SingleExperience.Services.Carrinho
{
    public class CarrinhoService
    {
        protected readonly Context.Context _context;

        public CarrinhoService(Context.Context context)
        {
            _context = context;
        }

        public async Task<List<ItemCarrinhoModel>> BuscarItens(int clienteId)
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
                Qtde = a.Qtde,
                Imagem = a.Produto.Imagem
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

        public async Task<bool> AlterarStatus(int carrinhoId, EdicaoStatusModel model)
        {
            model.CarrinhoId = carrinhoId;

            model.Validar();

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

        public async Task<bool> AlterarQtde(int carrinhoId,EdicaoQtdeModel model)
        {
            model.CarrinhoId = carrinhoId;

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

        public async Task<List<ProdutoQtdeModel>> BuscarQtde(int clienteId)
        {
            return await _context.Carrinho
            .Include(a => a.Produto)
            .Where(a => a.ClienteId == clienteId && a.StatusCarrinhoProdutoEnum == StatusCarrinhoProdutoEnum.Ativo)
            .Select(p => new ProdutoQtdeModel 
            {
                CarrinhoId = p.CarrinhoId,
                ProdutoId = p.ProdutoId,
                Preco = p.Produto.Preco,
                Qtde = p.Qtde
            })
            .ToListAsync();
        }

        public async Task<bool> VerificarItem(VerificarItemModel model)
        {
            return await _context.Carrinho
                .AnyAsync(a => a.ClienteId == model.ClienteId &&
                            a.ProdutoId == model.ProdutoId &&
                            a.StatusCarrinhoProdutoEnum == StatusCarrinhoProdutoEnum.Ativo);
        }
    }
}
