using Lumiere.API.Dtos.FormatoSessao;
using Microsoft.AspNetCore.Mvc;
using Lumiere.API.Interfaces;
namespace Lumiere.API.Controllers
{
    [Route("api/formatos-sessao")]
    public class FormatosSessaoController : ServiceResultController
    {
        private readonly IFormatoSessaoService _service;
        public FormatosSessaoController(IFormatoSessaoService formatoSessaoService)
        {
            _service = formatoSessaoService;
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
        public async Task<IActionResult> Add([FromBody] CreateFormatoSessaoDto formatoDto)
        {
            var result = await _service.CreateAsync(formatoDto);
            return HandleResult(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateFormatoSessaoDto dto)
        {
            var result = await _service.UpdateAsync(id, dto);
            return HandleResult(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _service.DeleteAsync(id);
            return HandleResult(result);
        }
    }
}
