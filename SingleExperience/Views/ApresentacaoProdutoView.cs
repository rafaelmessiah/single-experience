using System;
using System.Collections.Generic;
using System.Text;

namespace SingleExperience.Views
{
    public class ApresentacaoProdutoView
    {
        Header header = new Header();
        Footer footer = new Footer();

        public void Menu()
        {
            header.Exibir();

            Console.WriteLine("|  Categorias:                                           |");
            Console.WriteLine("|  1 - Computadores                                      |");
            Console.WriteLine("|  2 - Notebooks                                         |");
            Console.WriteLine("|  3 - Acessorios                                        |");
            Console.WriteLine("|  4 - Celulares                                         |");
            Console.WriteLine("|  5 - Tablets                                           |");

            Console.WriteLine("|  8 - Vizualizar Meu Carrinho                           |");
            Console.WriteLine("|  9 - Vizualizar Meus Pedidos                           |");
            Console.WriteLine("|  0 - Sair                                              |");
            Console.WriteLine(" ========================================================");
        }


    }
}
