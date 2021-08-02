using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SingleExperience.Entities.BD
{
    class CartaoCreditoBD
    {
        public List<CartaoCreditoEntity> ListaCartaoCredito()
        {
            string path = @"C:\Users\rafael.messias\source\repos\SingleExperience\Tabelas\CartaoCreditoId";

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
