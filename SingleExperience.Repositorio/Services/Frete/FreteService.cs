using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Refit;
using SingleExperience.Services.Endereco;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SingleExperience.Repositorio.Services.Frete.Models
{
    public class FreteService
    {
        protected readonly SingleExperience.Context.Context _context;
        protected readonly EnderecoService _enderecoService;

        public FreteService(SingleExperience.Context.Context context)
        {
            _context = context;
            _enderecoService = new EnderecoService(context);
        }

        public async Task<RespostaViaCepModel> ObterCep(string cep)
        {
            using(var hc = new HttpClient())
            {
                hc.DefaultRequestHeaders.Accept.Clear();
                hc.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("apllication/json"));

                var json = "";

                var resposta = await hc.GetAsync($"https://viacep.com.br/ws/{cep}/json/");
                if (resposta.IsSuccessStatusCode)
                {
                    json = await resposta.Content.ReadAsStringAsync();
                }

                return JsonConvert.DeserializeObject<RespostaViaCepModel>(json);
            }
        }

        public async Task<bool> VerificarCep(string cep)
        {
            using (var hc = new HttpClient())
            {
                hc.DefaultRequestHeaders.Accept.Clear();
                hc.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("apllication/json"));

                var json = "";

                var resposta = await hc.GetAsync($"https://viacep.com.br/ws/{cep}/json/");
                if (resposta.IsSuccessStatusCode)
                {
                    json = await resposta.Content.ReadAsStringAsync();
                }

                if (JsonConvert.DeserializeObject<RespostaViaCepModel>(json) == null)
                    return false;

                return true;
            }
        }

        public async Task<FreteSimplesModel> ObterFrete(string cepDestino)
        {
            using(var hc = new HttpClient())
            {
                hc.DefaultRequestHeaders.Accept.Clear();
                hc.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));

                var uriBuilder = new UriBuilder("http://ws.correios.com.br/calculador/CalcPrecoPrazo.aspx");
                uriBuilder.Query = "nCdEmpresa=08082650&" +
                    "sDsSenha=564321&" +
                    "sCepOrigem=80620010&" +
                    "sCepDestino=" + cepDestino +"&" +
                    "nVlPeso=1&" +
                    "nCdFormato=1&" +
                    "nVlComprimento=20&" +
                    "nVlAltura=20&" +
                    "nVlLargura=20&" +
                    "sCdMaoPropria=n&" +
                    "nVlValorDeclarado=0&" +
                    "sCdAvisoRecebimento=n&" +
                    "nCdServico=04510&" +
                    "nVlDiametro=0&" +
                    "StrRetorno=xml&" +
                    "nIndicaCalculo=3";

                var xml = "";
                var resposta = await hc.GetAsync(uriBuilder.Uri);
                if (resposta.IsSuccessStatusCode)
                {
                    xml = await resposta.Content.ReadAsStringAsync();
                }

                var xmlSerializer = new XmlSerializer(typeof(ServicoCorreiosModel));
                var servicoModel = new ServicoCorreiosModel();

                using (var sr = new StringReader(xml))
                {
                    servicoModel = (ServicoCorreiosModel)xmlSerializer.Deserialize(sr);
                }

                var valorString = servicoModel.RespostaFreteCorreioModel.Valor.Replace(",", ".");

                decimal valorDecimal;
                if (!Decimal.TryParse(valorString, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out valorDecimal))
                    throw new Exception("Não foi possivel converter o Valor do frete para decimal");

                return new FreteSimplesModel
                {
                    Valor = valorDecimal,
                    PrazoEntrega = servicoModel.RespostaFreteCorreioModel.PrazoEntrega
                };
            }
        }

        public async Task<decimal> CalcularValorFrete(int enderecoId)
        {
            using (var hc = new HttpClient())
            {
                var cepDestino = await _context.Endereco
                                        .Where(a => a.EnderecoId == enderecoId)
                                        .Select(b => b.Cep)
                                        .FirstOrDefaultAsync();
                
                if (cepDestino == null)
                    throw new Exception("Cep inválido");

                hc.DefaultRequestHeaders.Accept.Clear();
                hc.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));

        
                var uriBuilder = new UriBuilder("http://ws.correios.com.br/calculador/CalcPrecoPrazo.aspx");
                uriBuilder.Query = "nCdEmpresa=08082650&" +
                    "sDsSenha=564321&" +
                    "sCepOrigem=80620010&" +
                    "sCepDestino=" + cepDestino + "&" +
                    "nVlPeso=1&" +
                    "nCdFormato=1&" +
                    "nVlComprimento=20&" +
                    "nVlAltura=20&" +
                    "nVlLargura=20&" +
                    "sCdMaoPropria=n&" +
                    "nVlValorDeclarado=0&" +
                    "sCdAvisoRecebimento=n&" +
                    "nCdServico=04510&" +
                    "nVlDiametro=0&" +
                    "StrRetorno=xml&" +
                    "nIndicaCalculo=3";

                var xml = "";
                var resposta = await hc.GetAsync(uriBuilder.Uri);
                if (resposta.IsSuccessStatusCode)
                {
                    xml = await resposta.Content.ReadAsStringAsync();
                }

                var xmlSerializer = new XmlSerializer(typeof(ServicoCorreiosModel));
                var servicoModel = new ServicoCorreiosModel();

                using (var sr = new StringReader(xml))
                {
                    servicoModel = (ServicoCorreiosModel)xmlSerializer.Deserialize(sr);
                }

                var valorString = servicoModel.RespostaFreteCorreioModel.Valor.Replace(",", ".");

                decimal valorDecimal;
                if (!Decimal.TryParse(valorString, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out valorDecimal))
                    throw new Exception("Não foi possivel converter o Valor do frete para decimal");

                return valorDecimal;
            }
        }
    }
}
