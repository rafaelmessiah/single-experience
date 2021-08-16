using Microsoft.AspNetCore.Mvc;
using SingleExperience.Services.Carrinho;
using SingleExperience.Services.Carrinho.Models;
using SingleExperience.Services.Compra.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SingleExperience.Api.Controllers
{
    [Route("api/[controller]")]
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
            return await _carrinhoService.Buscar(clienteId);
        }

        [HttpPost]
        public async Task<bool> Adicionar([FromBody] SalvarModel model)
        {
            return await _carrinhoService.Adicionar(model);
        }

        [HttpPut]
        public async Task<bool> AlterarStatus([FromBody] EdicaoStatusModel model)
        {
            return await _carrinhoService.AlterarStatus(model);
        }

        [HttpPut]
        public async Task<bool> AlterarQtde([FromBody] EdicaoQtdeModel model)
        {
            return await _carrinhoService.AlterarQtde(model);
        }

        [HttpGet("calcular/{clienteId}")]
        public async Task<decimal> CalcularValorTotal(int clienteId)
        {
            return await _carrinhoService.CalcularValorTotal(clienteId);
        }

        [HttpGet("buscarqtde/{clienteId}")]
        public async Task<List<ProdutoQtdeModel>> BuscarQtde(int clienteId)
        {
            return await _carrinhoService.BuscarQtde(clienteId);
        }

    }
}
