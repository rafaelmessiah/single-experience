using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.IO;
using SingleExperience.Entities;
using SingleExperience.Entities.Enums;

namespace SingleExperience.Entities.BD
{
    public class CompraBD
    {
        public List<CompraEntity> BuscarCompras()
        {
            var path = @"C:\Users\rafael.messias\source\repos\SingleExperience\Tabelas\Compra.csv";

            List<CompraEntity> listaCompra = new List<CompraEntity>();

            try
            {
                var compras = File.ReadAllLines(path, Encoding.UTF8);

                compras.Skip(1)
                    .ToList()
                    .ForEach(c =>
                    {
                        var campos = c.Split(",");

                        var compra = new CompraEntity();

                        compra.CompraId = int.Parse(campos[0]);
                        Enum.TryParse(campos[1], out StatusCompraEnum statusCompraEnum);
                        compra.StatusCompraId = statusCompraEnum;
                        Enum.TryParse(campos[2], out FormaPagamentoEnum formaPagamentoEnum);
                        compra.FormaPagamentoId = formaPagamentoEnum;
                        compra.ClienteId = int.Parse(campos[3]);
                        Enum.TryParse(campos[4], out StatusPagamentoEnum statusPagamentoEnum);
                        compra.StatusPagamentoId = statusPagamentoEnum;
                        compra.DataCompra = DateTime.Parse(campos[5]);
                        compra.DataPagamento = DateTime.Parse(campos[6]);
                        compra.ValorFinal = double.Parse(campos[7]);

                        listaCompra.Add(compra);
                    });
            }
            catch (Exception)
            {

                throw;
            }

            return listaCompra;
        }

        
    }
}
