using SingleExperience.Entities.Enums;
using SingleExperience.Services.Carrinho;
using SingleExperience.Services.Carrinho.Models;
using SingleExperience.Services.Produto;
using System;
using System.Globalization;
using System.IO;


namespace SingleExperience.Views
{
    class ProdutoView
    {
        ProdutoService produtoService = new ProdutoService();
        Header header = new Header();

        public void MenuProdutos()
        {
            Console.Clear();
            header.Exibir();
            Console.WriteLine("|  Categorias:                                           |");
            Console.WriteLine("|  1 - Computadores                                      |");
            Console.WriteLine("|  2 - Notebooks                                         |");
            Console.WriteLine("|  3 - Acessorios                                        |");
            Console.WriteLine("|  4 - Celulares                                         |");
            Console.WriteLine("|  5 - Tablets                                           |");
            Console.WriteLine("|  0 - Retornar a página Anterior                        |");
            Console.WriteLine(" ========================================================");
            Console.Write("Digite a aba que Deseja Vizualizar: ");
            var op = Console.ReadLine();
            Navegar(op);
        }

        public void Navegar(string op)
        {
            switch (op)
            {
                case "1":
                    ListarProdutos(CategoriaEnum.Computador);
                    Console.Clear();
                    MenuProdutos();
                    break;
                case "2":
                    ListarProdutos(CategoriaEnum.Notebook);
                    Console.Clear();
                    MenuProdutos();
                    break;
                case "3":
                    ListarProdutos(CategoriaEnum.Acessorio);
                    Console.Clear();
                    MenuProdutos();
                    break;
                case "4":
                    ListarProdutos(CategoriaEnum.Celular);
                    Console.Clear();
                    MenuProdutos();
                    break;
                case "5":
                    ListarProdutos(CategoriaEnum.Tablet);
                    Console.Clear();
                    MenuProdutos();
                    break;
                case "0":
                    Console.Clear();
                    break;
                default:
                    Console.Clear();
                    MenuProdutos();
                    break;
            }
        }

        public void ListarProdutos(CategoriaEnum categoria)
        {
            Console.Clear();
                        
            var produtos = produtoService.BuscarCategoria(categoria);

            Console.WriteLine("============================");
            Console.WriteLine($"|| Categoria: {categoria} ||");
            Console.WriteLine("============================");

            Console.WriteLine();

            produtos.ForEach(a =>
            {
                Console.WriteLine("---------------------------------------------------------------------------------");
                Console.WriteLine($"|| ID: {a.ProdutoId} || Nome: {a.Nome} | Preço: {a.Preco.ToString("F2", CultureInfo.InvariantCulture)} ||");
                Console.WriteLine("---------------------------------------------------------------------------------"); ;
            });

            Console.WriteLine("");
            Console.Write("Deseja vizualizar algum produto? (y/n) :");
            var op = Console.ReadLine().ToLower();

            switch (op)
            {
                case "y":
                   
                    ProdutoDetalhado();
                    break;
                case "n":
                    break;
                default:
                    ListarProdutos(categoria);
                    break;
            }
        }

        public void ProdutoDetalhado()
        {
            Console.Write("Digite o Id do produto que deseja vizualizar: ");
            var produtoId = int.Parse(Console.ReadLine());

            try
            {
                var produto = produtoService.ObterDetalhe(produtoId);

                Console.WriteLine("---------------------------------------------------------------------------------");
                Console.WriteLine($"|| NOME: {produto.Nome} | ");
                Console.WriteLine($"|| Descrição:  {produto.Descricao} || ");
                Console.WriteLine($"|| Preço: {produto.Preco.ToString("F2", CultureInfo.InvariantCulture)} ");
                Console.WriteLine("---------------------------------------------------------------------------------");
                Console.Write("Deseja adicionar o produto no carrinho? (y/n): ");
                var op = Console.ReadLine().ToLower();

                switch (op)
                {
                    case "y":
                        Console.WriteLine("Digite a quantidade: ");
                        var salvarModel = new SalvarModel
                        {
                            ClienteId = 1,
                            ProdutoId = produto.ProdutoId,
                            Qtde = int.Parse(Console.ReadLine())
                        };

                        var carrinhoService = new CarrinhoService();

                        try
                        {
                            carrinhoService.Adicionar(salvarModel);
                        }
                        catch (IOException e)
                        {
                            Console.WriteLine("Ocurred an error");
                            Console.WriteLine(e.Message);
                        }
                        Console.Clear();
                        break;
                    case "n":
                        break;
                    default:
                        ProdutoDetalhado();
                        break;
                }

            }
            catch (IOException e)
            {
                Console.WriteLine(e);
                Console.WriteLine("Tente Novamente ");
                ProdutoDetalhado();
            }

           
        }
    }
}
