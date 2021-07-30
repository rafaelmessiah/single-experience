using System;
using SingleExperience.Entities.BD;
using SingleExperience.Entities;
using SingleExperience.Entities.Enums;
using SingleExperience.Services.CarrinhoService;
using System.IO;
using System.Linq;
using System.Globalization;

namespace SingleExperience
{
    class Program
    {
        static void Main(string[] args)
        {
            var carrinhoService = new CarrinhoService();

            carrinhoService.Adicionar(2, 1);
        }
    }
}
