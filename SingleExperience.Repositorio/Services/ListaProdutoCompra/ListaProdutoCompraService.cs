using SingleExperience.Services.ListaProdutoCompra.Models;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using SingleExperience.Services.Produto;
using System;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace SingleExperience.Services.ListaProdutoCompra
{
    public class ListaProdutoCompraService
    {
        protected readonly SingleExperience.Context.Context _context;

        public ListaProdutoCompraService(SingleExperience.Context.Context context)
        {
            _context = context;
        }

        public async Task<List<ItemProdutoCompraModel>> Buscar(int compraId)
        {
            return await _context.ListaProdutoCompra
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
                }).ToListAsync();
        }
    }
}
