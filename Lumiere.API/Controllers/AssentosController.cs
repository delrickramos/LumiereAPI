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
        public IActionResult GetById(int id)
        {
            var result = _service.GetById(id);
            return HandleResult(result);
        }

        [HttpGet("sala/{salaId}")]
        public IActionResult GetBySala(int salaId)
        {
            var result = _service.GetBySala(salaId);
            return HandleResult(result);
        }
    }
}