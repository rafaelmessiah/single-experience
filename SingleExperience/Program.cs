using System;
using SingleExperience.Entities.BD;
using SingleExperience.Entities.Enums;
using System.Linq;
using SingleExperience.Services.Produto;
using SingleExperience.Services.Compra;
using SingleExperience.Services.Produto.Models;
using System.Globalization;
using SingleExperience.Services.Carrinho;
using SingleExperience.Services.Carrinho.Models;
using SingleExperience.Services.Compra.Models;
using SingleExperience.Services.ListaProdutoCompra;
using SingleExperience.Services.ListaProdutoCompra.Models;
using SingleExperience.Views;

namespace SingleExperience
{
    class Program
    {
        static void Main(string[] args)
        {
            var header = new Header();
            var home = new Home();

            home.Menu();

        }
    }
}
