
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.IO;

namespace SingleExperience.Entities.BD
{
    class EnderecoBD
    {
        public List<EnderecoEntity> ListarEnderecos()
        {
            var path = @"C:\Users\rafael.messias\source\repos\SingleExperience\Tabelas\Endereco.csv";

            List<EnderecoEntity> listaEnderecos = new List<EnderecoEntity>();

            try
            {
                var enderecos = File.ReadAllLines(path, Encoding.UTF8);

                enderecos.Skip(1)
                    .ToList()
                    .ForEach(e =>
                    {
                        var campos = e.Split(",");

                        var endereco = new EnderecoEntity();

                        endereco.EnderecoId = int.Parse(campos[0]);
                        endereco.ClienteId = int.Parse(campos[1]);
                        endereco.Rua = campos[2];
                        endereco.Numero = campos[3];
                        endereco.Complemento = campos[4];
                        endereco.Cep = campos[5];

                        listaEnderecos.Add(endereco);
                    });
            }
            catch (Exception)
            {

                throw;
            }

            return listaEnderecos;
        }
    }
}
