using SingleExperience.Entities.Enums;
using SingleExperience.Services.Compra;
using SingleExperience.Services.Compra.Models;
using SingleExperience.Services.ListaProdutoCompra;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace SingleExperience.Views
{
    class CompraView
    {
        public void VizualizarCompras()
        {
            Console.Clear();
            var compraService = new CompraService();
            var compras = compraService.Buscar(1);

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
                $"Situação Pagamento: {compra.StatusPagamento} | Data Pagameto: {compra.DataPagamento}  ||");
            Console.WriteLine("==============================");
            Console.WriteLine("");

            Console.WriteLine("Digite Enter para voltar a tela anterior");
            Console.ReadLine();
            VizualizarCompras();
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
                EnderecoId = 1,
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
                    }
                    break;
                default:
                    Console.WriteLine("Opção Invalida, tente novamente");
                    Finalizar(clienteId);
                    break;
            }
        }
    }
}
