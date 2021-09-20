using Refit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SingleExperience.Repositorio.Services.Frete.Models
{
    public class FreteService
    {
        public async Task<CalcularFreteModel> Calcular()
        {
            var service = RestService.For<ICalcularFreteApiService>("http://ws.correios.com.br/");

            return await service.CalcularFrete();
        }
    }
}
