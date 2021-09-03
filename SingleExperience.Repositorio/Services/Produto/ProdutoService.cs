﻿using Microsoft.EntityFrameworkCore;
using SingleExperience.Enums;
using SingleExperience.Services.Produto.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SingleExperience.Services.Produto
{
    public class ProdutoService
    {
        protected readonly SingleExperience.Context.Context _context;

        public ProdutoService(SingleExperience.Context.Context context)
        {
            _context = context;
        }

        public async Task<List<ProdutoSimplesModel>> Buscar()
        {
            return await _context.Produto
                .Where(a => a.Disponivel)
                .Select(p => new ProdutoSimplesModel
                {
                    ProdutoId = p.ProdutoId,
                    Nome = p.Nome,
                    Preco = p.Preco,
                    Ranking = p.Ranking,
                    Imagem = p.Imagem
                }).ToListAsync();
        }

        public async Task<List<ProdutoSimplesModel>> BuscarRanking()
        {
            return await _context.Produto
                .Where(a => a.Disponivel == true && a.Ranking >= 3)
                .Select(a => new ProdutoSimplesModel
                {
                    ProdutoId = a.ProdutoId,
                    Nome = a.Nome,
                    Preco = a.Preco,
                    Ranking = a.Ranking,
                    Imagem = a.Imagem
                }).ToListAsync();
        }

        public async Task<List<ProdutoSimplesModel>> BuscarCategoria(CategoriaEnum categoria)
        {
            return await _context.Produto
                .Where(a => a.CategoriaEnum == categoria && a.Disponivel)
                .Select(b => new ProdutoSimplesModel
                {
                    ProdutoId = b.ProdutoId,
                    Nome = b.Nome,
                    Preco = b.Preco,
                    Ranking = b.Ranking,
                    Imagem = b.Imagem
                }).ToListAsync();
        }

        public async Task<DisponivelModel> ObterDisponibildade(int produtoId)
        {
            var produto = await _context.Produto
                     .Where(a => a.ProdutoId == produtoId)
                     .Select(b => new DisponivelModel
                     {
                         QtdeDisponivel = b.QtdeEmEstoque,
                         Disponivel = b.Disponivel
                     }).FirstOrDefaultAsync();

            if (produto == null)
                throw new Exception("Não foi possível encontrar esse Produto");

            return produto;
        }

        public async Task<bool> Verificar(int produtoId)
        {
            return await _context.Produto
                .AnyAsync(a => a.ProdutoId == produtoId);
        }

        public async Task<ProdutoDetalhadoModel> ObterDetalhe(int produtoId)
        {

            var produto = await _context.Produto
            .Where(a => a.ProdutoId == produtoId && a.Disponivel)
            .Select(b => new ProdutoDetalhadoModel
            {
                ProdutoId = b.ProdutoId,
                Nome = b.Nome,
                Descricao = b.Detalhe,
                Preco = b.Preco,
                Ranking = b.Ranking,
                Imagem = b.Imagem
            }).FirstOrDefaultAsync();

            if (produto == null)
                throw new Exception("Id invalido");

            return produto;
        }

        public async Task<bool> Retirar(AlterarQtdeModel model)
        {
            model.Validar();

            var produto = await _context.Produto
                .Where(a => a.ProdutoId == model.ProdutoId &&
                a.QtdeEmEstoque >= model.Qtde &&
                model.Qtde > 0)
                .FirstOrDefaultAsync();

            if (produto == null)
                throw new Exception("Não é possível retirar essa quantidade desse Produto");

            produto.QtdeEmEstoque -= model.Qtde;

            _context.Produto.Update(produto);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
