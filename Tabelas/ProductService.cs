using System;
using SingleExperience.Entities;
using System.IO;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace SingleExperience.Services.ProductServices
{
    class ProductService
    {
		public List<ProductEntitie> ListProducts()
        {
            int produtoId = 0, statusId = 0, amount = 0, categoryId = 0, ranking = 0;
            string name = null, detail = null;
            double price = 0.0;
            bool available = false;
            float rating = 0.0f;

            string path = @"C:\Users\mariane.santos\Documents\Products.csv";
            List<ProductEntitie> prod = new List<ProductEntitie>(); 
			try 
			{
                string[] products = File.ReadAllLines(path);

                using (StreamReader sr = File.OpenText(path))
                {
                    foreach (var item in products)
                    {
                        string[] fields = item.Split(',');

                        produtoId = int.Parse(fields[0]);
                        name = fields[1];
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
