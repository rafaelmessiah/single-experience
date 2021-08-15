
using SingleExperience.Services.CartaoCredito.Models;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Threading;

namespace SingleExperience.Services.CartaoCredito
{
    public class CartaoCreditoService
    {
        protected readonly SingleExperience.Context.Context _context;

        public CartaoCreditoService(SingleExperience.Context.Context context)
        {
            _context = context;
        }

        public List<CartaoItemModel> Buscar(int clienteId)
        {
            return _context.CartaoCredito
                .Where(a => a.ClienteId == clienteId)
                .Select(a => new CartaoItemModel
                {
                    ClienteId = a.ClienteId,
                    CartaoCreditoId = a.CartaoCreditoId,
                    Numero = a.Numero

                }).ToList();

        }

        public bool Cadastrar(CadastroCartaoModel model)
        {
            model.Validar();

            var cartao = new Entities.CartaoCredito
            {
                ClienteId = model.ClienteId,
                Numero = model.Numero,
                Bandeira = model.Bandeira,
                CodigoSeguranca = model.CodigoSeguranca,
                DataVencimento = model.DataVencimento
            };

            _context.CartaoCredito.Add(cartao);
            _context.SaveChanges();

            return true;
        }

        public CartaoDetalhadoModel Obter(CartaoClienteModel model)
        {
            var cartao = new CartaoDetalhadoModel();
            try
            {
                cartao = _context.CartaoCredito
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
            catch (Exception e)
            {
                Console.WriteLine(e);
                Thread.Sleep(3500);
            }

            return cartao;
        }

        public bool Verificar(VerificarCartaoModel model)
        {
            var codigoSeguranca = _context.CartaoCredito
                .Where(a => a.CartaoCreditoId == model.CartaoCredtioId && a.ClienteId == model.ClienteId)
                .Select(a => a.CodigoSeguranca)
                .FirstOrDefault();

            if (codigoSeguranca == null)
                throw new Exception("Não foi possível encontrar esse cartão");

            if (codigoSeguranca != model.CodigoSeguranca)
                throw new Exception("Código de Segurança Inválido");

            return true;
        }
    }
}
