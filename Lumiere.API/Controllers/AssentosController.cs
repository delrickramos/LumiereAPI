using Lumiere.API.Dtos.Assento;
using Lumiere.API.Interfaces;
using Lumiere.API.Mappers;
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

        [HttpGet("sala/{salaId}")]
        public IActionResult GetBySala(int salaId)
        {
            var result = _service.GetBySala(salaId);
            return HandleResult(result);
        }

        [HttpPost]
        public IActionResult Add([FromBody] CreateAssentoDto assentoDto)
        {
            var result = _service.Create(assentoDto);
            return HandleResult(result);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] UpdateAssentoDto assentoDto)
        {
            var result = _service.Update(id, assentoDto);
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