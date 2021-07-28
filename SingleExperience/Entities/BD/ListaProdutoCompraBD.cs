using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.IO;

namespace SingleExperience.Entities.BD
{
    class ListaProdutoCompraBD
    {
        List<ListaProdutosCompraEntity> ListaProdutosCompras()
        {
            var path = @"C:\Users\rafael.messias\source\repos\SingleExperience\Tabelas\ListaProdutoCompraEntity.csv";

            var listaProduto = new List<ListaProdutosCompraEntity>();

            try
            {
                var produtosCompra = File.ReadAllLines(path, Encoding.UTF8);

                produtosCompra.Skip(1)
                    .ToList()
                    .ForEach(p =>
                    {
                        var campos = p.Split(",");
                        var produtoCompra = new ListaProdutosCompraEntity();

                        produtoCompra.ListaProdutosCompraId = int.Parse(campos[0]);
                        produtoCompra.CompraId = int.Parse(campos[1]);
                        produtoCompra.ProdutoId = int.Parse(campos[2]);
                        produtoCompra.Qtde = int.Parse(campos[3]);
                        produtoCompra.Valor = double.Parse(campos[4]);
                        

                        listaProduto.Add(produtoCompra);
                    });
            }
            catch (Exception)
            {

                throw;
            }

            return listaProduto;
        }
    }
}
