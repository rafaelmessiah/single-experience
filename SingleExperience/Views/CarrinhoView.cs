using SingleExperience.Entities.Enums;
using SingleExperience.Services.Carrinho;
using SingleExperience.Services.Carrinho.Models;
using SingleExperience.Services.Cliente;
using SingleExperience.Services.Cliente.Models;
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
        ClienteService clienteService = new ClienteService();

        public void Vizualizar(ClienteLogadoModel clienteLogado)
        {
            Console.Clear();
            
            Console.WriteLine("==================");
            Console.WriteLine("|| Meu Carrinho ||");
            Console.WriteLine("==================");
            
            var carrinhos = carrinhoService.Buscar(clienteLogado.ClienteId);

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
            Console.WriteLine("|| VALOR TOTAL: R$ " + carrinhoService.CalcularValorTotal(clienteLogado.ClienteId).ToString("F2", CultureInfo.InvariantCulture) + "||");
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
                        Vizualizar(clienteLogado);
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
                        Vizualizar(clienteLogado);
                    }

                    break;
                case "3":

                    var verificarCliente = new VerificarClienteModel();
                    verificarCliente.ClienteId = clienteLogado.ClienteId;
                    
                    Console.WriteLine("Confirme seus Dados: ");
                    
                    Console.Write("Email: ");
                    verificarCliente.Email = Console.ReadLine();

                    Console.Write("Senha: ");
                    verificarCliente.Senha = Console.ReadLine();

                    if (!clienteService.Verificar(verificarCliente))
                    {
                        Console.WriteLine("Dados Invalidos, tente novamente");
                        Thread.Sleep(TimeSpan.FromSeconds(1.5));
                        Vizualizar(clienteLogado);
                    }
                    else
                    {
                        compraView.Finalizar(clienteLogado.ClienteId);
                    }
                    break;
                case "4":
                    Console.Clear();
                    break;
                default:
                    Vizualizar(clienteLogado);
                    break;
            }

        }

    }
}
