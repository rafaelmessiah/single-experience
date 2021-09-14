
using SingleExperience.Services.CartaoCredito.Models;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SingleExperience.Services.CartaoCredito
{
    public class CartaoCreditoService
    {
        protected readonly SingleExperience.Context.Context _context;

        public CartaoCreditoService(SingleExperience.Context.Context context)
        {
            _context = context;
        }

        public async Task<List<CartaoDetalhadoModel>> Buscar(int clienteId)
        {
            return await _context.CartaoCredito
                .Where(a => a.ClienteId == clienteId)
                .Select(a => new CartaoDetalhadoModel
                {
                    ClienteId = a.ClienteId,
                    CartaoCreditoId = a.CartaoCreditoId,
                    Numero = a.Numero,
                    DataVencimento = a.DataVencimento
                }).ToListAsync();
        }

        public async Task<bool> Cadastrar(CadastroCartaoModel model)
        {
            model.Validar();

            var cartao = new Entities.CartaoCredito
            {
                ClienteId = model.ClienteId,
                Numero = model.Numero,
                Bandeira = model.Bandeira,
                CodigoSeguranca = model.CodigoSeguranca,
                DataVencimento = model.DataVencimento
            };

            await _context.CartaoCredito.AddAsync(cartao);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<CartaoDetalhadoModel> Obter(int cartaoCreditoId)
        {
            var cartao = await _context.CartaoCredito
                .Where(a => a.CartaoCreditoId == cartaoCreditoId)
                .Select(a => new CartaoDetalhadoModel
                {
                    CartaoCreditoId = a.CartaoCreditoId,
                    ClienteId = a.ClienteId,
                    DataVencimento = a.DataVencimento,
                    Numero = a.Numero,
                }).FirstOrDefaultAsync();

            if (cartao == null)
                throw new Exception("Não foi possível encontrar esse cartão");

            return cartao;
        }

        public async Task<bool> Verificar(VerificarCartaoModel model)
        {
            return await _context.CartaoCredito
                         .AnyAsync(a => a.CartaoCreditoId == model.CartaoCreditoId && a.ClienteId == model.ClienteId);
        }
    }
}
