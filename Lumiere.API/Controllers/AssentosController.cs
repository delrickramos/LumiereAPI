using Lumiere.API.Dtos.Assento;
using Lumiere.API.Interfaces;
using Lumiere.API.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace Lumiere.API.Controllers
{
    [Route("api/assentos")]
    [ApiController]
    public class AssentosController : ControllerBase
    {
        private readonly IAssentoService _service;

        public AssentosController(IAssentoService service)
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

        [HttpGet("sala/{salaId}")]
        public IActionResult GetBySala(int salaId)
        {
            var result = _service.GetBySala(salaId);
            if (!result.Ok) return BadRequest(result.Error);
            return Ok(result.Data);
        }

        [HttpPost]
        public IActionResult Add([FromBody] CreateAssentoDto assentoDto)
        {
            var result = _service.Create(assentoDto);
            if (!result.Ok) return BadRequest(new { result.Error });
            return CreatedAtAction(nameof(GetById), new { id = result.Data!.Id }, result.Data);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] UpdateAssentoDto assentoDto)
        {
            var result = _service.Update(id, assentoDto);
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