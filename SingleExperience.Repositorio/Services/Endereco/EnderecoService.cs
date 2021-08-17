using Microsoft.EntityFrameworkCore;
using SingleExperience.Services.Endereco.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SingleExperience.Services.Endereco
{
    public class EnderecoService
    {
        protected readonly SingleExperience.Context.Context _context;

        public EnderecoService(SingleExperience.Context.Context context)
        {
            _context = context;
        }

        public async Task<List<EnderecoModel>> Buscar(int clienteId)
        {
            return await _context.Endereco
                .Where(a => a.ClienteId == clienteId)
                .Select(a => new EnderecoModel
                {
                    EnderecoId = a.EnderecoId,
                    ClienteId = a.ClienteId,
                    Rua = a.Rua,
                    Numero = a.Numero,
                    Complemento = a.Complemento,
                    Cep = a.Cep,
                }).ToListAsync();
        }

        public async Task<bool> Cadastrar(CadastroEnderecoModel model)
        {
            var endereco = new Entities.Endereco
            {
                ClienteId = model.ClienteId,
                Rua = model.Rua,
                Numero = model.Numero,
                Complemento = model.Complemento,
                Cep = model.Cep
            };

            await _context.Endereco.AddAsync(endereco);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> Editar(EnderecoModel model)
        {

            var endereco = await _context.Endereco
                .Where(a => a.EnderecoId == model.EnderecoId && a.ClienteId == a.ClienteId)
                .FirstOrDefaultAsync();

            if (endereco == null)
                throw new Exception("Não possivel encontrar esse endereco");

            endereco.Rua = model.Rua;
            endereco.Numero = model.Numero;
            endereco.Complemento = model.Complemento;
            endereco.Cep = model.Cep;

            _context.Endereco.Update(endereco);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> Verificar(VerificarEnderecoModel model)
        {
            return await _context.Endereco
                .AnyAsync(a => a.ClienteId == model.ClienteId && a.EnderecoId == model.EnderecoId);
        }

    }
}
