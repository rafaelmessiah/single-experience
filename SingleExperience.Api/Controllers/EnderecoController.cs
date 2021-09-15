using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SingleExperience.Services.Endereco;
using SingleExperience.Services.Endereco.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SingleExperience.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class EnderecoController : ControllerBase
    {
        protected readonly EnderecoService _enderecoService;

        public EnderecoController(EnderecoService enderecoService)
        {
            _enderecoService = enderecoService;
        }

        [HttpGet("{clienteId}")]
        public async Task<List<EnderecoModel>> Buscar(int clienteId)
        {
            return await _enderecoService.Buscar(clienteId);
        }

        [HttpPost]
        public async Task<EnderecoModel> Cadastrar([FromBody] CadastroEnderecoModel model)
        {
            return await _enderecoService.Cadastrar(model);
        }

        [HttpPost("{enderecoId}")]
        public async Task<bool> Editar(int enderecoId, [FromBody]EnderecoModel model)
        {
            return await _enderecoService.Editar(enderecoId, model);
        }
    }
}
