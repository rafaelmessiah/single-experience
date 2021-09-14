using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SingleExperience.Services.CartaoCredito;
using SingleExperience.Services.CartaoCredito.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SingleExperience.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CartaoCreditoController : ControllerBase
    {
        protected readonly CartaoCreditoService _cartaoCreditoService;

        public CartaoCreditoController(CartaoCreditoService cartaoCreditoService)
        {
            _cartaoCreditoService = cartaoCreditoService;
        }

        [HttpGet("{clienteId}")]
        public async Task<List<CartaoDetalhadoModel>> Buscar(int clienteId)
        {
            return await _cartaoCreditoService.Buscar(clienteId);
        }

        [HttpPost]
        public async Task<bool> Cadastrar([FromBody] CadastroCartaoModel model)
        {
            return await _cartaoCreditoService.Cadastrar(model);
        }

        [HttpGet("{cartaoCreditoId}/detalhe")]
        public async Task<CartaoDetalhadoModel> Obter(int cartaoCreditoId)
        {
            return await _cartaoCreditoService.Obter(cartaoCreditoId);
        }
    }
}
