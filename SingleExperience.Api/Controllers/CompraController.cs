using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SingleExperience.Services.Compra;
using SingleExperience.Services.Compra.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SingleExperience.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompraController : ControllerBase
    {
        protected readonly CompraService _compraService;

        public CompraController(CompraService compraService)
        {
            _compraService = compraService;
        }

        [HttpGet("clienteId")]
        public async Task<List<ItemCompraModel>> Buscar(int clienteId)
        {
            return await _compraService.Buscar(clienteId);
        }

        [HttpPost]
        public async Task<bool> Cadastrar([FromBody] IniciarModel model)
        {
            return await _compraService.Cadastrar(model);
        }

        [HttpGet("detalhe/{compraId}")]
        public async Task<CompraDetalhadaModel> Obter(int compraId)
        {
            return await _compraService.Obter(compraId);
        }

        [HttpGet("verificar")]
        public async Task<bool> Verificar([FromBody] VerificarCompraModel model)
        {
            return await _compraService.Verificar(model);
        }

        [HttpPut("{compraId}")]
        public async Task<bool> Pagar(int compraId)
        {
            return await _compraService.Pagar(compraId);
        }
    }
}
