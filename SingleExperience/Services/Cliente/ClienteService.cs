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
        ClienteBD clienteBd = new ClienteBD();

        public ClienteLogadoModel Login(LoginModel loginModel)
        {
            var cliente = new ClienteLogadoModel();
            try
            {
                cliente = clienteBd.Buscar().Where(a => a.Email == loginModel.Email &&
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
                clienteBd.Buscar().ForEach(a =>
                {
                    if (a.Cpf == model.Cpf)
                    {
                        throw new Exception("Esse Cpf Já esta cadastrado");
                    }

                    if (a.Email == model.Email)
                    {
                        throw new Exception("Esse Email Já esta cadastrado");
                    }
                });

                clienteBd.Cadastrar(model);
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
                cliente = clienteBd.Buscar()
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
                var emailExistente = clienteBd.Buscar()
                    .Where(a => a.Email == model.NovoEmail)
                    .FirstOrDefault();

                if (emailExistente != null)
                    throw new Exception("Esse email ja esta cadastrado");

                var clienteExiste = clienteBd.Buscar()
                    .Where(a => a.ClienteId == model.ClienteId)
                    .FirstOrDefault();

                if (clienteExiste == null)
                    throw new Exception("Não foi possível encontrar esse Usuario");

                clienteBd.EditarEmail(model);

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
                var cliente = clienteBd.Buscar()
                    .Where(a => a.ClienteId == model.ClienteId)
                    .FirstOrDefault();

                if (cliente == null)
                    throw new Exception("Esse Usuário não existe");

                if (cliente.Senha == model.NovaSenha)
                    throw new Exception("A nova senha não pode ser igual a anterior");

                clienteBd.EditarSenha(model);

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
                var cliente = clienteBd.Buscar().Where(a => a.ClienteId == model.ClienteId &&
                a.Email == model.Email &&
                a.Senha == model.Senha).FirstOrDefault();
                
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
