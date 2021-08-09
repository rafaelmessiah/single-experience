using SingleExperience.Services.CartaoCredito.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SingleExperience.Entities.BD
{
    class CartaoCreditoBD
    {
        string path = @"C:\Users\rafael.messias\source\repos\SingleExperience\Tabelas\CartaoCredito.csv";
        string header = "";
        
        public List<CartaoCreditoEntity> Buscar()
        {
            List<CartaoCreditoEntity> listaCartaoCredito = new List<CartaoCreditoEntity>();

            try
            {

                var cartaoCreditos = File.ReadAllLines(path, Encoding.UTF8);

                cartaoCreditos.Skip(1)
                    .ToList()
                    .ForEach(linha =>
                    {
                        var campo = linha.Split(",");

                        var cartao = new CartaoCreditoEntity();
                        cartao.CartaoCreditoId = int.Parse(campo[0]);
                        cartao.ClienteId = int.Parse(campo[1]);
                        cartao.Numero = campo[2];
                        cartao.Bandeira = campo[3];
                        cartao.CodigoSeguranca = campo[4];
                        DateTime.TryParse(campo[5], out DateTime dateTime);
                        cartao.DataVencimento = dateTime;
                       
                        listaCartaoCredito.Add(cartao);
                    });
            }
            catch (IOException e)
            {

                Console.WriteLine("Ocorreu um erro");
                Console.WriteLine(e);
            }

            return listaCartaoCredito;
        }

        public bool Salvar(CadastroCartaoModel model)
        {
            try
            {
                var cartaoId = Buscar().Count + 1;

                using (var streamWriter = File.AppendText(path))
                {
                    var aux = new string[]
                    {
                        cartaoId.ToString(),
                        model.ClienteId.ToString(),
                        model.Bandeira.ToString(),
                        model.CodigoSeguranca.ToString(),
                        model.DataVencimento.ToString(),
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

    }
}
