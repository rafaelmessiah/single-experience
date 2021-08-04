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

        string path = @"C:\Workspaces\visual_studio_2019\single-experience\Tabelas\Compra.csv";
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
                        Enum.TryParse(campos[4], out StatusPagamentoEnum statusPagamentoEnum);
                        compra.StatusPagamentoId = statusPagamentoEnum;
                        compra.DataCompra = DateTime.Parse(campos[5]);
                        DateTime.TryParse(campos[6], out DateTime dateTime);
                        compra.DataPagamento = dateTime;
                        compra.ValorFinal = double.Parse(campos[7], CultureInfo.InvariantCulture);

                        listaCompra.Add(compra);
                    });
            }
            catch (Exception)
            {

                throw;
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
                    var statusPagmentoId = ((int)StatusPagamentoEnum.NaoConfirmado);

                    var aux = new string[]
                    {
                    compraId.ToString(),
                    statusCompraId.ToString(),
                    formaPagamentoId.ToString(),
                    model.ClienteId.ToString(),
                    statusPagmentoId.ToString(),
                    DateTime.Now.ToString(),
                    null,
                    model.ValorFinal.ToString("F2", CultureInfo.InvariantCulture)
                    };

                    streamWriter.WriteLine(String.Join(",", aux));
                }

                return compraId;
            }
            catch (Exception)
            {
                throw new Exception("Não foi possivel iniciar essa compra");
            }

            
            
        }

        public bool Pagar(int compraId)
        {
            try
            {
                var compras = BuscarCompras();

                var index = compras
                    .FindIndex(a => a.CompraId == compraId);

                compras[index].StatusPagamentoId = StatusPagamentoEnum.Confirmado;
                compras[index].DataPagamento = DateTime.Now;

                var linhas = new List<string>();

                linhas.Add(header);

                foreach (var item in compras)
                {
                    var statusCompraId = ((int)item.StatusCompraId);
                    var formaPagamentoId = ((int)item.FormaPagamentoId);
                    var statusPagamentoId = ((int)item.StatusPagamentoId);

                    var aux = new string[]
                    {
                        item.CompraId.ToString(),
                        statusCompraId.ToString(),
                        formaPagamentoId.ToString(),
                        item.ClienteId.ToString(),
                        statusPagamentoId.ToString(),
                        item.DataCompra.ToString(),
                        item.DataPagamento.ToString(),
                        item.ValorFinal.ToString("F2", CultureInfo.InvariantCulture),
                    };

                    linhas.Add(String.Join(",", aux));
                }

                File.WriteAllLines(path, linhas);

            }
            catch (IOException e)
            {
                Console.WriteLine("Ocurred an error");
                Console.WriteLine(e.Message);
            }

            return true;
        }

    }
}
