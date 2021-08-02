using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.IO;

namespace SingleExperience.Entities.BD
{
    class ClienteBD
    {
        public List<ClienteEntity> ListarCliente()
        {
            var path = @"C:\Users\rafael.messias\source\repos\SingleExperience\Tabelas\Cliente.csv";

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

        

        
    }
}
