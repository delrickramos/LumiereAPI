using Lumiere.API.Dtos.Sala;
using Microsoft.AspNetCore.Mvc;
using Lumiere.API.Interfaces;

namespace Lumiere.API.Controllers
{
    [Route("api/salas")]
    public class SalasController : ServiceResultController
    {
        private readonly ISalaService _service;
        public SalasController(ISalaService service)
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

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateSalaDto salaDto)
        {
            var result = await _service.CreateAsync(salaDto);
            return HandleResult(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateSalaDto salaDto)
        {
            var result = await _service.UpdateAsync(id, salaDto);
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
