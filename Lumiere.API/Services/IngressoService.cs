using Lumiere.API.Dtos.Ingresso;
using Lumiere.API.Interfaces;
using Lumiere.API.Mappers;
using Lumiere.API.Common;
using Lumiere.Models;

namespace Lumiere.API.Services
{
    public class IngressoService : IIngressoService
    {
        private readonly IIngressoRepository _ingressoRepo;
        private readonly ISessaoRepository _sessaoRepo;
        private readonly IAssentoRepository _assentoRepo;
        private readonly ITipoIngressoRepository _tipoRepo;

        public IngressoService(IIngressoRepository ingressoRepo, ISessaoRepository sessaoRepo, IAssentoRepository assentoRepo, ITipoIngressoRepository tipoRepo)
        {
            _ingressoRepo = ingressoRepo;
            _sessaoRepo = sessaoRepo;
            _assentoRepo = assentoRepo;
            _tipoRepo = tipoRepo;
        }

        public ServiceResult<IEnumerable<IngressoDto>> GetAll()
        {
            var ingressos = _ingressoRepo.GetIngressos().Select(i => i.ToIngressoDto());
            return ServiceResult<IEnumerable<IngressoDto>>.Success(ingressos);
        }

        public ServiceResult<IngressoDto> GetById(int id)
        {
            if (id <= 0)
                return ServiceResult<IngressoDto>.Fail("Id inválido.", 400);

            var ingresso = _ingressoRepo.GetIngressoById(id);
            if (ingresso == null)
                return ServiceResult<IngressoDto>.Fail("Ingresso não encontrado.", 404);

            return ServiceResult<IngressoDto>.Success(ingresso.ToIngressoDto());
        }

        public ServiceResult<IEnumerable<IngressoDto>> GetBySessao(int sessaoId)
        {
            if (sessaoId <= 0)
                return ServiceResult<IEnumerable<IngressoDto>>.Fail("SessaoId inválido.", 400);

            if (!_sessaoRepo.SessaoExists(sessaoId))
                return ServiceResult<IEnumerable<IngressoDto>>.Fail("Sessão não encontrada.", 404);

            var ingressos = _ingressoRepo.GetIngressosBySessao(sessaoId).Select(i => i.ToIngressoDto());
            return ServiceResult<IEnumerable<IngressoDto>>.Success(ingressos);
        }

        public ServiceResult<IngressoDto> Vender(CreateIngressoDto dto)
        {
            if (dto.SessaoId <= 0) return ServiceResult<IngressoDto>.Fail("SessaoId inválido.", 400);
            if (dto.AssentoId <= 0) return ServiceResult<IngressoDto>.Fail("AssentoId inválido.", 400);
            if (dto.TipoIngressoId <= 0) return ServiceResult<IngressoDto>.Fail("TipoIngressoId inválido.", 400);

            var sessao = _sessaoRepo.GetSessaoById(dto.SessaoId);
            if (sessao == null)
                return ServiceResult<IngressoDto>.Fail("Sessão não encontrada.", 404);

            var assento = _assentoRepo.GetAssentoById(dto.AssentoId);
            if (assento == null)
                return ServiceResult<IngressoDto>.Fail("Assento não encontrado.", 404);

            if (assento.SalaId != sessao.SalaId)
                return ServiceResult<IngressoDto>.Fail("Assento não pertence à sala dessa sessão.", 400);

            if (_ingressoRepo.AssentoOcupadoNaSessao(dto.SessaoId, dto.AssentoId))
                return ServiceResult<IngressoDto>.Fail("Assento já está ocupado nesta sessão.", 409);

            var tipo = _tipoRepo.GetTipoIngressoById(dto.TipoIngressoId);
            if (tipo == null)
                return ServiceResult<IngressoDto>.Fail("Tipo de ingresso não encontrado.", 404);

            if (tipo.DescontoPercentual < 0m || tipo.DescontoPercentual > 100m)
                return ServiceResult<IngressoDto>.Fail("DescontoPercentual inválido.", 400);

            var agora = DateTimeOffset.Now;
            if (sessao.DataHoraInicio <= agora.AddMinutes(30))
                return ServiceResult<IngressoDto>.Fail("Não é possível vender ingresso com menos de 30 minutos para o início da sessão.", 400);

            var fator = 1m - (tipo.DescontoPercentual / 100m);
            var precoFinal = sessao.PrecoBase * fator;

            if (precoFinal < 0m)
                return ServiceResult<IngressoDto>.Fail("Preço final inválido.", 400);

            var ingresso = new Ingresso
            {
                SessaoId = dto.SessaoId,
                AssentoId = dto.AssentoId,
                TipoIngressoId = dto.TipoIngressoId,
                PrecoFinal = precoFinal,
                Status = StatusIngressoEnum.Confirmado
            };

            _ingressoRepo.AddIngresso(ingresso);
            return ServiceResult<IngressoDto>.Success(ingresso.ToIngressoDto(), 201);
        }
    }
}
