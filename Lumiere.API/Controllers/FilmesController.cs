using Lumiere.API.Dtos.Filme;
using Lumiere.API.Interfaces;
using Lumiere.API.Mappers;
using Lumiere.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Lumiere.API.Controllers
{
    [Route("api/filmes")]
    public class FilmesController : ServiceResultController
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
            return HandleResult(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var result = _service.GetById(id);
            return HandleResult(result);
        }

        [HttpGet("em-cartaz")]
        public IActionResult GetEmCartaz()
        {
            var result = _service.GetEmCartaz();
            return HandleResult(result);
        }

        [HttpPost]
        public IActionResult Add([FromBody] CreateFilmeDto filmeDto)
        {
            var result = _service.Create(filmeDto);
            return HandleResult(result);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] UpdateFilmeDto filmeDto)
        {
            var result = _service.Update(id, filmeDto);
            return HandleResult(result);
        }
        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            var result = _service.Delete(id);
            return HandleResult(result);
        }
    }
}
