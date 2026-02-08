using Lumiere.API.Interfaces;
using Lumiere.API.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace Lumiere.API.Controllers
{
    [Route("api/sessoes")]
    [ApiController]
    public class SessoesController : ControllerBase
    {
        private readonly ISessaoRepository _repository;
        public SessoesController(ISessaoRepository repository)
        {
            _repository = repository;
        }
        [HttpGet]
        public IActionResult Get()
        {
            var sessoes = _repository.GetSessoes();

            var sessoesDto = sessoes.Select(s => s.ToSessaoDto());

            return Ok(sessoesDto);
        }
        public IActionResult GetById(int id)
        {
            var sessao = _repository.GetSessaoById(id);
            if (sessao == null)
                return NotFound();
            return Ok(sessao.ToSessaoDto());
        }
    }
}
