using SingleExperience.Services.CartaoCredito;
using SingleExperience.Services.CartaoCredito.Models;
using SingleExperience.Services.Cliente;
using SingleExperience.Services.Cliente.Models;
using SingleExperience.Services.Endereco;
using SingleExperience.Services.Endereco.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;

namespace SingleExperience.Views
{
    class ClienteView
    {
        ClienteService clienteService = new ClienteService();
        EnderecoService enderecoService = new EnderecoService();
        CartaoCreditoService cartaoCreditoService = new CartaoCreditoService();
        
        public void VizualizarPerfil(ClienteLogadoModel clienteLogado)
        {
            if (clienteLogado.ClienteId == 0)
            {
                clienteLogado = FormularioLogin();
                VizualizarPerfil(clienteLogado);
            }

            var cliente = new ClienteDetalheModel();
            try
            {
                cliente = clienteService.Obter(clienteLogado.ClienteId);

                Console.WriteLine("============================");
                Console.WriteLine($"|| Meu Perfil: {cliente.Nome}  ||");
                Console.WriteLine("============================");

                Console.WriteLine("=================================================");
                Console.WriteLine("=== Opções ======================================");
                Console.WriteLine("| 1 - Editar Dados                              |");
                Console.WriteLine("| 2 - Vizualizar Meus enderecos Cadastrados     |");
                Console.WriteLine("| 3 - Vizualizar Meus Cartões de Credtios       |");
                Console.WriteLine("| 0 - Voltar a Pagina Principal                 |");
                Console.WriteLine(" ===============================================");
                var op = Console.ReadLine().ToLower();

                switch (op)
                {
                    case "1":
                        break;
                    case "2":
                        Console.Clear();
                        VizualizarEnderecos(clienteLogado);
                        break;
                    case "3":
                        Console.Clear();
                        VizualizarCartoes(clienteLogado);
                        break;
                    case "0":
                        break;
                    default:
                        break;
                }

            }
            catch (IOException e)
            {
                Console.WriteLine("Ocorreu um erro");
                Console.WriteLine(e.Message);
            }
           
        }

        public ClienteLogadoModel FormularioLogin()
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

                    Console.WriteLine("Digite sua Senha: ");
                    login.Senha = Console.ReadLine();

                    try
                    {
                        clienteLogado = clienteService.Login(login);

                        if (clienteLogado == null)
                        {
                            Console.WriteLine("Email ou senha incorretos, tente novamente");
                            FormularioLogin();
                        }
                    }
                    catch (IOException e)
                    {
                        Console.WriteLine("Ocorreu um Erro");
                        Console.WriteLine(e);
                    }

