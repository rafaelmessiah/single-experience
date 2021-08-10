using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.IO;
using SingleExperience.Services.ListaProdutoCompra.Models;

namespace SingleExperience.Entities.BD
{
    class ListaProdutoCompraBD
    {
        string path = @"C:\Users\rafael.messias\source\repos\SingleExperience\Tabelas\ListaProdutoCompra.csv";

        public List<ListaProdutoCompraEntity> BuscarProdutosCompras()
        {
            var listaProduto = new List<ListaProdutoCompraEntity>();

            try
            {
                var produtosCompra = File.ReadAllLines(path, Encoding.UTF8);

                produtosCompra.Skip(1)
                    .ToList()
                    .ForEach(p =>
                    {
                        var campos = p.Split(",");
                        var produtoCompra = new ListaProdutoCompraEntity();

                        produtoCompra.ListaProdutoCompraId = int.Parse(campos[0]);
                        produtoCompra.CompraId = int.Parse(campos[1]);
                        produtoCompra.ProdutoId = int.Parse(campos[2]);
                        produtoCompra.Qtde = int.Parse(campos[3]);
                     
                        

                        listaProduto.Add(produtoCompra);
                    });
            }
            catch (IOException e)
            {
                Console.WriteLine("Ocorreu um erro");
                Console.WriteLine(e);
            }
            
            return listaProduto;
        }

        public bool Salvar(CadastrarItemModel model)
        {
            var listaProdutoCompraId = BuscarProdutosCompras().Count + 1;

            try
            {
                using (var streamWriter = File.AppendText(path))
                {

                    var aux = new string[]
                    {
                       listaProdutoCompraId.ToString(),
                       model.CompraId.ToString(),
                       model.ProdutoId.ToString(),
                       model.Qtde.ToString(),
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
