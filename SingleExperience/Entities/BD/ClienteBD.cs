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

        public List<Cliente> Buscar()
        {
            
            List<Cliente> listaCliente = new List<Cliente>();

            try
            {
                var clientes = File.ReadAllLines(path, Encoding.UTF8);

                clientes.Skip(1)
                    .ToList()
                    .ForEach(linha =>
                    {
                        var campos = linha.Split(',');

                        var cliente = new Cliente();
                        cliente.ClienteId = int.Parse(campos[0]);
                        cliente.Cpf = campos[1];
                        cliente.Nome = campos[2];
                        cliente.Email = campos[3];
                        cliente.Senha = campos[4];
                        cliente.DataNascimento = DateTime.Parse(campos[5]);

                        listaCliente.Add(cliente);

                    });
            }
            catch (IOException e)
            {
                Console.WriteLine("Ocorreu um Erro");
                Console.WriteLine(e);
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
            catch (IOException e)
            {
                Console.WriteLine("Ocorreu um Erro");
                Console.WriteLine(e);
            }

            return true;
        }

        public bool EditarEmail(EdicaoEmailModel model)
        {
            try
            {
                var clientes = Buscar();

                var index = clientes
                    .FindIndex(a => a.ClienteId == model.ClienteId);

                clientes[index].Email = model.NovoEmail;

                // Gera as linhas para colocar no csv

                var linhas = new List<string>();

                linhas.Add(header);

                foreach (var item in clientes)
                {

                    var aux = new string[]
                    {
                      item.ClienteId.ToString(),
                      item.Cpf.ToString(),
                      item.Nome.ToString(),
                      item.Email.ToString(),
                      item.Senha.ToString(),
                      item.DataNascimento.ToString(),
                      item.Telefone.ToString(),
                    };

                    linhas.Add(String.Join(",", aux));
                }

                File.WriteAllLines(path, linhas);

            }
            catch (IOException e)
            {
                Console.WriteLine("Ocurred an error");
                Console.WriteLine(e.Message);
            }

            return true;
        }

        public bool EditarSenha(EdicaoSenhaModel model)
        {
            try
            {
                var clientes = Buscar();

                var index = clientes
                    .FindIndex(a => a.ClienteId == model.ClienteId);

                clientes[index].Senha = model.NovaSenha;

                // Gera as linhas para colocar no csv

                var linhas = new List<string>();

                linhas.Add(header);

                foreach (var item in clientes)
                {

                    var aux = new string[]
                    {
                      item.ClienteId.ToString(),
                      item.Cpf.ToString(),
                      item.Nome.ToString(),
                      item.Email.ToString(),
                      item.Senha.ToString(),
                      item.DataNascimento.ToString(),
                      item.Telefone.ToString(),
                    };

                    linhas.Add(String.Join(",", aux));
                }

                File.WriteAllLines(path, linhas);

            }
            catch (IOException e)
            {
                Console.WriteLine("Ocurred an error");
                Console.WriteLine(e.Message);
            }

            return true;
        }
    }
}
