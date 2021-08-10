
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
using SingleExperience.Services.Cliente.Models;
using SingleExperience.Services.Cliente;

namespace SingleExperience.Views
{
    public class Home
    {
        Header header = new Header();
        Footer footer = new Footer();
        ProdutoView produtoView = new ProdutoView();
        CompraView compraView = new CompraView();
        CarrinhoView carrinhoView = new CarrinhoView();
        ClienteView clienteView = new ClienteView();
        ClienteService clienteService = new ClienteService();

        public void Inicio()
        {
            Console.Clear();
            Console.WriteLine("Para continuar é necessario Logar ou Se Cadastrar");

            Console.WriteLine("=================================================");
            Console.WriteLine("=== Opções ======================================");
            Console.WriteLine("| 1 - Logar                                     |");
            Console.WriteLine("| 2 - Cadastrar                                 |");
            Console.WriteLine("| 0 - Sair                                      |");
            Console.WriteLine(" ===============================================");
            var op = Console.ReadLine().ToLower();

            var clienteLogado = new ClienteLogadoModel();

            switch (op)
            {
                case "1":

                    var login = new LoginModel();

                    Console.Write("Digite seu Email: ");
                    login.Email = Console.ReadLine();

                    Console.Write("Digite sua Senha: ");
                    login.Senha = Console.ReadLine();

                    try
                    {
                        clienteLogado = clienteService.Login(login);

                        if (clienteLogado == null)
                        {
                            Console.WriteLine("Email ou senha incorretos, tente novamente");
                            Inicio();
                        }
                    }
                    catch (IOException e)
                    {
                        Console.WriteLine("Ocorreu um Erro");
                        Console.WriteLine(e);
                    }

                    Menu(clienteLogado);
                    break;

                case "2":

                    var cadastroClienteModel = new CadastroClienteModel();

                    Console.Write("Digite seu Nome: ");
                    cadastroClienteModel.Nome = Console.ReadLine();

                    Console.Write("Digite seu Cpf: ");
                    cadastroClienteModel.Cpf = Console.ReadLine();

                    Console.Write("Digite seu Email: ");
                    cadastroClienteModel.Email = Console.ReadLine();

                    Console.Write("Digite sua Senha: ");
                    cadastroClienteModel.Senha = Console.ReadLine();

                    Console.WriteLine("Digite sua Data de Nascimento (DD/MM/AAAA) : ");
                    cadastroClienteModel.DataNascimento = DateTime.Parse(Console.ReadLine());

                    Console.WriteLine("Digite seu Telefone: ");
                    cadastroClienteModel.Telefone = Console.ReadLine();

                    try
                    {
                        if (clienteService.Cadastrar(cadastroClienteModel))
                        {
                            Console.WriteLine("Cadastro realizado com sucesso, Agora você pode logar normalmente");
                            
                            login = new LoginModel();
                            login.Email = cadastroClienteModel.Email;
                            login.Senha = cadastroClienteModel.Senha;

                            clienteLogado = clienteService.Login(login);
                        }
                    }
                    catch (IOException e)
                    {
                        Console.WriteLine("Ocorreu um erro");
                        Console.WriteLine(e);
                    }
                    Menu(clienteLogado);
                    break;
                case "0":
                    Console.Clear();
                    break;
                default:
                    Console.Clear();
                    Inicio();
                    break;
            }

        }

        public void Menu(ClienteLogadoModel clienteLogado)
        {
            Console.Clear();
            header.Exibir();
            Console.WriteLine("  Bem Vindo(a): " + clienteLogado.Nome.ToString());
            Console.WriteLine("|                                                        |");
            Console.WriteLine("|  Abas:                                                 |");
            Console.WriteLine("|  1 - Ver Produtos Disponíveis                          |");
            Console.WriteLine("|  2 - Vizualizar Meu Carrinho                           |");
            Console.WriteLine("|  3 - Vizualizar Meus Pedidos                           |");
            Console.WriteLine("|  4 - Meu Perfil                                        |");
            Console.WriteLine("|  0 - Sair                                              |");
            Console.WriteLine(" ========================================================");
            Console.Write("Digite a aba que Deseja Vizualizar: ");
            var op = Console.ReadLine();
            Navegar(op, clienteLogado);
            Console.Clear();
        }

        public void Navegar(string op, ClienteLogadoModel clienteLogado)
        {
            switch (op)
            {
                case "1":
                    produtoView.MenuProdutos(clienteLogado);
                    Console.Clear();
                    Menu(clienteLogado);
                    break;
                case "2":
                    carrinhoView.Vizualizar(clienteLogado);
                    Console.Clear();
                    Menu(clienteLogado);
                    break;
                case "3":
                    compraView.VizualizarCompras(clienteLogado);
                    Console.Clear();
                    Menu(clienteLogado);
                    break;
                case "4":
                    clienteView.VizualizarPerfil(clienteLogado);
                    Console.Clear();
                    Menu(clienteLogado);
                    break;
                case "0":
                    footer.Exibir();
                    break;
                default:
                    Menu(clienteLogado);
                    break;
            }
        }

     }
}