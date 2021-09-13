using Microsoft.EntityFrameworkCore;
using SingleExperience.Repositorio.Services.FormaPagamento.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingleExperience.Repositorio.Services.FormaPagamento
{
    public class FormaPagamentoService
    {
        protected readonly Context.Context _context;
        public FormaPagamentoService(Context.Context context)
        {
            _context = context;
        }

        public async Task<List<FormaPagamentoModel>> Buscar()
        {
            return await _context.FormaPagamento
                         .Select(p => new FormaPagamentoModel
                         {
                             FormaPagamentoEnum = p.FormaPagamentoEnum,
                             Descricao = p.Descricao
                         }).ToListAsync();
        }
    }
}
