using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SingleExperience.Repositorio;
using SingleExperience.Services.Carrinho.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SingleExperience.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarrinhoController : ControllerBase
    {
        protected readonly SingleExperienceRepository _repo;

        public CarrinhoController(SingleExperienceRepository repo)
        {
            _repo = repo;
        }

        [HttpGet("{id}")]
        public async Task<List<ItemCarrinhoModel>> Buscar(int clienteId)
        {

        }

        [HttpPost]  
        public async bool Adicionar(SalvarModel model)
        {

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
