using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using SingleExperience.Entities.Enums;
using System.Globalization;

namespace SingleExperience.Entities.BD
{
    class ProdutoBD
    {
        public List<ProdutoEntity> ListarProdutos()
        {
            string path = @"C:\Users\rafael.messias\source\repos\SingleExperience\Tabelas\Produto.csv";

            List<ProdutoEntity> listaProduto = new List<ProdutoEntity>();

            try
            {
                var produtos = File.ReadAllLines(path);
                
                produtos.Skip(1)
                    .ToList()
                    .ForEach(p =>
                    {
                        var campos = p.Split(",");

                        var produto = new ProdutoEntity();
                        produto.ProdutoId = int.Parse(campos[0]);

                        Enum.TryParse(campos[1], out CategoriaEnum categoria);
                        produto.CategoriaId = categoria;

                        Enum.TryParse(campos[2], out StatusProdutoEnum statusProduto);
                        produto.StatusProdutoId = statusProduto;

                        produto.Nome = campos[3];

                        produto.Preco = double.Parse(campos[4], CultureInfo.InvariantCulture);
                        produto.Detalhe = campos[5];
                        produto.QtdeEmEstoque = int.Parse(campos[6]);
                        produto.Ranking = campos[7];
                        produto.Disponivel = bool.Parse(campos[8]);

                        listaProduto.Add(produto);
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
