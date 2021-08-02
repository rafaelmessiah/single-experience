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
            var produtos = produtoBd.BuscarProdutos().Where(a => a.Disponivel)
                .Select(p => new ProdutoSimplesModel
                {
                    ProdutoId = p.ProdutoId,
                    Nome = p.Nome,
                    Preco = p.Preco,
                }).ToList();

            if (produtos == null)
                return null;

            return produtos;
        }
        public List<ProdutoSimplesModel> BuscarCategoria(CategoriaEnum categoria)
        {
            var produtos = produtoBd.BuscarProdutos()
                .Where(a => a.CategoriaId == categoria && a.Disponivel)
                .Select(b => new ProdutoSimplesModel
                {
                    ProdutoId = b.ProdutoId,
                    Nome = b.Nome,
                    Preco = b.Preco,
                }).ToList();

            if (produtos == null)
                return null;

            return produtos;
        }
        public ProdutoSimplesModel ObterSimples(int produtoId)
        {
            var produto = produtoBd.BuscarProdutos()
                .Where(a => a.ProdutoId == produtoId)
                .Select(b => new ProdutoSimplesModel
                {
                    Nome = b.Nome,
                    ProdutoId = b.ProdutoId,
                    Preco = b.Preco
                }).FirstOrDefault();

            if (produto == null)
                return null;

            return produto;
        }
        public DisponivelModel ObterDisponibildade(int produtoId)
        {
            var produto = produtoBd.BuscarProdutos()
                .Where(a => a.ProdutoId == produtoId)
                .Select(b => new DisponivelModel
                {
                    QtdeDisponivel = b.QtdeEmEstoque,
                    Disponivel = b.Disponivel
                }).FirstOrDefault();

            if (produto == null)
                return null;

            return produto;
        }
        public ProdutoDetalhadoModel ObterDetalhe(int produtoId)
        {
            var produto = produtoBd.BuscarProdutos()
                .Where(a => a.ProdutoId == produtoId && a.Disponivel)
                .Select(b => new ProdutoDetalhadoModel
                {
                    ProdutoId = b.ProdutoId,
                    Nome = b.Nome,
                    Descricao = b.Detalhe,
                    Preco = b.Preco
                }).FirstOrDefault();

            if (produto == null)
                return null;

            return produto;
        }
        public bool Retirar(AlterarQtdeModel model)
        {
            var produto = produtoBd.BuscarProdutos()
                .Where(a => a.ProdutoId == model.ProdutoId &&
                a.QtdeEmEstoque >= model.Qtde &&
                model.Qtde > 0)
                .FirstOrDefault();

            if (produto == null)
                throw new Exception("Não é possível retirar essa quantidade desse Produto");

            produtoBd.AlterarQtde(model);

            return true;
        }
    }
}
