using Lumiere.API.Dtos.TipoIngresso;
using Lumiere.API.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Lumiere.API.Controllers
{
    [Route("api/tipos-ingresso")]
    public class TiposIngressoController : ServiceResultController
    {
        private readonly ITipoIngressoService _service;

        public TiposIngressoController(ITipoIngressoService service)
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
        public IActionResult Add([FromBody] CreateTipoIngressoDto tipoDto)
        {
            var result = _service.Create(tipoDto);
            return HandleResult(result);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] UpdateTipoIngressoDto dto)
        {
            var result = _service.Update(id, dto);
            return HandleResult(result);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = _service.Delete(id);
            return HandleResult(result);
        }
    }

}