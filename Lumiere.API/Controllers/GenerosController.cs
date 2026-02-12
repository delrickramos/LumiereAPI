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
        public async Task<IActionResult> Get()
        {
            var result = await _service.GetAllAsync();
            return HandleResult(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _service.GetByIdAsync(id);
            return HandleResult(result);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateGeneroDto generoDto)
        {
            var result = await _service.CreateAsync(generoDto);
            return HandleResult(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateGeneroDto generoDto)
        {
            var result = await _service.UpdateAsync(id, generoDto);
            return HandleResult(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var result = await _service.DeleteAsync(id);
            return HandleResult(result);
        }
    }
}