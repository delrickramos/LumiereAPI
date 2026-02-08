using Lumiere.API.Dtos.Genero;
using Lumiere.API.Interfaces;
using Lumiere.API.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace Lumiere.API.Controllers
{
    [Route("api/generos")]
    [ApiController]
    public class GenerosController : ControllerBase
    {
        private readonly IGeneroRepository _generoRepo;
        public GenerosController(IGeneroRepository generoRepo)
        {
            _generoRepo = generoRepo;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var generos = _generoRepo.GetGeneros();
            return Ok(generos.Select(g => g.ToGeneroDto()));
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var genero = _generoRepo.GetGeneroById(id);
            if (genero == null)
                return NotFound();
            return Ok(genero.ToGeneroDto());
        }

        [HttpPost]
        public IActionResult Add([FromBody] CreateGeneroDto generoDto)
        {
            var genero = generoDto.ToGeneroModel();
            _generoRepo.AddGenero(genero);
            return CreatedAtAction(nameof(GetById), new { id = genero.Id }, genero.ToGeneroDto());
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] UpdateGeneroDto generoDto)
        {
            var genero = _generoRepo.GetGeneroById(id);
            if (genero == null)
                return NotFound();

            generoDto.UpdateGeneroModel(genero);
            _generoRepo.UpdateGenero(genero);

            return Ok(genero.ToGeneroDto());
        }

        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            if (_generoRepo.GetGeneroById(id) == null)
                return NotFound();

            _generoRepo.DeleteGenero(id);
            return NoContent();
        }
    }
}