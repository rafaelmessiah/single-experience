using Microsoft.AspNetCore.Mvc;
using SingleExperience.Services.Carrinho;
using SingleExperience.Services.Carrinho.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SingleExperience.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarrinhoController : ControllerBase
    {
        protected readonly CarrinhoService carrinhoService;

        public CarrinhoController(CarrinhoService carrinhoService)
        {
            carrinhoService = carrinhoService;
        }

        [HttpGet("{clienteId}")]
        public async Task<List<ItemCarrinhoModel>> Buscar([FromBody] int clienteId)
        {
            return await carrinhoService.Buscar(clienteId);
        }

        [HttpPost]  
        public async bool Adicionar(SalvarModel model)
        {
            return await carrinhoService.Adicionar(model);
        }

        [HttpPut]
        public async bool AlterarStatus(EdicaoStatusModel model)
        {

        }

        [HttpPut]
        public async bool AlterarQtde(EdicaoQtdeModel model)
        {

        }

        [HttpGet]
        public async decimal CalcularValorTotal(int clienteId)
        {

        }
    }
}
