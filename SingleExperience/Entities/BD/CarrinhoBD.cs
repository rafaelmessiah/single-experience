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
                        carrinho.Qtde = int.Parse(campos[3]);
                        Enum.TryParse(campos[4], out StatusCarrinhoProdutoEnum statusCarrinhoProdutoEnum);
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
                var carrinhoId = BuscarCarrinho().Count + 1;

                using (var streamWriter = File.AppendText(path))
                {
                    var statusCarrinhoId = ((int)StatusCarrinhoProdutoEnum.Ativo);

                    var aux = new string[]
                    {
                        carrinhoId.ToString(),
                        model.ProdutoId.ToString(),
                        model.ClienteId.ToString(),
                        model.Qtde.ToString(),
                        statusCarrinhoId.ToString()
                    };

                    streamWriter.WriteLine(String.Join(",", aux));
                }

            }
            catch (Exception)
            {
                throw new Exception("Não foi possivel inserir o produto no carrinho");
            }

            return true;

        }

        public bool AlterarStatus(EdicaoStatusModel model)
        {
            try
            {
                var carrinhos = BuscarCarrinho();

                var index = carrinhos
                    .FindIndex(a => a.CarrinhoId == model.CarrinhoId);

                carrinhos[index].StatusCarrinhoProdutoId = model.StatusEnum;

                // Gera as linhas para colocar no csv

                var linhas = new List<string>();

                linhas.Add(header);

                foreach (var item in carrinhos)
                {
                    var statusCarrinhoId = ((int)item.StatusCarrinhoProdutoId);

                    var aux = new string[]
                    {
                      item.CarrinhoId.ToString(),
                      item.ProdutoId.ToString(),
                      item.ClienteId.ToString(),
                      item.Qtde.ToString(),
                      statusCarrinhoId.ToString()
                    };

                    linhas.Add(String.Join(",", aux));
                }

                File.WriteAllLines(path, linhas);

            }
            catch (IOException e)
            {
                Console.WriteLine("Ocurred an error");
                Console.WriteLine(e.Message);
            }

            return true;
        }

        public bool AlterarQtde(EdicaoQtdeModel model)
        {
            try
            {
                var carrinhos = BuscarCarrinho();

                var index = carrinhos
                    .FindIndex(a => a.CarrinhoId == model.CarrinhoId);

                carrinhos[index].Qtde = model.Qtde;

                // Gera as linhas para colocar no csv

                var linhas = new List<string>();

                linhas.Add(header);

                foreach (var item in carrinhos)
                {
                    var statusCarrinhoId = ((int)item.StatusCarrinhoProdutoId);

                    var aux = new string[]
                    { item.CarrinhoId.ToString(),
                      item.ProdutoId.ToString(),
                      item.ClienteId.ToString(),
                      item.Qtde.ToString(),
                      statusCarrinhoId.ToString()
                    };

                    linhas.Add(String.Join(",", aux));
                }

                File.WriteAllLines(path, linhas);

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
