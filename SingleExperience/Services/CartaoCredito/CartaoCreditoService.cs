using SingleExperience.Entities.BD;
using SingleExperience.Services.CartaoCredito.Models;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace SingleExperience.Services.CartaoCredito
{
    public class CartaoCreditoService
    {
        CartaoCreditoBD cartaoCreditoBd = new CartaoCreditoBD();

        public List<CartaoItemModel> Listar(int clienteId)
        {
            var cartoes = new List<CartaoItemModel>();

            try
            {
                cartoes = cartaoCreditoBd.Buscar()
                    .Where(a => a.ClienteId == clienteId)
                    .Select(a => new CartaoItemModel
                    {
                        ClienteId = a.ClienteId,
                        CartaoCreditoId = a.CartaoCreditoId,
                        Final = a.Numero.Substring(a.Numero.Length - 4)

                    }).ToList();
                
            }
            catch (IOException e)
            {
                Console.WriteLine("Ocorreu um erro");
                Console.WriteLine(e);
            }

            return cartoes;
        }
    }
}
