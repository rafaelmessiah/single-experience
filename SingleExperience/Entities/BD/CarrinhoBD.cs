using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.IO;
using SingleExperience.Entities.Enums;

namespace SingleExperience.Entities.BD
{
    class CarrinhoBD
    {
        string path = @"C:\Workspaces\visual_studio_2019\single-experience\Tabelas\Carrinho.csv";

        public List<CarrinhoEntity> ListarCarrinho()
        {
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

        public void Salvar(int clienteId, int produtoId)
        {
            try
            {
                using (var streamWriter = File.AppendText(path))
                {
                    streamWriter.WriteLine($"{File.ReadAllLines(path).Length}," +
                        $"{produtoId}," +
                        $"{clienteId}," +
                        $"{StatusCarrinhoProdutoEnum.Inserido}");
                }
            }
            catch (Exception)
            {
                throw new Exception("Não foi possivel inserir o produto no carrinho");
            }
            
        }

        public void Alterar(int carrinhoId, StatusCarrinhoProdutoEnum statusCarrinhoProdutoId)
        {
            try
            {
                using (var streamWriter = File.)
                {

                }
            }
        }
    }
}
