
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.IO;
using SingleExperience.Services.Endereco.Models;

namespace SingleExperience.Entities.BD
{
    class EnderecoBD
    {
        string path = @"C:\Users\rafael.messias\source\repos\SingleExperience\Tabelas\Endereco.csv";
        string header = "";

        public List<EnderecoEntity> Buscar()
        {
            List<EnderecoEntity> listaEnderecos = new List<EnderecoEntity>();

            try
            {
                var enderecos = File.ReadAllLines(path, Encoding.UTF8);

                header = enderecos[0];

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
            catch (IOException e)
            {

                Console.WriteLine("Ocorreu um erro");
                Console.WriteLine(e);
            }

            return listaEnderecos;
        }

        public bool Editar(EnderecoModel model)
        {
            try
            {
                var enderecos = Buscar();
                var index = enderecos.FindIndex(a => a.EnderecoId == model.EnderecoId);

                enderecos[index].Rua = model.Rua;
                enderecos[index].Numero = model.Numero;
                enderecos[index].Complemento = model.Complemento;
                enderecos[index].Cep = model.Cep;

                var linhas = new List<string>();

                linhas.Add(header);

                foreach (var item in enderecos)
                {
                    var aux = new string[]
                    {
                        item.EnderecoId.ToString(),
                        item.ClienteId.ToString(),
                        item.Numero.ToString(),
                        item.Complemento.ToString(),
                        item.Cep.ToString()
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

        public bool Salvar(CadastroEnderecoModel model)
        {
            try
            {
                var enderecoId = Buscar().Count + 1;

                using(var streamWriter = File.AppendText(path))
                {
                    var aux = new string[]
                    {
                        enderecoId.ToString(),
                        model.ClienteId.ToString(),
                        model.Numero.ToString(),
                        model.Complemento.ToString(),
                        model.Cep
                    };

                    streamWriter.WriteLine(String.Join("," , aux));
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
