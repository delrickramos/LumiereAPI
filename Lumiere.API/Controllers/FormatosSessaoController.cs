using Lumiere.API.Dtos.Filme;
using Lumiere.API.Dtos.FormatoSessao;
using Lumiere.API.Interfaces;
using Lumiere.API.Mappers;
using Microsoft.AspNetCore.Mvc;
using Lumiere.API.Services;
using Lumiere.API.Services.Interfaces;

namespace Lumiere.API.Controllers
{
    [Route("api/formatos-sessao")]
    [ApiController]
    public class FormatosSessaoController : ControllerBase
    {
        private readonly IFormatoSessaoRepository _formatoRepo;
        private readonly IFormatoSessaoService _service;
        public FormatosSessaoController(IFormatoSessaoRepository formatoSessaoRepository, IFormatoSessaoService formatoSessaoService)
        {
            _formatoRepo = formatoSessaoRepository;
            _service = formatoSessaoService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var result = _service.GetAll();
            if (!result.Ok) return BadRequest(result.Error);
            return Ok(result.Data);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var result = _service.GetById(id);
            if (!result.Ok) return NotFound(result.Error);
            return Ok(result.Data);
        }
        
        [HttpPost]
        public IActionResult Add([FromBody] CreateFormatoSessaoDto formatoDto)
        {
            var result = _service.Create(formatoDto);
            if (!result.Ok) return BadRequest(new { result.Error });
            return CreatedAtAction(nameof(GetById), new { id = result.Data!.Id }, result.Data);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] UpdateFormatoSessaoDto dto)
        {
            var result = _service.Update(id, dto);
            if (!result.Ok) return BadRequest(result.Error);
            return Ok(result.Data);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = _service.Delete(id);
            if (!result.Ok) return BadRequest(result.Error);
            return NoContent();
        }
    }
}
