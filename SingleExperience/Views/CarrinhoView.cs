using SingleExperience.Entities.Enums;
using SingleExperience.Services.Carrinho;
using SingleExperience.Services.Carrinho.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Threading;

namespace SingleExperience.Views
{
    class CarrinhoView
    {
        CarrinhoService carrinhoService = new  CarrinhoService();
        CompraView compraView = new CompraView();

        public void Vizualizar()
        {
            Console.Clear();
            
            Console.WriteLine("==================");
            Console.WriteLine("|| Meu Carrinho ||");
            Console.WriteLine("==================");
            
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
                        Vizualizar();
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
                        Vizualizar();
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
                    break;
                default:
                    Vizualizar();
                    break;
            }

        }

    }
}
