using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SingleExperience.Repositorio.Services.Frete.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SingleExperience.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class FreteController : ControllerBase
    {
        protected readonly FreteService _freteService;

        public FreteController(FreteService freteService)
        {
            _freteService = freteService;
        }

        [HttpGet("{cep}")]
        public async Task<RespostaViaCepModel> ObterCep(string cep)
        {
            return await _freteService.ObterCep(cep);
        }

        [HttpGet("{cep}/verificar")]
        public async Task<bool> VerificarCep(string cep)
        {
            return await _freteService.VerificarCep(cep);
        }

        [HttpGet("{cepDestino}/correios")]
        public async Task<FreteSimplesModel> ObterFrete(string cepDestino)
        {
            return await _freteService.ObterFrete(cepDestino);
        }

        [HttpGet("/correios/{enderecoId}")]
        public async Task<decimal> ObterFrete(int enderecoId)
        {
            return await _freteService.CalcularValorFrete(enderecoId);
        }
    }
}
