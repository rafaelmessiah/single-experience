
using SingleExperience.Services.Produto;
using System;
using System.Collections.Generic;
using System.Text;
using SingleExperience.Entities.Enums;
using SingleExperience.Services.Carrinho.Models;
using SingleExperience.Services.Carrinho;
using System.IO;
using System.Globalization;
using System.Threading;
using SingleExperience.Services.Compra;
using SingleExperience.Services.Compra.Models;
using SingleExperience.Services.ListaProdutoCompra;

namespace SingleExperience.Views
{
    public class Home
    {
        Header header = new Header();
        Footer footer = new Footer();
        ProdutoView produtoView = new ProdutoView();
        CompraView compraView = new CompraView();
        CarrinhoView carrinhoView = new CarrinhoView();

        public void Menu()
        {
            header.Exibir();

            Console.WriteLine("|  Abas:                                                 |");
            Console.WriteLine("|  1 - Ver Produtos Dísponiveis                          |");
            Console.WriteLine("|  2 - Vizualizar Meu Carrinho                           |");
            Console.WriteLine("|  3 - Vizualizar Meus Pedidos                           |");
            Console.WriteLine("|  0 - Sair                                              |");
            Console.WriteLine(" ========================================================");
            Console.Write("Digite a aba que Deseja Vizualizar: ");
            var op = Console.ReadLine();
            Navegar(op);
            Console.Clear();
        }

        public void Navegar(string op)
        {
            switch (op)
            {
                case "1":
                    produtoView.MenuProdutos();
                    Console.Clear();
                    Menu();
                    break;
                case "2":
                    carrinhoView.Vizualizar();
                    Console.Clear();
                    Menu();
                    break;
                case "3":
                    compraView.VizualizarCompras();
                    Console.Clear();
                    Menu();
                    break;
                case "0":
                    footer.Exibir();
                    break;
                default:
                    Menu();
                    break;
            }
        }

        public void Carrinho()
        {
            Console.Clear();
            header.Exibir();


            Console.WriteLine("==================");
            Console.WriteLine("|| Meu Carrinho ||");
            Console.WriteLine("==================");
            var carrinhoService = new CarrinhoService();

            var carrinhos = carrinhoService.Buscar(1);

            Console.WriteLine("");
            Console.WriteLine("=================================================================================");
            Console.WriteLine("|| PRODUTOS                                                                    ||");
            Console.WriteLine("=================================================================================");

            carrinhos.ForEach(a =>
            {
                Console.WriteLine("---------------------------------------------------------------------------------");
                Console.WriteLine($"|| ID: {a.CarrinhoId} || Nome: {a.Nome} | Preço: {a.Preco.ToString("F2", CultureInfo.InvariantCulture)} | Qtd: {a.Qtde} ||");
                Console.WriteLine("---------------------------------------------------------------------------------");
            });
            Console.WriteLine("=================================================================================");

            Console.WriteLine("==============================");
            Console.WriteLine("|| VALOR TOTAL: R$ " + carrinhoService.CalcularValorTotal(1).ToString("F2", CultureInfo.InvariantCulture) + "||");
            Console.WriteLine("==============================");
            Console.WriteLine("");

            Console.WriteLine("=================================================");
            Console.WriteLine("=== Opções ======================================");
            Console.WriteLine("| 1 - Alterar Quantidade de Um Produto          |");
            Console.WriteLine("| 2 - Remover Um Produtodo Carrinho             |");
            Console.WriteLine("| 3 - Finalizar Compra                          |");
            Console.WriteLine("| 4 - Retornar a Página Principal               |");
            Console.WriteLine(" ===============================================");
            var op = Console.ReadLine().ToLower();

            switch (op)
            {
                case "1":
                    var edicaoQtdeModel = new EdicaoQtdeModel();

                    Console.Write("Digite o Id do produto que você quer alterar: ");
                    edicaoQtdeModel.CarrinhoId = int.Parse(Console.ReadLine());

                    Console.Write("Digite a Nova quantidade Desejada: ");
                    edicaoQtdeModel.Qtde = int.Parse(Console.ReadLine());

                    if (carrinhoService.AlterarQtde(edicaoQtdeModel))
                    {
                        Console.Clear();
                        Console.WriteLine("Quantidade Alterada com Sucesso");
                        Thread.Sleep(TimeSpan.FromSeconds(2.00));
                        Carrinho();
                    }
                    break;

                case "2":
                    var editarStatusModel = new EdicaoStatusModel();
                    editarStatusModel.StatusEnum = StatusCarrinhoProdutoEnum.Excluido;

                    Console.WriteLine("Digite o Id do Produto que voce quer Remover");
                    editarStatusModel.CarrinhoId = int.Parse(Console.ReadLine());

                    if (carrinhoService.AlterarStatus(editarStatusModel))
                    {
                        Console.Clear();
                        Console.WriteLine("Produto Removido do Carrinho com sucesso!");
                        Thread.Sleep(TimeSpan.FromSeconds(2.00));
                        Carrinho();
                    }

                    break;
                case "3":

                    Console.WriteLine("");
                    Console.WriteLine("Confirme Seus Dados: ");
                    Console.Write("Nome: ");
                    Console.ReadLine();
                    Console.Write("Telefone: ");
                    Console.ReadLine();
                    Console.Write("Endereco: ");
                    Console.ReadLine();
                    compraView.Finalizar(1);

                    break;
                case "4":
                    Console.Clear();
                    Menu();
                    break;
                default:
                    Carrinho();
                    break;
            }

        }

     }
}