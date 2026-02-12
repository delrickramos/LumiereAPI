using Lumiere.API.Dtos.Filme;
using Lumiere.API.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Lumiere.API.Controllers
{
    // Controller para gerenciamento de filmes
    [Route("api/filmes")]
    public class FilmesController : ServiceResultController
    {
        private readonly IFilmeService _service;

        public FilmesController(IFilmeService service)
        {
            _service = service;
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

        // Endpoint para listar filmes em cartaz (requisito: próximos 7 dias)
        [HttpGet("em-cartaz")]
        public async Task<IActionResult> GetEmCartaz()
        {
            var result = await _service.GetEmCartazAsync();
            return HandleResult(result);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateFilmeDto filmeDto)
        {
            var result = await _service.CreateAsync(filmeDto);
            return HandleResult(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateFilmeDto filmeDto)
        {
            var result = await _service.UpdateAsync(id, filmeDto);
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
