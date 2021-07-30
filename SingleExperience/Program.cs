using System;
using SingleExperience.Entities.BD;
using SingleExperience.Entities;
using SingleExperience.Entities.Enums;
using SingleExperience.Services.CarrinhoService;
using System.IO;
using System.Linq;
using System.Globalization;
using SingleExperience.Services.Carrinho.Models;
using SingleExperience.Services.CompraService;
using SingleExperience.Services.ListaProdutoCompra;

namespace SingleExperience
{
    class Program
    {
        static void Main(string[] args)
        {
            var listaProdutoCompraService = new ListaProdutoCompraService();

            var itens = listaProdutoCompraService.BuscarProdutos(1);

            itens.ForEach(a =>
            {
                Console.WriteLine(a.Nome + " Preco: " + a.PrecoUnitario + " Quantidade: " + a.Qtde);
            });
          
        }
    }
}
