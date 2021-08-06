using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using SingleExperience.Entities.Enums;
using System.Globalization;
using SingleExperience.Services.Produto.Models;

namespace SingleExperience.Entities.BD
{
    class ProdutoBD
    {
        string path = @"C:\Users\rafael.messias\source\repos\SingleExperience\Tabelas\Produto.csv";
        string header = "";

        public List<ProdutoEntity> BuscarProdutos()
        {
            
            List<ProdutoEntity> listaProduto = new List<ProdutoEntity>();

            try
            {
                var produtos = File.ReadAllLines(path);

                header = produtos[0];
                
                produtos.Skip(1)
                    .ToList()
                    .ForEach(p =>
                    {
                        var campos = p.Split(",");
                        
                        var produto = new ProdutoEntity();
                        produto.ProdutoId = int.Parse(campos[0]);

                        Enum.TryParse(campos[1], out CategoriaEnum categoria);
                        produto.CategoriaId = categoria;
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

        public bool AlterarQtde (AlterarQtdeModel model)
        {
            try
            {
                var produtos = BuscarProdutos();

                var index = produtos
                    .FindIndex(a => a.ProdutoId == model.ProdutoId);

                produtos[index].QtdeEmEstoque -= model.Qtde;

                var linhas = new List<string>();

                linhas.Add(header);

                foreach (var item in produtos)
                {
                    var categoriaId = ((int)item.CategoriaId);

                    var aux = new string[]
                    {
                      item.ProdutoId.ToString(),
                      categoriaId.ToString(),
                      item.Nome.ToString(),
                      item.Preco.ToString("F2", CultureInfo.InvariantCulture),
                      item.Detalhe.ToString(),
                      item.QtdeEmEstoque.ToString(),
                      item.Ranking.ToString(),
                      item.Disponivel.ToString()
                    };

                    linhas.Add(String.Join(",", aux));
                }

                File.WriteAllLines(path, linhas);
            }
            catch (Exception)
            {

                throw new Exception("Não foi possivel retirar esse produto");
            }

            return true;
        }
    }
}
