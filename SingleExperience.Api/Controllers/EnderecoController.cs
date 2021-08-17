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
        public async Task<bool> Cadastrar([FromBody]CadastroEnderecoModel model)
        {
            return await _enderecoService.Cadastrar(model);
        }

        [HttpPut]
        public async Task<bool> Editar([FromBody]EnderecoModel model)
        {
            return await _enderecoService.Editar(model);
        }

        [HttpGet]
        public async Task<bool> Verificar([FromBody] VerificarEnderecoModel model)
        {
            return await _enderecoService.Verificar(model);
        }
    }
}
