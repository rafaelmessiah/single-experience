using SingleExperience.Entities.BD;
using SingleExperience.Services.Compra.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using SingleExperience.Entities.Enums;

namespace SingleExperience.Services.CompraService
{
    public class CompraService
    {
        CompraBD compraBd = new CompraBD();

        public List<ItemModel> BuscarCompras(int clienteId)
        {
            var compras = compraBd.BuscarCompras()
                .Where(a => a.ClienteId == clienteId)
                .Select(b => new ItemModel
                {
                    CompraId = b.CompraId,
                    StatusCompra = b.StatusCompraId,
                    DataCompra = b.DataCompra,
                    ValorFinal = b.ValorFinal
                }).ToList();

            if (compras == null)
                throw new Exception("Não foi possivel encontrar compras desse usuário");

            return compras;
                
        }

        public CompraDetalhadaModel Obter(int compraId)
        {
            var compra = compraBd.BuscarCompras()
                .Where(a => a.CompraId == compraId)
                .Select(b => new CompraDetalhadaModel
                {
                    CompraId = b.CompraId,
                    StatusPagamentoId = b.StatusPagamentoId,
                    FormaPagamentoId = b.FormaPagamentoId,
                    DataCompra = b.DataCompra,
                    DataPagamento = b.DataPagamento,
                    ValorTotal = b.ValorFinal

                }).FirstOrDefault();

            if (compra == null)
                throw new Exception("Compra Não Encontrada");

            return compra;
        }

       
    }
}
