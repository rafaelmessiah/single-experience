using SingleExperience.Entities.BD;
using SingleExperience.Entities.Enums;
using SingleExperience.Services.Produto.Models;
using System;
using System.Collections.Generic;
using System.Linq;


namespace SingleExperience.Services.Produto
{
    public class ProdutoService
    {
        ProdutoBD produtoBd = new ProdutoBD();

        public List<ProdutoSimplesModel> Buscar()
        {
            return produtoBd.ListarProdutos().Where(a => a.Disponivel)
                .Select(p => new ProdutoSimplesModel
                {
                    ProdutoId = p.ProdutoId,
                    Nome = p.Nome,
                    Preco = p.Preco,
                }).ToList();
        }

        public List<ProdutoSimplesModel> BuscarCategoria(CategoriaEnum categoria)
        {
            return produtoBd.ListarProdutos()
                .Where(a => a.CategoriaId == categoria && a.Disponivel)
                .Select(b => new ProdutoSimplesModel
                {
                    ProdutoId = b.ProdutoId,
                    Nome = b.Nome,
                    Preco = b.Preco,
                }).ToList();

        }

        public ProdutoSimplesModel ObterSimples(int produtoId)
        {
            return produtoBd.ListarProdutos()
                .Where(a => a.ProdutoId == produtoId)
                .Select(b => new ProdutoSimplesModel
                {
                    Nome = b.Nome,
                    ProdutoId = b.ProdutoId,
                    Preco = b.Preco
                }).FirstOrDefault();

        } 

        public ProdutoDetalhadoModel ObterDetalhe(int produtoId)
        {
            var produto = produtoBd.ListarProdutos()
                .Where(a => a.ProdutoId == produtoId && a.Disponivel)
                .Select(b => new ProdutoDetalhadoModel
                {
                    ProdutoId = b.ProdutoId,
                    Nome = b.Nome,
                    Descricao = b.Detalhe,
                    Preco = b.Preco
                }).FirstOrDefault();

            if (produto == null)
                throw new Exception("Produto não encontrado");

            return produto;
        }
    }
}
