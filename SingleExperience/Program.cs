using System;
using SingleExperience.Entities.BD;
using SingleExperience.Entities.Enums;
using System.Linq;
using SingleExperience.Services.ProdutoService;
using SingleExperience.Services.ProdutoService.Models;
using System.Globalization;

namespace SingleExperience
{
    class Program
    {
        static void Main(string[] args)
        {
            var produtoService = new ProdutoService();

            var produto = produtoService.BuscarCategoria(CategoriaEnum.Notebook);

            produto.ForEach(a =>
            {
                Console.WriteLine(a.Nome+" " +a.Preco+" ");
            });
        }
    }
}
