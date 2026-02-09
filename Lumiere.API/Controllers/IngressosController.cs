using Lumiere.API.Dtos.Ingresso;
using Lumiere.API.Interfaces;
using Lumiere.API.Mappers;
using Microsoft.AspNetCore.Mvc;
using Lumiere.Models;

namespace Lumiere.API.Controllers
{
    [Route("api/ingressos")]
    [ApiController]
    public class IngressosController : ControllerBase
    {
        private readonly IIngressoRepository _ingressoRepo;
        private readonly ISessaoRepository _sessaoRepo;
        private readonly IAssentoRepository _assentoRepo;
        private readonly ITipoIngressoRepository _tipoIngressoRepo;

        public IngressosController(
            IIngressoRepository ingressoRepo, 
            ISessaoRepository sessaoRepo, 
            IAssentoRepository assentoRepo, 
            ITipoIngressoRepository tipoIngressoRepo)
        {
            _ingressoRepo = ingressoRepo;
            _sessaoRepo = sessaoRepo;
            _assentoRepo = assentoRepo;
            _tipoIngressoRepo = tipoIngressoRepo;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var ingressos = _ingressoRepo.GetIngressos();
            return Ok(ingressos.Select(i => i.ToIngressoDto()));
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var ingresso = _ingressoRepo.GetIngressoById(id);
            if (ingresso == null)
                return NotFound();
            return Ok(ingresso.ToIngressoDto());
        }

        [HttpGet("sessao/{sessaoId}")]
        public IActionResult GetBySessao(int sessaoId)
        {
            var ingressos = _ingressoRepo.GetIngressosBySessao(sessaoId);
            return Ok(ingressos.Select(i => i.ToIngressoDto()));
        }

        [HttpPost]
        public IActionResult VenderIngresso([FromBody] CreateIngressoDto ingressoDto)
        {
            if (!_sessaoRepo.SessaoExists(ingressoDto.SessaoId))
            {
                return BadRequest("Sessão não encontrada");
            }

            if (!_assentoRepo.AssentoExists(ingressoDto.AssentoId))
            {
                return BadRequest("Assento não encontrado");
            }

            if (!_tipoIngressoRepo.TipoIngressoExists(ingressoDto.TipoIngressoId))
            {
                return BadRequest("Tipo de ingresso não encontrado");
            }

            if (_ingressoRepo.AssentoOcupadoNaSessao(ingressoDto.SessaoId, ingressoDto.AssentoId))
            {
                return BadRequest("Assento já está ocupado nesta sessão");
            }

            var sessao = _sessaoRepo.GetSessaoById(ingressoDto.SessaoId);
            var tipoIngresso = _tipoIngressoRepo.GetTipoIngressoById(ingressoDto.TipoIngressoId);

            var precoFinal = sessao.PrecoBase * (1 - tipoIngresso.DescontoPercentual);

            var ingresso = new Ingresso
            {
                SessaoId = ingressoDto.SessaoId,
                AssentoId = ingressoDto.AssentoId,
                TipoIngressoId = ingressoDto.TipoIngressoId,
                PrecoFinal = precoFinal,
                ExpiraEm = sessao.DataHoraInicio.AddMinutes(-30),
                Status = StatusIngressoEnum.Confirmado
            };

            _ingressoRepo.AddIngresso(ingresso);
            return CreatedAtAction(nameof(GetById), new { id = ingresso.Id }, ingresso.ToIngressoDto());
        }

        [HttpPut("{id}/cancelar")]
        public IActionResult CancelarIngresso(int id)
        {
            var ingresso = _ingressoRepo.GetIngressoById(id);
            if (ingresso == null)
                return NotFound();

            if (ingresso.Status == StatusIngressoEnum.Cancelado)
            {
                return BadRequest("Ingresso já está cancelado");
            }

            ingresso.Status = StatusIngressoEnum.Cancelado;
            _ingressoRepo.UpdateIngresso(ingresso);

            return Ok(new { message = "Ingresso cancelado com sucesso", ingresso = ingresso.ToIngressoDto() });
        }
    }
}