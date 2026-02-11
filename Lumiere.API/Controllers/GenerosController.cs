using Lumiere.API.Dtos.Genero;
using Microsoft.AspNetCore.Mvc;
using Lumiere.API.Interfaces;

namespace Lumiere.API.Controllers
{
    [Route("api/generos")]
    public class GenerosController : ServiceResultController
    {
        private readonly IGeneroService _service;
        public GenerosController(IGeneroService generoService)
        {
            _service = generoService;
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

        [HttpPost]
        public IActionResult Add([FromBody] CreateGeneroDto generoDto)
        {
            var result = _service.Create(generoDto);
            return HandleResult(result);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] UpdateGeneroDto generoDto)
        {
            var result = _service.Update(id, generoDto);
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