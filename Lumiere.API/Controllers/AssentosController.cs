using Lumiere.API.Dtos.Assento;
using Lumiere.API.Interfaces;
using Lumiere.API.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace Lumiere.API.Controllers
{
    [Route("api/assentos")]
    [ApiController]
    public class AssentosController : ControllerBase
    {
        private readonly IAssentoService _service;

        public AssentosController(IAssentoService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var result = _service.GetAll();
            if (!result.Ok) return BadRequest(result.Error);
            return Ok(result.Data);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var result = _service.GetById(id);
            if (!result.Ok) return NotFound(result.Error);
            return Ok(result.Data);
        }

        [HttpGet("sala/{salaId}")]
        public IActionResult GetBySala(int salaId)
        {
            var result = _service.GetBySala(salaId);
            if (!result.Ok) return BadRequest(result.Error);
            return Ok(result.Data);
        }
    }
}