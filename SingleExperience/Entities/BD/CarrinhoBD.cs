using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.IO;
using SingleExperience.Entities.Enums;
using SingleExperience.Services.Carrinho.Models;

namespace SingleExperience.Entities.BD
{
    class CarrinhoBD
    {
        string path = @"C:\Users\rafael.messias\source\repos\SingleExperience\Tabelas\Carrinho.csv";
        string header = "";

        public List<CarrinhoEntity> BuscarCarrinho()
        {
            var listaCarrinho = new List<CarrinhoEntity>();

            try
            {
                var carrinhos = File.ReadAllLines(path, Encoding.UTF8);

                header = carrinhos[0];

                carrinhos.Skip(1)
                    .ToList()
                    .ForEach(c =>
                    {
                        var campos = c.Split(",");
                        var carrinho = new CarrinhoEntity();

                        carrinho.CarrinhoId = int.Parse(campos[0]);
                        carrinho.ProdutoId = int.Parse(campos[1]);
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

        public bool Salvar(SalvarModel model)
        {
            try
            {
                var carrinhoId = File.ReadAllLines(path).Length;

                using (var streamWriter = File.AppendText(path))
                {
                    streamWriter.WriteLine($"{carrinhoId}," +
                        $"{model.ProdutoId}," +
                        $"{model.ClienteId}," +
                        $"{StatusCarrinhoProdutoEnum.Ativo}");
                }
            }
            catch (Exception)
            {
                throw new Exception("Não foi possivel inserir o produto no carrinho");
            }

            return true;
            
        }

        public bool Alterar(EdicaoModel model)
        {
            try
            {
                var carrinhos = BuscarCarrinho();

                var index = carrinhos
                    .FindIndex(a => a.CarrinhoId == model.CarrinhoId);

                carrinhos[index].StatusCarrinhoProdutoId = model.StatusEnum;

                // Gera as linhas para colocar no csv

                var lines = new List<string>();

                lines.Add(header);

                foreach (var item in carrinhos)
                {
                    var aux = new string[] { item.CarrinhoId.ToString(), item.ProdutoId.ToString(), item.ClienteId.ToString(), item.StatusCarrinhoProdutoId.ToString()};
                    lines.Add(String.Join(",", aux));
                }

                File.WriteAllLines(path, lines);

            }
            catch (IOException e)
            {
                Console.WriteLine("Ocurred an error");
                Console.WriteLine(e.Message);
            }

            return true;
        }
    }
}
