using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.IO;
using SingleExperience.Entities;
using SingleExperience.Entities.Enums;
using SingleExperience.Services.Compra.Models;
using System.Globalization;

namespace SingleExperience.Entities.BD
{
    public class CompraBD
    {

        string path = @"C:\Users\rafael.messias\source\repos\SingleExperience\Tabelas\Compra.csv";
        string header = "";
        

        public List<CompraEntity> BuscarCompras()
        {
            List<CompraEntity> listaCompra = new List<CompraEntity>();

            try
            {
                var compras = File.ReadAllLines(path, Encoding.UTF8);

                header = compras[0];

                compras.Skip(1)
                    .ToList()
                    .ForEach(c =>
                    {
                        var campos = c.Split(",");

                        var compra = new CompraEntity();

                        compra.CompraId = int.Parse(campos[0]);
                        Enum.TryParse(campos[1], out StatusCompraEnum statusCompraEnum);
                        compra.StatusCompraId = statusCompraEnum;
                        Enum.TryParse(campos[2], out FormaPagamentoEnum formaPagamentoEnum);
                        compra.FormaPagamentoId = formaPagamentoEnum;
                        compra.ClienteId = int.Parse(campos[3]);
                        compra.EnderecoId = int.Parse(campos[4]);
                        compra.StatusPagamento = bool.Parse(campos[5]);
                        compra.DataCompra = DateTime.Parse(campos[6]);
                        DateTime.TryParse(campos[7], out DateTime dateTime);
                        compra.DataPagamento = dateTime;
                        compra.ValorFinal = double.Parse(campos[8], CultureInfo.InvariantCulture);

                        listaCompra.Add(compra);
                    });
               
                
            }
            catch (IOException e)
            {
                Console.WriteLine("Ocorreu um Erro");
                Console.WriteLine(e);
            }
            return listaCompra;
        }
        
        public int Salvar(CadastroModel model)
        {
            try
            {
                var compraId = BuscarCompras().Count + 1;

                using (var streamWriter = File.AppendText(path))
                {
                    var statusCompraId = ((int)StatusCompraEnum.Aberta);
                    var formaPagamentoId = ((int)model.FormaPagamentoId);
                    var statusPagmento = false;

                    var aux = new string[]
                    {
                    compraId.ToString(),
                    statusCompraId.ToString(),
                    formaPagamentoId.ToString(),
                    model.ClienteId.ToString(),
                    model.EnderecoId.ToString(),
                    statusPagmento.ToString(),
                    DateTime.Now.ToString(),
                    null,
                    model.ValorFinal.ToString("F2", CultureInfo.InvariantCulture)
                    };

                    streamWriter.WriteLine(String.Join(",", aux));
                }

                return compraId;
            }
            catch (IOException e)
            {
                Console.WriteLine("Ocorreu um Erro:");
                Console.WriteLine(e.Message);
            }

            return 0;
        }

        public bool Pagar(int compraId)
        {
            try
            {
                var compras = BuscarCompras();

                var index = compras
                    .FindIndex(a => a.CompraId == compraId);

                compras[index].StatusPagamento = true;
                compras[index].DataPagamento = DateTime.Now;

                var linhas = new List<string>();

                linhas.Add(header);

                foreach (var item in compras)
                {
                    var statusCompraId = ((int)item.StatusCompraId);
                    var formaPagamentoId = ((int)item.FormaPagamentoId);

                    var aux = new string[]
                    {
                        item.CompraId.ToString(),
                        statusCompraId.ToString(),
                        formaPagamentoId.ToString(),
                        item.ClienteId.ToString(),
                        item.StatusPagamento.ToString(),
                        item.DataCompra.ToString(),
                        item.DataPagamento.ToString(),
                        item.ValorFinal.ToString("F2", CultureInfo.InvariantCulture),
                    };

                    linhas.Add(String.Join(",", aux));
                }

                File.WriteAllLines(path, linhas);

                return true;
            }
            catch (IOException e)
            {
                Console.WriteLine("Ocorreu um Erro");
                Console.WriteLine(e);
            }

            return true;
        }

    }
}
