using SingleExperience.Entities.BD;
using SingleExperience.Services.Cliente.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;

namespace SingleExperience.Services.Cliente
{
    public class ClienteService
    {
        protected readonly SingleExperience.Context.Context _context;

        public ClienteService()
        {
        }

        public ClienteService(SingleExperience.Context.Context context)
        {
            _context = context;
        }

        public ClienteLogadoModel Login(LoginModel loginModel)
        {
            var cliente = new ClienteLogadoModel();
            try
            {
                cliente = _context.Cliente
                    .Where(a => a.Email == loginModel.Email &&
                           a.Senha == loginModel.Senha)
                    .Select(a => new ClienteLogadoModel
                    {
                        ClienteId = a.ClienteId,
                        Nome = a.Nome,
                    }).FirstOrDefault();
                    
            }
            catch (IOException e)
            {
                Console.WriteLine("Ocorreu um erro");
                Console.WriteLine(e);
            }

            return cliente;
        }

        public bool Cadastrar(CadastroClienteModel model)
        {
            try
            {
                var cliente = _context.Cliente
                    .Where(a => a.Email == model.Email)
                    .FirstOrDefault();

                if (cliente != null)
                    throw new Exception("Esse email ja está cadastrado");

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
            }
            catch (IOException e)
            {
                Console.WriteLine("Ocorreu um Erro:");
                Console.WriteLine(e.Message);
            }

            return true;
        }

        public ClienteDetalheModel Obter(int clienteId)
        {
            var cliente = new ClienteDetalheModel();

            try
            {
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

            }
            catch (IOException e)
            {
                Console.WriteLine("Ocorreu um Erro");
                Console.WriteLine(e.Message);
            }

            return cliente;
        }

        public bool EditarEmail(EdicaoEmailModel model)
        {
            try
            {
                var emailExistente = _context.Cliente
                    .Where(a => a.Email == model.NovoEmail)
                    .FirstOrDefault();

                if (emailExistente != null)
                    throw new Exception("Esse email ja esta cadastrado");

                var cliente = _context.Cliente
                    .Where(a => a.ClienteId == model.ClienteId)
                    .FirstOrDefault();

                if (cliente == null)
                    throw new Exception("Não foi possível encontrar esse Usuario");


                cliente.Email = model.NovoEmail;

                _context.Cliente.Update(cliente);
                _context.SaveChanges();

            }
            catch (IOException e)
            {

                Console.WriteLine("Ocorreu um erro");
                Console.WriteLine(e);
            }

            return true;
        }

        public bool EditarSenha(EdicaoSenhaModel model)
        {
            try
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

            }
            catch (IOException e)
            {

                Console.WriteLine("Ocorreu um erro");
                Console.WriteLine(e);
            }

            return true;
        }

        public bool Verificar(VerificarClienteModel model)
        {
            
            try
            {
                var cliente = _context.Cliente
                    .Where(a => a.ClienteId == model.ClienteId &&
                           a.Email == model.Email &&
                           a.Senha == model.Senha)
                    .FirstOrDefault();
                
                if(cliente == null)
                {
                    return false;
                }

            }
            catch (IOException e)
            {
                Console.WriteLine("Ocorreu um erro");
                Console.WriteLine(e);
            }

            return true;
        }

    }

}
