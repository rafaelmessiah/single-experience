using SingleExperience.Entities.Enums;
using SingleExperience.Services.CartaoCredito;
using SingleExperience.Services.CartaoCredito.Models;
using SingleExperience.Services.Cliente.Models;
using SingleExperience.Services.Compra;
using SingleExperience.Services.Compra.Models;
using SingleExperience.Services.Endereco;
using SingleExperience.Services.Endereco.Models;
using SingleExperience.Services.ListaProdutoCompra;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Threading;

namespace SingleExperience.Views
{
    class CompraView
    {
        static SingleExperience.Context.Context context = new SingleExperience.Context.Context();
        CompraService compraService = new CompraService(context);
        EnderecoService enderecoService = new EnderecoService(context);
        CartaoCreditoService cartaoCreditoService = new CartaoCreditoService(context);
        ListaProdutoCompraService listaProdutoCompraService = new ListaProdutoCompraService(context);

        public void VizualizarCompras(ClienteLogadoModel clienteLogado)
        {
            Console.Clear();
            var compras = compraService.Buscar(clienteLogado.ClienteId);

            Console.WriteLine("============================");
            Console.WriteLine($"|| PEDIDOS               ||");
            Console.WriteLine("============================");

            compras.ForEach(a =>
            {
                Console.WriteLine("---------------------------------------------------------------------------------");
                Console.WriteLine($"|| PEDIDO: {a.CompraId} || Valor Total: {a.ValorFinal.ToString("F2", CultureInfo.InvariantCulture)} | " +
                    $"Data Compra: {a.DataCompra} ||");
                Console.WriteLine("---------------------------------------------------------------------------------");
            });

            Console.WriteLine("Deseja ver algum pedido com mais detalhes? (y/n)");
            var op = Console.ReadLine().ToLower();

            switch (op)
            {
                case "y":
                    VizualizarCompraDetalhada(clienteLogado);
                    break;
                case "n":
                    Console.Clear();
                    break;
                default:
                    VizualizarCompras(clienteLogado);
                    break;
            }
                        
        }

        public void VizualizarCompraDetalhada(ClienteLogadoModel clienteLogado)
        {
            Console.WriteLine("Digite o Numero do pedido: ");
            VerificarCompraModel model = new VerificarCompraModel();
            model.ClienteId = clienteLogado.ClienteId;

            if(!int.TryParse(Console.ReadLine(), out int compraId))
            {
                Console.WriteLine("Id Invalido tente novamente");
                VizualizarCompraDetalhada(clienteLogado);
            }

            model.CompraId = compraId;

            if (!compraService.Verificar(model))
            {
                Console.WriteLine("Id Invalido tente novamente");
                VizualizarCompraDetalhada(clienteLogado);
            }

            Console.Clear();
            var compra = compraService.Obter(model.CompraId);
            var itensComprados = listaProdutoCompraService.Buscar(model.CompraId);

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
            VizualizarCompras(clienteLogado);
        }

        public void Finalizar(int clienteId)
        {
            try
            {
                Console.WriteLine("Escolha a forma de Pagamento: ");
                Console.WriteLine("=== Opções ======================================");
                Console.WriteLine("| 1 - Boleto                                    |");
                Console.WriteLine("| 2 - Pix                                       |");
                Console.WriteLine("| 3 - Cartao                                    |");
                Console.WriteLine("=================================================");

                var op = Console.ReadLine().ToLower();

                Console.WriteLine("Escolha o Endereco de entrega: ");

                var enderecos = enderecoService.Buscar(clienteId);

                if (enderecos.Count == 0)
                    throw new Exception("Não há enderecos cadastrados. Cadastre um endereço para poder finalizar a compra");

                enderecos.ForEach(a =>
                {
                    Console.WriteLine("------------------------------------------------------------------------------------------------------------");
                    Console.WriteLine($"|| Endereco º: {a.EnderecoId}  ||   Rua: {a.Rua}    Numero: {a.Numero}     Complemento: {a.Complemento} ||");
                    Console.WriteLine("------------------------------------------------------------------------------------------------------------");
                });

                var verificarEndereco = new VerificarEnderecoModel();
                verificarEndereco.ClienteId = clienteId;

                Console.WriteLine("Digite o numero do endereco: ");

                if (int.TryParse(Console.ReadLine(), out int enderecoId))
                {
                    verificarEndereco.EnderecoId = enderecoId;
                }
                else
                {
                    Console.WriteLine("Digito invalido, tente novamente");
                    Thread.Sleep(1500);
                }

                if (!enderecoService.Verificar(verificarEndereco))
                {
                    Console.WriteLine("Endereco invalido tente novamente");
                    Thread.Sleep(1500);
                    Console.Clear();
                    Finalizar(clienteId);
                }

                var iniciarModel = new IniciarModel
                {
                    ClienteId = clienteId,
                    EnderecoId = verificarEndereco.EnderecoId,
                };

                switch (op)
                {
                    case "1":
                        iniciarModel.FormaPagamentoEnum = FormaPagamentoEnum.Boleto;

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
                        iniciarModel.FormaPagamentoEnum = FormaPagamentoEnum.Pix;

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
                        iniciarModel.FormaPagamentoEnum = FormaPagamentoEnum.Cartao;

                        var cartoes = cartaoCreditoService.Buscar(clienteId);

                        cartoes.ForEach(a =>
                        {
                            Console.WriteLine("------------------------------------------------------------------------------------------------------------");
                            Console.WriteLine($"|| Cartão Nº: {a.CartaoCreditoId}  ||  Numero do Cartao: **** **** **** {a.Numero.Substring(a.Numero.Length - 4)} ||");
                            Console.WriteLine("------------------------------------------------------------------------------------------------------------");
                        });

                        var verificarCartao = new VerificarCartaoModel();
                        verificarCartao.ClienteId = clienteId;

                        Console.WriteLine("Selecione o seu cartão");
                        if (!int.TryParse(Console.ReadLine(), out int cartaoId))
                        {
                            Console.WriteLine("Digito invalido, tente novamente");
                            Thread.Sleep(1500);

                        }

                        verificarCartao.CartaoCredtioId = cartaoId;

                        Console.WriteLine("Digite seu codigo de segunça");
                        verificarCartao.CodigoSeguranca = Console.ReadLine();

                        if (!cartaoCreditoService.Verificar(verificarCartao))
                        {
                            Console.WriteLine("Cartao ou Codigo invalide, tente novamente ou escolha outra forma de pagamento");
                            Thread.Sleep(1500);
                            Finalizar(clienteId);
                        }

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
            catch (Exception)
            {

                throw;
            }
            
        }
    }
}
