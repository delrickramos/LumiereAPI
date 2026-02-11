using Lumiere.API.Dtos.Sessao;
using Lumiere.API.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Lumiere.API.Controllers
{
    // Controller para gerenciamento de sessões
    [Route("api/sessoes")]
    public class SessoesController : ServiceResultController
    {
        private readonly ISessaoService _service;
        public SessoesController(ISessaoService service)
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
        public IActionResult Add([FromBody] CreateSessaoDto sessaoDto)
        {
            var result = _service.Create(sessaoDto);
            return HandleResult(result);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] UpdateSessaoDto sessaoDto)
        {
            var result = _service.Update(id, sessaoDto);
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