                    return clienteLogado;
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
                            FormularioLogin();
                        }
                    }
                    catch (IOException e)
                    {
                        Console.WriteLine("Ocorreu um erro");
                        Console.WriteLine(e);
                    }

                    break;
                case "0":
                    Console.Clear();
                    break;
                default:
                    Console.Clear();
                    FormularioLogin();
                    break;
            }

            return clienteLogado;

        }

        public void VizualizarEnderecos (ClienteLogadoModel clienteLogado)
        {
            
            try
            {
                Console.Clear();
                var enderecos = enderecoService.Buscar(clienteLogado.ClienteId);

                Console.WriteLine("============================");
                Console.WriteLine($"|| Meus Endereços        ||");
                Console.WriteLine("============================");

                enderecos.ForEach(a =>
                {
                    Console.WriteLine("------------------------------------------------------------------------------------------------------------");
                    Console.WriteLine($"|| Endereco Nº: {a.EnderecoId}  ||   Rua: {a.Rua}    Numero: {a.Numero}     Complemento: {a.Complemento} ||");
                    Console.WriteLine("------------------------------------------------------------------------------------------------------------");
                });

                Console.WriteLine();
                Console.WriteLine("=================================================");
                Console.WriteLine("=== Opções ======================================");
                Console.WriteLine("| 1 - Cadastrar Novo Endereço                   |");
                Console.WriteLine("| 0 - Voltar a Pagina Principal                 |");
                Console.WriteLine(" ===============================================");
                var op = Console.ReadLine().ToLower();

                switch (op)
                {
                    case "1":
                        var endereco = new CadastroEnderecoModel();
                        endereco.ClienteId = clienteLogado.ClienteId;

                        Console.Write("Digite a Rua: ");
                        endereco.Rua = Console.ReadLine();

                        Console.Write("Digite o Numero: ");
                        endereco.Numero = Console.ReadLine();

                        Console.Write("Digite o Complemento: ");
                        endereco.Complemento = Console.ReadLine();

                        Console.Write("Digite o CEP: ");
                        endereco.Cep = Console.ReadLine();

                        if (enderecoService.Cadastrar(endereco))
                        {
                            Console.WriteLine("Endereco Cadastrado com Sucesso!");
                            Thread.Sleep(1500);
                        }
                        else
                        {
                            Console.WriteLine("Não foi possível cadastrar esse endereço, Tente Novamente");
                        }

                        VizualizarEnderecos(clienteLogado);

                        break;
                    case "0":
                        Console.Clear();
                        break;
                    default:
                        VizualizarEnderecos(clienteLogado);
                        break;
                }

            }
            catch (IOException e)
            {
                Console.WriteLine("Ocorreu um erro");
                Console.WriteLine(e.Message);
            }
        }

        public void VizualizarCartoes (ClienteLogadoModel clienteLogado)
        {
            try
            {
                Console.Clear();
                var cartoes = cartaoCreditoService.Listar(clienteLogado.ClienteId);

                Console.WriteLine("============================");
                Console.WriteLine($"|| Meus Cartões          ||");
                Console.WriteLine("============================");

                cartoes.ForEach(a =>
                {
                    Console.WriteLine("------------------------------------------------------------------------------------------------------------");
                    Console.WriteLine($"|| Cartão Nº: {a.CartaoCreditoId}  ||  Numero do Cartao: **** **** **** {a.Final} ||");
                    Console.WriteLine("------------------------------------------------------------------------------------------------------------");
                });

                Console.WriteLine();
                Console.WriteLine("=================================================");
                Console.WriteLine("=== Opções ======================================");
                Console.WriteLine("| 1 - Cadastrar Novo Cartão                     |");
                Console.WriteLine("| 0 - Voltar a Pagina Principal                 |");
                Console.WriteLine(" ===============================================");
                var op = Console.ReadLine().ToLower();

                switch (op)
                {
                    case "1":
                        var cartao = new CadastroCartaoModel();
                        cartao.ClienteId = clienteLogado.ClienteId;

                        Console.Write("Digite o Numero do Cartão: ");
                        cartao.Numero = Console.ReadLine();

                        Console.Write("Digite a Bandeira: ");
                        cartao.Bandeira = Console.ReadLine();

                        Console.Write("Digite o Codigo de Segurança: ");
                        cartao.CodigoSeguranca = Console.ReadLine();

                        Console.Write("Digite a Data de Vencimento (dd/mmm/aaaa): ");
                        cartao.DataVencimento = DateTime.Parse(Console.ReadLine());

                        if (cartaoCreditoService.Cadastrar(cartao))
                        {
                            Console.WriteLine("Cartão Cadastrado com Sucesso!");
                            Thread.Sleep(1500);
                        }
                        else
                        {
                            Console.WriteLine("Não foi possível cadastrar esse cartão, Tente Novamente");
                        }
                        
                        VizualizarCartoes(clienteLogado);

                        break;
                    case "0":
                        Console.Clear();
                        break;
                    default:
                        VizualizarCartoes(clienteLogado);
                        break;
                }

            }
            catch (IOException e)
            {
                Console.WriteLine("Ocorreu um erro");
                Console.WriteLine(e.Message);
            }
        }
    }
}
