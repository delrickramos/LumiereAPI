using Lumiere.API.Dtos.Ingresso;
using Lumiere.API.Interfaces;
using Lumiere.API.Mappers;
using Microsoft.AspNetCore.Mvc;
using Lumiere.Models;

namespace Lumiere.API.Controllers
{
    [Route("api/ingressos")]
    [ApiController]
    public class IngressosController : ControllerBase
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
            if (!result.Ok) return BadRequest(result.Error);
            return Ok(result.Data);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var result = _service.GetById(id);
            if (!result.Ok) return NotFound(result.Error);
            return Ok(result.Data);
        }

        [HttpGet("sessao/{sessaoId}")]
        public IActionResult GetBySessao(int sessaoId)
        {
            var result = _service.GetBySessao(sessaoId);
            if (!result.Ok) return BadRequest(result.Error);
            return Ok(result.Data);
        }

        [HttpPost]
        public IActionResult VenderIngresso([FromBody] CreateIngressoDto ingressoDto)
        {
            var result = _service.Vender(ingressoDto);
            if (!result.Ok) return BadRequest(new { result.Error });
            return CreatedAtAction(nameof(GetById), new { id = result.Data!.Id }, result.Data);
        }
    }
}