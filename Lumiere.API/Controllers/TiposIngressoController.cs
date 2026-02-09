using Lumiere.API.Dtos.TipoIngresso;
using Lumiere.API.Interfaces;
using Lumiere.API.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace Lumiere.API.Controllers
{
    [Route("api/tipos-ingresso")]
    [ApiController]
    public class TiposIngressoController : ControllerBase
    {
        private readonly ITipoIngressoService _service;

        public TiposIngressoController(ITipoIngressoService service)
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
        public IActionResult Add([FromBody] CreateTipoIngressoDto tipoDto)
        {
            var result = _service.Create(tipoDto);
            if (!result.Ok) return BadRequest(new { result.Error });
            return CreatedAtAction(nameof(GetById), new { id = result.Data!.Id }, result.Data);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] UpdateTipoIngressoDto dto)
        {
            var result = _service.Update(id, dto);
            if (!result.Ok) return BadRequest(result.Error);
            return Ok(result.Data);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = _service.Delete(id);
            if (!result.Ok) return BadRequest(result.Error);
            return NoContent();
        }
    }

}