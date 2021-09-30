using Microsoft.AspNetCore.Mvc;
using SingleExperience.Repositorio.Services.Carrinho.Models;
using SingleExperience.Services.Carrinho;
using SingleExperience.Services.Carrinho.Models;
using SingleExperience.Services.Compra.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SingleExperience.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CarrinhoController : ControllerBase
    {
        protected readonly CarrinhoService _carrinhoService;

        public CarrinhoController(CarrinhoService carrinhoService)
        {
            _carrinhoService = carrinhoService;
        }

        [HttpGet("{clienteId}")]
        public async Task<List<ItemCarrinhoModel>> Buscar(int clienteId)
        {
            return await _carrinhoService.BuscarItens(clienteId);
        }

        [HttpPost]
        public async Task<ItemCarrinhoModel> Adicionar([FromBody] SalvarModel model)
        {
            return await _carrinhoService.Adicionar(model);
        }

        [HttpPut("{carrinhoId}/alterar/status")]
        public async Task<bool> AlterarStatus(int carrinhoId, [FromBody] EdicaoStatusModel model)
        {
            return await _carrinhoService.AlterarStatus(carrinhoId, model);
        }

        [HttpPut("{carrinhoId}/alterar/qtde")]
        public async Task<bool> AlterarQtde(int carrinhoId, [FromBody] EdicaoQtdeModel model)
        {
            return await _carrinhoService.AlterarQtde(carrinhoId, model);
        }

        [HttpGet("verificar")]
        public async Task<bool> VerificarItem([FromBody] VerificarItemModel model)
        {
            return await _carrinhoService.VerificarItem(model);
        }

        [HttpGet("{clienteId}/calcular")]
        public async Task<decimal> CalcularValorTotal(int clienteId)
        {
            return await _carrinhoService.CalcularValorTotal(clienteId);
        }
    }
}
