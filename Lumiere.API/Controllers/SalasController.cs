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
        public IActionResult Add([FromBody] CreateSalaDto salaDto)
        {
            var result = _service.Create(salaDto);
            return HandleResult(result);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] UpdateSalaDto salaDto)
        {
            var result = _service.Update(id, salaDto);
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
