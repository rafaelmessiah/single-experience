using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SingleExperience.Services.ListaProdutoCompra;
using SingleExperience.Services.ListaProdutoCompra.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SingleExperience.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ListaProdutoCompraController : ControllerBase
    {
        protected readonly ListaProdutoCompraService _listaProdutoCompraService;

        public ListaProdutoCompraController(ListaProdutoCompraService listaProdutoCompraService)
        {
            _listaProdutoCompraService = listaProdutoCompraService;
        }

        [HttpGet("{compraId}")]
        public async Task<List<ItemProdutoCompraModel>> Buscar(int compraId)
        {
            return await _listaProdutoCompraService.Buscar(compraId);
        }
    }
}
