using Microsoft.AspNetCore.Mvc;
using SingleExperience.Enums;
using SingleExperience.Services.Produto;
using SingleExperience.Services.Produto.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SingleExperience.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProdutoController : ControllerBase
    {
        protected readonly ProdutoService _produtoService;

        public ProdutoController(ProdutoService produtoService)
        {
            _produtoService = produtoService;
        }

        [HttpGet]
        public async Task<List<ProdutoSimplesModel>> Buscar()
        {
            return await _produtoService.Buscar();
        }

        [HttpGet("{categoriaEnum}")]
        public async Task<List<ProdutoSimplesModel>> BuscarCategoria(CategoriaEnum categoriaEnum)
        {
            return await _produtoService.BuscarCategoria(categoriaEnum);
        }

        [HttpGet("disponibilidade/{produtoId}")]
        public async Task<DisponivelModel> ObterDisponibildade(int produtoId)
        {
            return await _produtoService.ObterDisponibildade(produtoId);
        }

        [HttpGet("verificar/{produtoId}")]
        public async Task<bool> Verificar(int produtoId)
        {
            return await _produtoService.Verificar(produtoId);
        }

        [HttpGet("detalhe/{produtoId}")]
        public async Task<ProdutoDetalhadoModel> ObterDetalhe(int produtoId)
        {
            return await _produtoService.ObterDetalhe(produtoId);
        }

        [HttpPut]
        public async Task<bool> Retirar([FromBody] AlterarQtdeModel model)
        {
            return await _produtoService.Retirar(model);
        }
    }
}
