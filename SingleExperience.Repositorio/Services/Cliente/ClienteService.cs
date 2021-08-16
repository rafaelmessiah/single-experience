using SingleExperience.Services.Cliente.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;
using System.Threading;

namespace SingleExperience.Services.Cliente
{
    public class ClienteService
    {
        protected readonly SingleExperience.Context.SeContext _context;

        public ClienteService(SingleExperience.Context.SeContext context)
        {
            _context = context;
        }

        public ClienteLogadoModel Login(LoginModel loginModel)
        {
            var cliente = new ClienteLogadoModel();

            cliente = _context.Cliente
                .Where(a => a.Email == loginModel.Email &&
                       a.Senha == loginModel.Senha)
                .Select(a => new ClienteLogadoModel
                {
                    ClienteId = a.ClienteId,
                    Nome = a.Nome,
                }).FirstOrDefault();

            if (cliente == null)
                throw new Exception("Email ou Senha Incorreto");

            return cliente;
        }

        public bool Cadastrar(CadastroClienteModel model)
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

            _context.Cliente.Add(novoCliente);
            _context.SaveChanges();

            return true;
        }

        public ClienteDetalheModel Obter(int clienteId)
        {
            var cliente = new ClienteDetalheModel();

            cliente = _context.Cliente
               .Where(a => a.ClienteId == clienteId)
           .Select(a => new ClienteDetalheModel
           {
               ClienteId = a.ClienteId,
               Cpf = a.Cpf,
               Nome = a.Nome,
               DataNascimento = a.DataNascimento,
               Telefone = a.Telefone
           }).FirstOrDefault();

            if (cliente == null)
                throw new Exception("Não foi possivel encontrar esse cliente");

            return cliente;
        }

        public bool EditarEmail(EdicaoEmailModel model)
        {

            if (_context.Cliente.Any(a => a.Email == model.NovoEmail))
                throw new Exception("Esse email ja esta cadastrado");

            var cliente = _context.Cliente
                .Where(a => a.ClienteId == model.ClienteId)
                .FirstOrDefault();

            if (cliente == null)
                throw new Exception("Não foi possível encontrar esse Usuario");

            cliente.Email = model.NovoEmail;

            _context.Cliente.Update(cliente);
            _context.SaveChanges();

            return true;
        }

        public bool EditarSenha(EdicaoSenhaModel model)
        {

            var cliente = _context.Cliente
                .Where(a => a.ClienteId == model.ClienteId)
                .FirstOrDefault();

            if (cliente == null)
                throw new Exception("Esse Usuário não existe");

            if (cliente.Senha == model.NovaSenha)
                throw new Exception("A nova senha não pode ser igual a anterior");

            cliente.Senha = model.NovaSenha;

            _context.Cliente.Update(cliente);
            _context.SaveChanges();

            return true;
        }

        public bool Verificar(VerificarClienteModel model)
        {
            return _context.Cliente
               .Any(a => a.ClienteId == model.ClienteId &&
                      a.Email == model.Email &&
                      a.Senha == model.Senha);

        }

    }

}
