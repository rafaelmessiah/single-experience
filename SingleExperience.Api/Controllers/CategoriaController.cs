using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SingleExperience.Services;
using SingleExperience.Services.Categoria;
using SingleExperience.Repositorio.Services.Categoria.Models;

namespace SingleExperience.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        protected readonly CategoriaService _categoriaService;

        public CategoriaController(CategoriaService categoriaService)
        {
            _categoriaService = categoriaService;
        }

        [HttpGet]
        public async Task<List<CategoriaModel>> Buscar()
        {
            return await _categoriaService.Buscar();
        }
    }
}
