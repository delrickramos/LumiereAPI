using Lumiere.API.Dtos.Filme;
using Lumiere.API.Dtos.FormatoSessao;
using Lumiere.API.Interfaces;
using Lumiere.API.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace Lumiere.API.Controllers
{
    [Route("api/formatos-sessao")]
    [ApiController]
    public class FormatosSessaoController : ControllerBase
    {
        private readonly IFormatoSessaoRepository _formatoRepo;
        public FormatosSessaoController(IFormatoSessaoRepository formatoRepo)
        {
            _formatoRepo = formatoRepo;
        }
        [HttpGet]
        public IActionResult Get()
        {
            var formatos = _formatoRepo.GetFormatosSessoes();
            return Ok(formatos.Select(fs => fs.ToFormatoSessaoDto()));
        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var formato = _formatoRepo.GetFormatoSessaoById(id);
            if (formato == null)
            {
                return NotFound();
            }
            return Ok(formato.ToFormatoSessaoDto());
        }
        [HttpPost]
        public IActionResult Add([FromBody] CreateFormatoSessaoDto formatoDto)
        {
            var formato = formatoDto.ToFormatoSessaoModel();

            _formatoRepo.AddFormatoSessao(formato);

            return CreatedAtAction(nameof(GetById), new { id = formato.Id }, formato.ToFormatoSessaoDto());
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (_formatoRepo.GetFormatoSessaoById(id) == null)
            {
                return NotFound();
            }
            _formatoRepo.DeleteFormatoSessao(id);
            return NoContent();
        }
    }
}
