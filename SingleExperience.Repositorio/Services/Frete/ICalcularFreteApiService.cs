using Refit;
using SingleExperience.Repositorio.Services.Frete.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SingleExperience.Repositorio.Services.Frete
{
    interface ICalcularFreteApiService
    {
        [Post("/calculador/CalcPrecoPrazo.aspx?nCdEmpresa=08082650&sDsSenha=564321&sCepOrigem=70002900&sCepDestino=04547000&nVlPeso=1&nCdFormato=1&nVlComprimento=20&nVlAltura=20&nVlLargura=20&sCdMaoPropria=n&nVlValorDeclarado=0&sCdAvisoRecebimento=n&nCdServico=04510&nVlDiametro=0&StrRetorno=xml&nIndicaCalculo=3")]
        Task<CalcularFreteModel> CalcularFrete();
       
    }
}
