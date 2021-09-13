using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SingleExperience.Repositorio.Services.FormaPagamento;
using SingleExperience.Repositorio.Services.FormaPagamento.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SingleExperience.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class FormaPagamentoController : ControllerBase
    {
        protected readonly FormaPagamentoService _formaPagamentoService;

        public FormaPagamentoController(FormaPagamentoService formaPagamentoService)
        {
            _formaPagamentoService = formaPagamentoService;
        }

        [HttpGet]
        public async Task<List<FormaPagamentoModel>> Buscar()
        {
            return await _formaPagamentoService.Buscar();
        }
    }
}
