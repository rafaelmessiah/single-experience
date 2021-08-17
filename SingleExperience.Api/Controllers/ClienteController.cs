﻿using Microsoft.AspNetCore.Http;
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

        [HttpGet("login")]
        public async Task<ClienteLogadoModel> Login([FromBody] LoginModel loginModel)
        {
            return await _clienteService.Login(loginModel);
        }

        [HttpPost]
        public async Task<bool> Cadastrar([FromBody] CadastroClienteModel model)
        {
            return await _clienteService.Cadastrar(model);
        }

        [HttpGet("{clienteId}")]
        public async Task<ClienteDetalheModel> Obter(int clienteId)
        {
            return await _clienteService.Obter(clienteId);
        }

        [HttpPut("editar/email")]
        public async Task<bool> EditarEmail([FromBody] EdicaoEmailModel model)
        {
            return await _clienteService.EditarEmail(model);
        }
        
        [HttpPut("editar/senha")]
        public async Task<bool> EditarSenha([FromBody] EdicaoSenhaModel model)
        {
            return await _clienteService.EditarSenha(model);
        }

        [HttpGet("verificar")]
        public async Task<bool> Verificar([FromBody] VerificarClienteModel model)
        {
            return await _clienteService.Verificar(model);
        }

    }
}