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
                        cartao.Nome = campo[2];
                        cartao.Numero = campo[3];
                        cartao.Bandeira = campo[4];
                        DateTime.TryParse(campo[5], out DateTime dateTime);
                        cartao.DataVencimento = dateTime;
                       
                        listaCartaoCredito.Add(cartao);
                    });
            }
            catch (Exception)
            {

                throw;
            }

            return listaCartaoCredito;
        }


    }
}
