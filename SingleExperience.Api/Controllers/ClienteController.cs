using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SingleExperience.Services.Cliente;
using SingleExperience.Services.Cliente.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SingleExperience.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        protected readonly ClienteService _clienteService;

        public ClienteController(ClienteService clienteService)
        {
            _clienteService = clienteService;
        }

        [HttpPost("login")]
        public async Task<ClienteLogadoModel> Login([FromBody] LoginModel loginModel)
        {
            return await _clienteService.Login(loginModel);
        }

        [HttpPost]
        public async Task<ClienteLogadoModel> Cadastrar([FromBody] CadastroClienteModel model)
        {
            return await _clienteService.Cadastrar(model);
        }

        [HttpGet("{clienteId}")]
        public async Task<ClienteDetalheModel> Obter(int clienteId)
        {
            return await _clienteService.Obter(clienteId);
        }

        [HttpPut("{clienteId}/editar/email")]
        public async Task<bool> EditarEmail(int clienteId, [FromBody] EdicaoEmailModel model)
        {
            return await _clienteService.EditarEmail(clienteId, model);
        }
        
        [HttpPut("{clienteId}/editar/senha")]
        public async Task<bool> EditarSenha(int clienteId, [FromBody] EdicaoSenhaModel model)
        {
            return await _clienteService.EditarSenha(clienteId, model);
        }
    }
}
