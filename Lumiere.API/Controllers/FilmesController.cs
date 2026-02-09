using Lumiere.API.Dtos.Filme;
using Lumiere.API.Interfaces;
using Lumiere.API.Mappers;
using Lumiere.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Lumiere.API.Controllers
{
    [Route("api/filmes")]
    [ApiController]
    public class FilmesController : ControllerBase
    {
        private readonly IFilmeService _service;

        public FilmesController(IFilmeService service)
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
        public IActionResult Add([FromBody] CreateFilmeDto filmeDto)
        {
            var result = _service.Create(filmeDto);
            if (!result.Ok) return BadRequest(new { result.Error });
            return CreatedAtAction(nameof(GetById), new { id = result.Data!.Id }, result.Data);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] UpdateFilmeDto filmeDto)
        {
            var result = _service.Update(id, filmeDto);
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
