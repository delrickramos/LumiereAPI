using Lumiere.API.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Lumiere.API.Controllers
{
    [Route("api/assentos")]
    public class AssentosController : ServiceResultController
    {
        private readonly IAssentoService _service;

        public AssentosController(IAssentoService service)
        {
            _service = service;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _service.GetByIdAsync(id);
            return HandleResult(result);
        }

        [HttpGet("sala/{salaId}")]
        public async Task<IActionResult> GetBySala(int salaId)
        {
            var result = await _service.GetBySalaAsync(salaId);
            return HandleResult(result);
        }
    }
}