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
        private readonly ISalaRepository _salaRepo;
        public SessoesController(ISessaoRepository sessaoRepo, IFilmeRepository filmeRepo, ISalaRepository salaRepo )
        {
            _sessaoRepo = sessaoRepo;
            _filmeRepo = filmeRepo;
            _salaRepo = salaRepo;
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
            if (!_salaRepo.SalaExists(sessaoDto.SalaId))
            {
                return BadRequest("Sala não encontrada");
            }

            var sessao = sessaoDto.ToSessaoModel(FilmeId);
            _sessaoRepo.AddSessao(sessao);
            return CreatedAtAction(nameof(GetById), new { id = sessao.Id }, sessao.ToSessaoDto());
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] UpdateSessaoDto sessaoDto)
        {
            var sessao = _sessaoRepo.GetSessaoById(id);
            if (sessao == null)
                return NotFound();

            if (!_salaRepo.SalaExists(sessaoDto.SalaId))
            {
                return BadRequest("Sala não encontrada");
            }

            if (sessao.Ingressos?.Any() == true)
            {
                return BadRequest("Não é possível atualizar sessão com ingressos vendidos");
            }

            sessaoDto.UpdateSessaoModel(sessao);
            _sessaoRepo.UpdateSessao(sessao);

            return Ok(sessao.ToSessaoDto());
        }

        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            var sessao = _sessaoRepo.GetSessaoById(id);
            if (sessao == null)
                return NotFound();

            if (sessao.Ingressos?.Any() == true)
            {
                return BadRequest("Não é possível excluir sessão com ingressos vendidos");
            }

            _sessaoRepo.DeleteSessao(id);
            return NoContent();
        }
    }
}
