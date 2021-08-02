
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
    class Body
    {
        Header header = new Header();
        Footer footer = new Footer();
        ProdutoService produtoService = new ProdutoService();

        public void Menu()
        {
            header.Exibir();

            Console.WriteLine("|  Categorias:                                           |");
            Console.WriteLine("|  1 - Computadores                                      |");
            Console.WriteLine("|  2 - Notebooks                                         |");
            Console.WriteLine("|  3 - Acessorios                                        |");
            Console.WriteLine("|  4 - Celulares                                         |");
            Console.WriteLine("|  5 - Tablets                                           |");
            Console.WriteLine("|  6 - Vizualizar Meu Carrinho                           |");
            Console.WriteLine("|  7 - Vizualizar Meus Pedidos                           |");
            Console.WriteLine("|  0 - Sair                                              |");
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
                   Produtos(CategoriaEnum.Computador);
                    break;
                case "2":
                    Produtos(CategoriaEnum.Notebook);
                    break;
                case "3":
                    Produtos(CategoriaEnum.Acessorio);
                    break;
                case "4":
                    Produtos(CategoriaEnum.Celular);
                    break;
                case "5":
                    Produtos(CategoriaEnum.Tablet);
                    break;
                case "6":
                    Carrinho();
                    break;
                case "7":
                    VizualizarCompras();
                    break;
                case "0":
                    footer.Exibir();
                    break;
                default:
                    Menu();
                    break;
            }
        }

        public void Produtos(CategoriaEnum categoria)
        {
            Console.Clear();

            header.Exibir();

            var produtos = produtoService.BuscarCategoria(categoria);

            Console.WriteLine("==================");
            Console.WriteLine($"|| Categoria: {categoria} ||");
            Console.WriteLine("==================");

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
                    Console.Clear();
                    Menu();
                    break;
                default:
                    Produtos(categoria);
                    break;
            }
        }

        public void ProdutoDetalhado()
        {
            Console.Write("Digite o Id do produto que deseja vizualizar: ");
            var produtoId = int.Parse(Console.ReadLine());

            Console.Clear();
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
                        Menu();
                    }
                    Console.Clear();
                    Menu();
                    break;
                case "n":
                    Console.Clear();
                    Menu();
                    break;
                default:
                    ProdutoDetalhado();
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
                    Finalizar(1);

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

        public void Finalizar(int clienteId)
        {
            Console.WriteLine("Escolha a forma de Pagamento: ");
            Console.WriteLine("=== Opções ======================================");
            Console.WriteLine("| 1 - Boleto                                    |");
            Console.WriteLine("| 2 - Pix                                       |");
            Console.WriteLine("| 3 - Cartao                                    |");
            Console.WriteLine("=================================================");

            var op = Console.ReadLine().ToLower();

            var compraService = new CompraService();

            var iniciarModel = new IniciarModel
            {
                ClienteId = clienteId,
            };

            switch (op)
            {
                case "1":
                    iniciarModel.FormaPagamentoId = FormaPagamentoEnum.Boleto;

                    if (compraService.Cadastrar(iniciarModel))
                    {
                        Console.WriteLine("Compra Cadastrada com Sucesso!");
                        var guid = Guid.NewGuid();
                        Console.WriteLine("O número do seu boleto é: " + guid);
                        Console.WriteLine("Aperte Enter para continuar");
                        Console.ReadLine();
                        Console.Clear();
                        Menu();
                    }
                    break;
                case "2":
                    iniciarModel.FormaPagamentoId = FormaPagamentoEnum.Pix;

                    if (compraService.Cadastrar(iniciarModel))
                    {
                        Console.WriteLine("Compra Cadastrada com Sucesso!");
                        var guid = Guid.NewGuid();
                        Console.WriteLine("O número do seu Pix é: " + guid);
                        Console.WriteLine("Aperte Enter para continuar");
                        Console.ReadLine();
                        Console.Clear();
                        Menu();
                    }
                    break;
                case "3":
                    iniciarModel.FormaPagamentoId = FormaPagamentoEnum.Cartao;

                    Console.Write("Confirme os dados do seu Cartão");
                    Console.ReadLine();
                    Console.Write("Numero:  ");
                    Console.ReadLine();
                    Console.Write("Codigo de Segurança: ");
                    Console.ReadLine();

                    if (compraService.Cadastrar(iniciarModel))
                    {
                        Console.Clear();
                        Console.WriteLine("Compra Cadastrada com Sucesso!");
                        Console.WriteLine("O pagamento foi Confirmado com sua Operadora");
                        Console.WriteLine("Aperte Enter para continuar");
                        Console.ReadLine();
                        Menu();
                    }
                    break;
                default:
                    Console.WriteLine("Opção Invalida, tente novamente");
                    Finalizar(clienteId);
                    break;
            }
        }

        public void VizualizarCompras()
        {
            Console.Clear();
            var compraService = new CompraService();
            var compras = compraService.BuscarCompras(1);

            compras.ForEach(a =>
            {
                Console.WriteLine("---------------------------------------------------------------------------------");
                Console.WriteLine($"|| PEDIDO: {a.CompraId} || Valor Total: {a.ValorFinal} | " +
                    $"Data Compra: {a.DataCompra} ||");
                Console.WriteLine("---------------------------------------------------------------------------------");
            });

            Console.WriteLine("Deseja ver algum pedido com mais detalhes? (y/n)");
            var op = Console.ReadLine().ToLower();

            switch (op)
            {
                case "y":
                    Console.WriteLine("Digite o Numero do pedido: ");
                    var compraId = int.Parse(Console.ReadLine());
                    VizualizarCompraDetalhada(compraId);
                    break;
                case "n":
                    Console.Clear();
                    Menu();
                    break;
                default:
                    VizualizarCompras();
                    break;
            }
        }

        public void VizualizarCompraDetalhada(int compraId)
        {
            Console.Clear();
            var compraService = new CompraService();
            var compra = compraService.Obter(compraId);
            var listaProdutoCompraService = new ListaProdutoCompraService();
            var itensComprados = listaProdutoCompraService.Buscar(compraId);

            Console.WriteLine("============================");
            Console.WriteLine($"|| PEDIDO Nº: {compra.CompraId} ||");
            Console.WriteLine("============================");
            Console.WriteLine("");
            Console.WriteLine("=================================================================================");
            Console.WriteLine("|| PRODUTOS                                                                    ||");
            Console.WriteLine("=================================================================================");

            itensComprados.ForEach(a =>
            {
                Console.WriteLine("---------------------------------------------------------------------------------");
                Console.WriteLine($"|| Nome: {a.Nome} | Preço: {a.PrecoUnitario.ToString("F2", CultureInfo.InvariantCulture)} | Qtd: {a.Qtde} ||");
                Console.WriteLine("---------------------------------------------------------------------------------");
            });

            Console.WriteLine("==============================");
            Console.WriteLine($"|| VALOR TOTAL: R$ {compra.ValorTotal.ToString("F2", CultureInfo.InvariantCulture)} | Data da Compra: {compra.DataCompra} | " +
                $"Situação Pagamento: {compra.StatusPagamentoId} | Data Pagameto: {compra.DataPagamento}  ||");
            Console.WriteLine("==============================");
            Console.WriteLine("");

            Console.WriteLine("Digite Enter para voltar a tela anterior");
            Console.ReadLine();
            VizualizarCompras();
        }
    }
}