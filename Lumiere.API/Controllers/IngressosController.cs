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
        public IActionResult Get()
        {
            var result = _service.GetAll();
            return HandleResult(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var result = _service.GetById(id);
            return HandleResult(result);
        }

        [HttpGet("sessao/{sessaoId}")]
        public IActionResult GetBySessao(int sessaoId)
        {
            var result = _service.GetBySessao(sessaoId);
            return HandleResult(result);
        }

        [HttpPost]
        public IActionResult VenderIngresso([FromBody] CreateIngressoDto ingressoDto)
        {
            var result = _service.Vender(ingressoDto);
            return HandleResult(result);
        }
    }
}