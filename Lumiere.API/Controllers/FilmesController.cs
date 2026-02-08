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
        private readonly IFilmeRepository _repository;

        public FilmesController(IFilmeRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var filmes = _repository.GetFilmes();
            return Ok(filmes.Select( f => f.ToFilmeDto()));
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var filme = _repository.GetFilmeById(id);
            if (filme == null)
                return NotFound();
            return Ok(filme.ToFilmeDto());
        }

        [HttpPost]
        public IActionResult Add([FromBody] CreateFilmeDto filmeDto)
        {
            var filme = filmeDto.ToFilmeModel();

            _repository.AddFilme(filme);

            return CreatedAtAction(nameof(Get), new { id = filme.Id }, filme.ToFilmeDto());
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] UpdateFilmeDto filmeDto)
        {
            var filme = _repository.GetFilmeById(id);
            if (filme == null)
                return NotFound();

            filmeDto.UpdateFilmeModel(filme);
            _repository.UpdateFilme(filme);

            return Ok(filme.ToFilmeDto());
        }
        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            if (_repository.GetFilmeById(id) == null)
                return NotFound();

            _repository.DeleteFilme(id);
            return NoContent();
        }
    }
}
