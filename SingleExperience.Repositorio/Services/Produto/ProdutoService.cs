using SingleExperience.Entities.Enums;
using SingleExperience.Services.Produto.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using SingleExperience.Context;


namespace SingleExperience.Services.Produto
{
    public class ProdutoService
    {
        protected readonly SingleExperience.Context.Context _context;

        public ProdutoService(SingleExperience.Context.Context context)
        {
            _context = context;
        }

        public List<ProdutoSimplesModel> Buscar()
        {
            return _context.Produto
                .Where(a => a.Disponivel)
            .Select(p => new ProdutoSimplesModel
            {
                ProdutoId = p.ProdutoId,
                Nome = p.Nome,
                Preco = p.Preco,
            }).ToList();

        }

        public List<ProdutoSimplesModel> BuscarCategoria(CategoriaEnum categoria)
        {

            return _context.Produto
           .Where(a => a.CategoriaEnum == categoria && a.Disponivel)
           .Select(b => new ProdutoSimplesModel
           {
               ProdutoId = b.ProdutoId,
               Nome = b.Nome,
               Preco = b.Preco,
           }).ToList();

        }

        public DisponivelModel ObterDisponibildade(int produtoId)
        {
            var produto = _context.Produto
             .Where(a => a.ProdutoId == produtoId)
             .Select(b => new DisponivelModel
             {
                 QtdeDisponivel = b.QtdeEmEstoque,
                 Disponivel = b.Disponivel
             }).FirstOrDefault();

            if (produto == null)
                throw new Exception("Não foi possível encontrar esse Produto");

            return produto;
        }

        public bool Verificar(int produtoId)
        {
            try
            {
                var produto = _context.Produto
                    .Where(a => a.ProdutoId == produtoId)
                    .FirstOrDefault();

                if (produto == null)
                    return false;

            }
            catch (IOException e)
            {
                Console.WriteLine("Ocorreu um erro");
                Console.WriteLine(e.Message);
            }

            return true;
        }

        public ProdutoDetalhadoModel ObterDetalhe(int produtoId)
        {

            var produto = _context.Produto
            .Where(a => a.ProdutoId == produtoId && a.Disponivel)
            .Select(b => new ProdutoDetalhadoModel
            {
                ProdutoId = b.ProdutoId,
                Nome = b.Nome,
                Descricao = b.Detalhe,
                Preco = b.Preco
            }).FirstOrDefault();

            if (produto == null)
                throw new Exception("Id invalido");

            return produto;
        }

        public bool Retirar(AlterarQtdeModel model)
        {

            var produto = _context.Produto
                .Where(a => a.ProdutoId == model.ProdutoId &&
                a.QtdeEmEstoque >= model.Qtde &&
                model.Qtde > 0)
                .FirstOrDefault();

            if (produto == null)
                throw new Exception("Não é possível retirar essa quantidade desse Produto");

            produto.QtdeEmEstoque -= model.Qtde;

            _context.Produto.Update(produto);
            _context.SaveChanges();

            return true;
        }
    }
}
