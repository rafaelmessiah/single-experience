using SingleExperience.Services.Cliente.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SingleExperience.Services.Cliente
{
    public class ClienteService
    {
        protected readonly SingleExperience.Context.Context _context;

        public ClienteService(SingleExperience.Context.Context context)
        {
            _context = context;
        }

        public async Task<ClienteLogadoModel> Login(LoginModel loginModel)
        {

            var cliente = await _context.Cliente
                .Where(a => a.Email == loginModel.Email &&
                       a.Senha == loginModel.Senha)
                .Select(a => new ClienteLogadoModel
                {
                    ClienteId = a.ClienteId,
                    Nome = a.Nome,
                }).FirstOrDefaultAsync();

            if (cliente == null)
                throw new Exception("Email ou Senha Incorreto");

            return cliente;
        }

        public async Task<bool> Cadastrar(CadastroClienteModel model)
        {
            if (_context.Cliente.Any(a => a.Email == model.Email))
                throw new Exception("Esse email ja esta cadastrado");

            var novoCliente = new Entities.Cliente
            {
                Cpf = model.Cpf,
                Nome = model.Nome,
                Email = model.Email,
                Senha = model.Senha,
                DataNascimento = model.DataNascimento,
                Telefone = model.Telefone
            };

            await _context.Cliente.AddAsync(novoCliente);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<ClienteDetalheModel> Obter(int clienteId)
        {
            var cliente = await _context.Cliente
               .Where(a => a.ClienteId == clienteId)
               .Select(a => new ClienteDetalheModel
               {
                   ClienteId = a.ClienteId,
                   Cpf = a.Cpf,
                   Nome = a.Nome,
                   DataNascimento = a.DataNascimento,
                   Telefone = a.Telefone
               }).FirstOrDefaultAsync();

            if (cliente == null)
                throw new Exception("Não foi possivel encontrar esse cliente");

            return cliente;
        }

        public async Task<bool> EditarEmail(EdicaoEmailModel model)
        {

            if (_context.Cliente.Any(a => a.Email == model.NovoEmail))
                throw new Exception("Esse email ja esta cadastrado");

            var cliente = await _context.Cliente
                .Where(a => a.ClienteId == model.ClienteId)
                .FirstOrDefaultAsync();

            if (cliente == null)
                throw new Exception("Não foi possível encontrar esse Usuario");

            cliente.Email = model.NovoEmail;

            _context.Cliente.Update(cliente);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> EditarSenha(EdicaoSenhaModel model)
        {

            var cliente = await _context.Cliente
                .Where(a => a.ClienteId == model.ClienteId)
                .FirstOrDefaultAsync();

            if (cliente == null)
                throw new Exception("Esse Usuário não existe");

            if (cliente.Senha == model.NovaSenha)
                throw new Exception("A nova senha não pode ser igual a anterior");

            cliente.Senha = model.NovaSenha;

            _context.Cliente.Update(cliente);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> Verificar(VerificarClienteModel model)
        {
             return _context.Cliente
               .Any(a => a.ClienteId == model.ClienteId &&
                      a.Email == model.Email &&
                      a.Senha == model.Senha);

        }

    }

}
