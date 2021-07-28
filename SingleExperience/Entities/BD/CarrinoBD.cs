using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.IO;
using SingleExperience.Entities.Enums;

namespace SingleExperience.Entities.BD
{
    class CarrinoBD
    {
        List<CarrinhoEntity> ListaProdutosCompras()
        {
            var path = @"C:\Users\rafael.messias\source\repos\SingleExperience\Tabelas\ListaProdutoCompraEntity.csv";

            var listaCarrinho = new List<CarrinhoEntity>();

            try
            {
                var carrinhos = File.ReadAllLines(path, Encoding.UTF8);

                carrinhos.Skip(1)
                    .ToList()
                    .ForEach(c =>
                    {
                        var campos = c.Split(",");
                        var carrinho = new CarrinhoEntity();

                        carrinho.CarrinhoId = int.Parse(campos[0]);
                        carrinho.Produtoid = int.Parse(campos[1]);
                        carrinho.ClienteId = int.Parse(campos[2]);

                        Enum.TryParse(campos[3], out StatusCarrinhoProdutoEnum statusCarrinhoProdutoEnum);
                        carrinho.StatusCarrinhoProdutoId = statusCarrinhoProdutoEnum;


                        listaCarrinho.Add(carrinho);
                    });
            }
            catch (Exception)
            {

                throw;
            }

            return listaCarrinho;
        }
    }
}
