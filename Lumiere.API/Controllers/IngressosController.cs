using Lumiere.API.Dtos.Ingresso;
using Lumiere.API.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Lumiere.API.Controllers
{
    [Route("api/ingressos")]
    public class IngressosController : ServiceResultController
    {
        private readonly IIngressoService _service;

        public IngressosController(IIngressoService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _service.GetAllAsync();
            return HandleResult(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _service.GetByIdAsync(id);
            return HandleResult(result);
        }

        [HttpGet("sessao/{sessaoId}")]
        public async Task<IActionResult> GetBySessao(int sessaoId)
        {
            var result = await _service.GetBySessaoAsync(sessaoId);
            return HandleResult(result);
        }

        [HttpPost]
        public async Task<IActionResult> VenderIngresso([FromBody] CreateIngressoDto ingressoDto)
        {
            var result = await _service.VenderAsync(ingressoDto);
            return HandleResult(result);
        }
    }
}