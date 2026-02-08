using Lumiere.API.Dtos.Sessao;
using Lumiere.API.Interfaces;
using Lumiere.API.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace Lumiere.API.Controllers
{
    [Route("api/sessoes")]
    [ApiController]
    public class SessoesController : ControllerBase
    {
        private readonly ISessaoRepository _sessaoRepo;
        private readonly IFilmeRepository _filmeRepo;
        public SessoesController(ISessaoRepository sessaoRepo, IFilmeRepository filmeRepo)
        {
            _sessaoRepo = sessaoRepo;
            _filmeRepo = filmeRepo;
        }
        [HttpGet]
        public IActionResult Get()
        {
            var sessoes = _sessaoRepo.GetSessoes();

            var sessoesDto = sessoes.Select(s => s.ToSessaoDto());

            return Ok(sessoesDto);
        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var sessao = _sessaoRepo.GetSessaoById(id);
            if (sessao == null)
                return NotFound();
            return Ok(sessao.ToSessaoDto());
        }

        [HttpPost("{FilmeId}")]
        public IActionResult Add([FromRoute] int FilmeId, CreateSessaoDto sessaoDto )
        {
            if (!_filmeRepo.FilmeExists(FilmeId))
            {
                return BadRequest("Filme não encontrado");
            }
            var sessao = sessaoDto.ToSessaoModel(FilmeId);
            _sessaoRepo.AddSessao(sessao);
            return CreatedAtAction(nameof(GetById), new { id = sessao.Id }, sessao.ToSessaoDto());
        }
}
}
