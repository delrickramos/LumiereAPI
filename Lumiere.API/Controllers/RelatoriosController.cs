using Lumiere.API.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Lumiere.API.Controllers
{
    // Controller para relatórios e consultas especiais
    [Route("api/relatorios")]
    [ApiController]
    public class RelatoriosController : ControllerBase
    {
        private readonly IRelatorioRepository _relatorioRepo;

        public RelatoriosController(IRelatorioRepository relatorioRepo)
        {
            _relatorioRepo = relatorioRepo;
        }

        // Requisito: relatório de ocupação das salas
        [HttpGet("salas/ocupacao")]
        public IActionResult GetTaxaOcupacaoSalas()
        {
            var resultado = _relatorioRepo.GetTaxaOcupacaoSalas();
            return Ok(resultado);
        }
    }
}