using SingleExperience.Entities.BD;
using SingleExperience.Services.Endereco.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.IO;

namespace SingleExperience.Services.Endereco
{
    class EnderecoService
    {
        EnderecoBD enderecoBd = new EnderecoBD();

        public List<EnderecoModel> Buscar(int clienteId)
        {
            var enderecos = new List<EnderecoModel>();

            try
            {
                enderecos = enderecoBd.Buscar()
                    .Where(a => a.ClienteId == clienteId)
                    .Select(a => new EnderecoModel
                    {
                        EnderecoId = a.EnderecoId,
                        ClienteId = a.ClienteId,
                        Rua = a.Rua,
                        Numero = a.Numero,
                        Complemento = a.Complemento,
                        Cep = a.Cep,

                    }).ToList();

            }
            catch (IOException e) 
            {
                Console.WriteLine("Ocorreu um Erro");
                Console.WriteLine(e);
            }

            return enderecos;
        }

        public bool Cadastrar (CadastroEnderecoModel model)
        {
            try
            {
                enderecoBd.Salvar(model);
            }
            catch (IOException e)
            {

                Console.WriteLine("Ocorreu um Erro");
                Console.WriteLine(e);
            }

            return true;
        }

        public bool Editar (EnderecoModel model)
        {
            try
            {
                var endereco = enderecoBd.Buscar()
                    .Where(a => a.EnderecoId == model.EnderecoId && a.ClienteId == a.ClienteId)
                    .FirstOrDefault();

                if (endereco == null)
                    throw new Exception("Não possivel encontrar esse endereco");

                enderecoBd.Editar(model);
            }
            catch (IOException e)
            {

                Console.WriteLine("Ocorreu um Erro");
                Console.WriteLine(e);
            }

            return true;
        }

        public bool Verificar (VerificarEnderecoModel model)
        {
            try
            {
                var endereco = enderecoBd.Buscar()
                    .Where(a => a.ClienteId == model.ClienteId && a.EnderecoId == model.EnderecoId)
                    .FirstOrDefault();

                if (endereco==null)
                {
                    return false;
                }
                
            }
            catch (IOException e)
            {
                Console.WriteLine("Ocorreu um Erro");
                Console.WriteLine(e);
            }

            return true;
        }

    }
}
