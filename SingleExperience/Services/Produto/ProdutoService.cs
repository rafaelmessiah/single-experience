using System;
using System.Collections.Generic;
using System.Text;
using SingleExperience.Entities;
using System.IO;

namespace SingleExperience.Services
{
    class ProdutoService
    {
        

        public List<ProdutoEntity> ListProducts()
        {

            string path = @"C:\Users\rafael.messias\source\repos\SingleExperience\Tabelas";
            try
            {
                string[] products = File.ReadAllLines(path);

                using (StreamReader sr = File.OpenText(path))
                {
                    foreach (var item in products)
                    {
                        string[] fields = item.Split(',');

                        var produto = 

                        produtoId = int.Parse(fields[0]);
                         = fields[1];
                        price = double.Parse(fields[2]);
                        detail = fields[3];
                        statusId = int.Parse(fields[4]);
                        amount = int.Parse(fields[5]);
                        categoryId = int.Parse(fields[6]);
                        ranking = int.Parse(fields[7]);
                        available = bool.Parse(fields[8]);
                        rating = float.Parse(fields[9]);


                        prod.Add(new ProductEntitie(produtoId, name, price, detail, statusId, amount, categoryId, ranking, available, rating));
                    }
                }
            }
            catch (IOException e)
            {
                Console.WriteLine("Ocorreu um erro");
                Console.WriteLine(e.Message);
            }
            return prod;
        }




    }
}
