using Lumiere.API.Dtos.Sala;
using Lumiere.API.Interfaces;
using Lumiere.API.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace Lumiere.API.Controllers
{
    [Route("api/salas")]
    [ApiController]
    public class SalasController : ControllerBase
    {
        private readonly ISalaRepository _salaRepo;
        public SalasController(ISalaRepository salaRepo)
        {
            _salaRepo = salaRepo;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var salas = _salaRepo.GetSalas();
            return Ok(salas.Select(s => s.ToSalaDto()));
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var sala = _salaRepo.GetSalaById(id);
            if (sala == null)
                return NotFound();
            return Ok(sala.ToSalaDto());
        }

        [HttpPost]
        public IActionResult Add([FromBody] CreateSalaDto salaDto)
        {
            var sala = salaDto.ToSalaModel();
            _salaRepo.AddSala(sala);
            return CreatedAtAction(nameof(GetById), new { id = sala.Id }, sala.ToSalaDto());
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] UpdateSalaDto salaDto)
        {
            var sala = _salaRepo.GetSalaById(id);
            if (sala == null)
                return NotFound();

            salaDto.UpdateSalaModel(sala);
            _salaRepo.UpdateSala(sala);

            return Ok(sala.ToSalaDto());
        }

        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            if (_salaRepo.GetSalaById(id) == null)
                return NotFound();

            _salaRepo.DeleteSala(id);
            return NoContent();
        }
    }
}
