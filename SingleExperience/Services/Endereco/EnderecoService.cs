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
        protected readonly SingleExperience.Context.Context _context;

        public EnderecoService(SingleExperience.Context.Context context)
        {
            _context = context;
        }

        public List<EnderecoModel> Buscar(int clienteId)
        {
            return _context.Endereco
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

        public bool Cadastrar(CadastroEnderecoModel model)
        {
            try
            {
                var endereco = new Entities.Endereco
                {
                    ClienteId = model.ClienteId,
                    Rua = model.Rua,
                    Numero = model.Numero,
                    Complemento = model.Complemento,
                    Cep = model.Cep
                };

                _context.Endereco.Add(endereco);
                _context.SaveChanges();
            }
            catch (IOException e)
            {

                Console.WriteLine("Ocorreu um Erro");
                Console.WriteLine(e);
            }

            return true;
        }

        public bool Editar(EnderecoModel model)
        {
            try
            {
                var endereco = _context.Endereco
                    .Where(a => a.EnderecoId == model.EnderecoId && a.ClienteId == a.ClienteId)
                    .FirstOrDefault();

                if (endereco == null)
                    throw new Exception("Não possivel encontrar esse endereco");

                endereco.Rua = model.Rua;
                endereco.Numero = model.Numero;
                endereco.Complemento = model.Complemento;
                endereco.Cep = model.Cep;

                _context.Endereco.Update(endereco);
                _context.SaveChanges();
            }
            catch (IOException e)
            {

                Console.WriteLine("Ocorreu um Erro");
                Console.WriteLine(e);
            }

            return true;
        }

        public bool Verificar(VerificarEnderecoModel model)
        {
            try
            {
                var endereco = _context.Endereco
                    .Where(a => a.ClienteId == model.ClienteId && a.EnderecoId == model.EnderecoId)
                    .FirstOrDefault();

                if (endereco == null)
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
