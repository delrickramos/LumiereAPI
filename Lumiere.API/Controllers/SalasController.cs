using Lumiere.API.Dtos.Sala;
using Microsoft.AspNetCore.Mvc;
using Lumiere.API.Interfaces;

namespace Lumiere.API.Controllers
{
    [Route("api/salas")]
    [ApiController]
    public class SalasController : ControllerBase
    {
        private readonly ISalaService _service;
        public SalasController(ISalaService service)
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
        public IActionResult Add([FromBody] CreateSalaDto salaDto)
        {
            var result = _service.Create(salaDto);
            if (!result.Ok) return BadRequest(new { result.Error });
            return CreatedAtAction(nameof(GetById), new { id = result.Data!.Id }, result.Data);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] UpdateSalaDto salaDto)
        {
            var result = _service.Update(id, salaDto);
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
