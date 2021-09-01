using SingleExperience.Repositorio.Services.Categoria.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace SingleExperience.Services.Categoria
{
    public class CategoriaService
    {
        protected readonly Context.Context _context;

        public CategoriaService(Context.Context context)
        {
            _context = context;
        }

        public async Task<List<CategoriaModel>> Buscar()
        {
            return await _context.Categoria.Select(p => new CategoriaModel
            {
                CategoriaEnum = p.CategoriaEnum,
                Descricao = p.Descricao
            }).ToListAsync();
        }
    }
}
