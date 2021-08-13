using SingleExperience.Entities.BD;
using SingleExperience.Services.ListaProdutoCompra.Models;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using SingleExperience.Services.Produto;
using System;
using Microsoft.EntityFrameworkCore;

namespace SingleExperience.Services.ListaProdutoCompra
{
    public class ListaProdutoCompraService
    {
        protected readonly SingleExperience.Context.Context _context;

        public ListaProdutoCompraService(SingleExperience.Context.Context context)
        {
            _context = context;
        }

        public List<ItemProdutoCompraModel> Buscar(int compraId)
        {
            return _context.ListaProdutoCompra
                .Include(a=> a.Produto)
                .Where(a => a.CompraId == compraId)
                .Select(b => new ItemProdutoCompraModel
                {
                    ListaProdutoCompraId = b.ListaProdutoCompraId,
                    CompraId = b.CompraId,
                    ProdutoId = b.ProdutoId,
                    Nome = b.Produto.Nome,
                    PrecoUnitario = b.Produto.Preco,
                    Qtde = b.Qtde

                }).ToList();

        }

    }
}
