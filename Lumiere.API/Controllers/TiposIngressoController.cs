using Lumiere.API.Dtos.TipoIngresso;
using Lumiere.API.Interfaces;
using Lumiere.API.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace Lumiere.API.Controllers
{
    [Route("api/tipos-ingresso")]
    [ApiController]
    public class TiposIngressoController : ControllerBase
    {
        private readonly ITipoIngressoRepository _tipoRepo;

        public TiposIngressoController(ITipoIngressoRepository tipoRepo)
        {
            _tipoRepo = tipoRepo;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var tipos = _tipoRepo.GetTiposIngresso();
            return Ok(tipos.Select(ti => ti.ToTipoIngressoDto()));
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var tipo = _tipoRepo.GetTipoIngressoById(id);
            if (tipo == null)
            {
                return NotFound();
            }
            return Ok(tipo.ToTipoIngressoDto());
        }

        [HttpPost]
        public IActionResult Add([FromBody] CreateTipoIngressoDto tipoDto)
        {
            var tipo = tipoDto.ToTipoIngressoModel();

            _tipoRepo.AddTipoIngresso(tipo);

            return CreatedAtAction(nameof(GetById), new { id = tipo.Id }, tipo.ToTipoIngressoDto());
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (_tipoRepo.GetTipoIngressoById(id) == null)
            {
                return NotFound();
            }
            _tipoRepo.DeleteTipoIngresso(id);
            return NoContent();
        }
    }

}