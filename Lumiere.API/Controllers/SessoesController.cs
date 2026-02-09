using Lumiere.API.Dtos.Sessao;
using Lumiere.API.Services.Interfaces;
using Lumiere.API.Mappers;
using Microsoft.AspNetCore.Mvc;
using Lumiere.Models;

namespace Lumiere.API.Controllers
{
    [Route("api/sessoes")]
    [ApiController]
    public class SessoesController : ControllerBase
    {
        private readonly ISessaoService _service;
        public SessoesController(ISessaoService service)
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

        [HttpPost]
        public IActionResult Add([FromBody] CreateSessaoDto sessaoDto)
        {
            var result = _service.Create(sessaoDto);
            if (!result.Ok) return BadRequest(new { result.Error });
            return CreatedAtAction(nameof(GetById), new { id = result.Data!.Id }, result.Data);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] UpdateSessaoDto sessaoDto)
        {
            var result = _service.Update(id, sessaoDto);
            if (!result.Ok) return BadRequest(result.Error);
            return Ok(result.Data);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            var result = _service.Delete(id);
            if (!result.Ok) return BadRequest(result.Error);
            return NoContent();
        }
    }
}
