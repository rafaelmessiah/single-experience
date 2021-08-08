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

        public CartaoDetalhadoModel Obter(CartaoClienteModel model)
        {
            var cartao = new CartaoDetalhadoModel();
            try
            {
                cartao = cartaoCreditoBd.Buscar()
                    .Where(a => a.CartaoCreditoId == model.CartaoCreditoId)
                    .Select(a => new CartaoDetalhadoModel
                    {
                        CartaoCreditoId = a.CartaoCreditoId,
                        ClienteId = a.ClienteId,
                        DataVencimento = a.DataVencimento,
                        Numero = a.Numero,
                    }).FirstOrDefault();

                if (cartao == null)
                    throw new Exception("Não foi possível encontrar esse cartão");
                
            }
            catch (IOException e)
            {

                Console.WriteLine("Ocorreu um erro");
                Console.WriteLine(e);
            }

            return cartao;
        }

        public bool Verificar(VerificarCartaoModel model)
        {
            try
            {
                var codigoSeguranca = cartaoCreditoBd.Buscar()
                    .Where(a => a.CartaoCreditoId == model.CartaoCredtioId && a.ClienteId == model.ClienteId)
                    .Select(a => a.CodigoSeguranca)
                    .FirstOrDefault();

                if (codigoSeguranca == null)
                    throw new Exception("Não foi possível encontrar esse cartão");

                if(codigoSeguranca != model.CodigoSeguranca)
                    throw new Exception("Código de Segurança Inválido");

            }
            catch (Exception)
            {

                throw;
            }

            return true;
        }
    }
}
