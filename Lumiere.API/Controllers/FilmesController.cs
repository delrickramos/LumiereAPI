using Lumiere.API.Repositories;
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
            return Ok(_repository.GetFilmes());
        }
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var filme = _repository.GetFilmeById(id);
            if (filme == null)
                return NotFound();
            return Ok(filme);
        }
        [HttpPost]
        public IActionResult Add([FromBody] Filme filme)
        {
            _repository.AddFilme(filme);
            return CreatedAtAction(nameof(Get), new { id = filme.Id }, filme);
        }

        [HttpPut("{id}")]
        public IActionResult Update([FromBody] Filme filme, int id)
        {
            _repository.UpdateFilme(filme);
            return Ok(filme);
        }
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            _repository.DeleteFilme(id);
            return Ok();
        }
    }
}
