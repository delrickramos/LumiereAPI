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
        public IActionResult Add([FromBody] CreateFormatoSessaoDto formatoDto)
        {
            var result = _service.Create(formatoDto);
            return HandleResult(result);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] UpdateFormatoSessaoDto dto)
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
