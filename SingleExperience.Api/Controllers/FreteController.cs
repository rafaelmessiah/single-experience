using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SingleExperience.Repositorio.Services.Frete.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SingleExperience.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class FreteController : ControllerBase
    {
        protected readonly FreteService _freteService;

        public FreteController(FreteService freteService)
        {
            _freteService = freteService;
        }

        [HttpPost]
        public async Task<CalcularFreteModel> Calcular()
        {
            return await _freteService.Calcular();
        }
    }
}
