using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.IO;
using SingleExperience.Services.Cliente.Models;

namespace SingleExperience.Entities.BD
{
    class ClienteBD
    {
        string path = @"C:\Users\rafael.messias\source\repos\SingleExperience\Tabelas\Cliente.csv";
        string header = "";

        public List<ClienteEntity> Buscar()
        {
            
            List<ClienteEntity> listaCliente = new List<ClienteEntity>();

            try
            {
                var clientes = File.ReadAllLines(path, Encoding.UTF8);

                clientes.Skip(1)
                    .ToList()
                    .ForEach(linha =>
                    {
                        var campos = linha.Split(',');

                        var cliente = new ClienteEntity();
                        cliente.ClienteId = int.Parse(campos[0]);
                        cliente.Cpf = campos[1];
                        cliente.Nome = campos[2];
                        cliente.Email = campos[3];
                        cliente.Senha = campos[4];
                        cliente.DataNascimento = DateTime.Parse(campos[5]);

                        listaCliente.Add(cliente);

                    });
            }
            catch (Exception)
            {

                throw;
            }

            return listaCliente;
        }

        public bool Cadastrar(CadastroClienteModel model)
        {
            try
            {
                var clienteId = Buscar().Count + 1;

                using (var streamWriter = File.AppendText(path))
                {
                    var aux = new string[]
                    {
                        clienteId.ToString(),
                        model.Cpf.ToString(),
                        model.Nome.ToString(),
                        model.Email.ToString(),
                        model.Senha.ToString(),
                        model.DataNascimento.ToString(),
                        model.Telefone.ToString()
                    };

                    streamWriter.WriteLine(String.Join(",", aux));
                }

            }
            catch (Exception)
            {
                throw new Exception("Não foi Possível inserir esse ");
            }

            return true;
        }

        
    }
}
