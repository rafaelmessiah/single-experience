
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

     }
}