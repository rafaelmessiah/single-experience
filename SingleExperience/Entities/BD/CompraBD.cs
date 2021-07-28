using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.IO;
using SingleExperience.Entities;

namespace SingleExperience.Entities.BD
{
    class CompraBD
    {
        public List<CompraEntity> ListarCompras()
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
                        compra.SituacaoCompraId = int.Parse(campos[1]);
                        compra.FormaPagamentoId = int.Parse(campos[2]);
                        compra.DataCompra = DateTime.Parse(campos[3]);
                        compra.DataPagamento = DateTime.Parse(campos[4]);
                        compra.ValorFinal = double.Parse(campos[5]);

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
