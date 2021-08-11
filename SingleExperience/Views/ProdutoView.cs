using SingleExperience.Entities.Enums;
using SingleExperience.Services.Carrinho;
using SingleExperience.Services.Carrinho.Models;
using SingleExperience.Services.Cliente.Models;
using SingleExperience.Services.Produto;
using System;
using System.Globalization;
using System.IO;


namespace SingleExperience.Views
{
    class ProdutoView
    {
        static SingleExperience.Context.Context context = new SingleExperience.Context.Context();
        ProdutoService produtoService = new ProdutoService(context);
        CarrinhoService carrinhoService = new CarrinhoService(context);
        Header header = new Header();

        public void MenuProdutos(ClienteLogadoModel clienteLogado)
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
            Navegar(op, clienteLogado);
        }

        public void Navegar(string op, ClienteLogadoModel clienteLogado)
        {
            switch (op)
            {
                case "1":
                    ListarProdutos(CategoriaEnum.Computador, clienteLogado);
                    Console.Clear();
                    MenuProdutos(clienteLogado);
                    break;
                case "2":
                    ListarProdutos(CategoriaEnum.Notebook, clienteLogado);
                    Console.Clear();
                    MenuProdutos(clienteLogado);
                    break;
                case "3":
                    ListarProdutos(CategoriaEnum.Acessorio, clienteLogado);
                    Console.Clear();
                    MenuProdutos(clienteLogado);
                    break;
                case "4":
                    ListarProdutos(CategoriaEnum.Celular, clienteLogado);
                    Console.Clear();
                    MenuProdutos(clienteLogado);
                    break;
                case "5":
                    ListarProdutos(CategoriaEnum.Tablet, clienteLogado);
                    Console.Clear();
                    MenuProdutos(clienteLogado);
                    break;
                case "0":
                    Console.Clear();
                   
                    break;
                default:
                    Console.Clear();
                    MenuProdutos(clienteLogado);
                    break;
            }
        }

        public void ListarProdutos(CategoriaEnum categoria, ClienteLogadoModel clienteLogado)
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
                   
                    ProdutoDetalhado(clienteLogado);
                    break;
                case "n":
                    break;
                default:
                    ListarProdutos(categoria, clienteLogado);
                    break;
            }
        }

        public void ProdutoDetalhado(ClienteLogadoModel clienteLogado)
        {
            Console.Write("Digite o Id do produto que deseja vizualizar: ");

            if(!int.TryParse(Console.ReadLine(), out int produtoId))
            {
                Console.WriteLine("Id invalido tente novamente");
                ProdutoDetalhado(clienteLogado);
            }
            
            if (!produtoService.Verificar(produtoId))
            {
                Console.WriteLine("Id invalido tente novamente");
                ProdutoDetalhado(clienteLogado);
            }

            try
            {
                ProdutoService produtoService = new ProdutoService(context);
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
                            ClienteId = clienteLogado.ClienteId,
                            ProdutoId = produto.ProdutoId,
                            Qtde = int.Parse(Console.ReadLine())
                        };

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
                        ProdutoDetalhado(clienteLogado);
                        break;
                }

            }
            catch (IOException e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine("Tente Novamente ");
                ProdutoDetalhado(clienteLogado);
            }
           
        }
    }
}
