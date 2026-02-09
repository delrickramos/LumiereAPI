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
        private readonly IAssentoRepository _assentoRepo;
        private readonly ISalaRepository _salaRepo;

        public AssentosController(IAssentoRepository assentoRepo, ISalaRepository salaRepo)
        {
            _assentoRepo = assentoRepo;
            _salaRepo = salaRepo;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var assentos = _assentoRepo.GetAssentos();
            return Ok(assentos.Select(a => a.ToAssentoDto()));
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var assento = _assentoRepo.GetAssentoById(id);
            if (assento == null)
                return NotFound();
            return Ok(assento.ToAssentoDto());
        }

        [HttpGet("sala/{salaId}")]
        public IActionResult GetBySala(int salaId)
        {
            if (!_salaRepo.SalaExists(salaId))
                return NotFound("Sala não encontrada");

            var assentos = _assentoRepo.GetAssentosBySala(salaId);
            return Ok(assentos.Select(a => a.ToAssentoDto()));
        }

        [HttpPost]
        public IActionResult Add([FromBody] CreateAssentoDto assentoDto)
        {
            if (!_salaRepo.SalaExists(assentoDto.SalaId))
            {
                return BadRequest("Sala não encontrada");
            }

            var assento = assentoDto.ToAssentoModel();
            _assentoRepo.AddAssento(assento);
            return CreatedAtAction(nameof(GetById), new { id = assento.Id }, assento.ToAssentoDto());
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] UpdateAssentoDto assentoDto)
        {
            var assento = _assentoRepo.GetAssentoById(id);
            if (assento == null)
                return NotFound();

            if (!_salaRepo.SalaExists(assentoDto.SalaId))
            {
                return BadRequest("Sala não encontrada");
            }

            assentoDto.UpdateAssentoModel(assento);
            _assentoRepo.UpdateAssento(assento);

            return Ok(assento.ToAssentoDto());
        }

        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            var assento = _assentoRepo.GetAssentoById(id);
            if (assento == null)
                return NotFound();

            if (assento.Ingressos?.Any() == true)
            {
                return BadRequest("Não é possível excluir assento com ingressos vendidos");
            }

            _assentoRepo.DeleteAssento(id);
            return NoContent();
        }
    }
}