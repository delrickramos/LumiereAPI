using Lumiere.API.Dtos.Sessao;
using Lumiere.API.Interfaces;
using Lumiere.API.Mappers;

namespace Lumiere.API.Services
{
    public class SessaoService : ISessaoService
    {
        private readonly ISessaoRepository _sessaoRepo;
        private readonly IFilmeRepository _filmeRepo;
        private readonly ISalaRepository _salaRepo;
        private readonly IFormatoSessaoRepository _formatoSessaoRepo;

        public SessaoService(ISessaoRepository sessaoRepo, IFilmeRepository filmeRepo, ISalaRepository salaRepo, IFormatoSessaoRepository formatoSessaoRepo)
        {
            _sessaoRepo = sessaoRepo;
            _filmeRepo = filmeRepo;
            _salaRepo = salaRepo;
            _formatoSessaoRepo = formatoSessaoRepo;
        }

        public ServiceResult<IEnumerable<SessaoDto>> GetAll()
        {
            var sessoes = _sessaoRepo.GetSessoes().Select(s => s.ToSessaoDto());
            return ServiceResult<IEnumerable<SessaoDto>>.Success(sessoes);
        }

        public ServiceResult<SessaoDto> GetById(int id)
        {
            if (id <= 0)
                return ServiceResult<SessaoDto>.Fail("Id inválido.", 400);

            var sessao = _sessaoRepo.GetSessaoById(id);
            if (sessao == null)
                return ServiceResult<SessaoDto>.Fail("Sessão não encontrada.", 404);

            return ServiceResult<SessaoDto>.Success(sessao.ToSessaoDto());
        }

        public ServiceResult<SessaoDto> Create(CreateSessaoDto dto)
        {
            if (dto.FilmeId <= 0)
                return ServiceResult<SessaoDto>.Fail("FilmeId inválido.", 400);

            if (!_filmeRepo.FilmeExists(dto.FilmeId))
                return ServiceResult<SessaoDto>.Fail("Filme não encontrado.", 404);

            if (dto.SalaId <= 0)
                return ServiceResult<SessaoDto>.Fail("SalaId inválido.", 400);

            if (!_salaRepo.SalaExists(dto.SalaId))
                return ServiceResult<SessaoDto>.Fail("Sala não encontrada.", 404);

            if (dto.FormatoSessaoId <= 0)
                return ServiceResult<SessaoDto>.Fail("FormatoSessaoId inválido.", 400);

            if (!_formatoSessaoRepo.FormatoSessaoExists(dto.FormatoSessaoId))
                return ServiceResult<SessaoDto>.Fail("Formato de sessão não encontrado.", 404);

            var filme = _filmeRepo.GetFilmeById(dto.FilmeId);

            var inicio = dto.DataHoraInicio;
            if (inicio <= DateTime.Now)
                return ServiceResult<SessaoDto>.Fail("A data e hora de início da sessão devem ser no futuro.");

            var fim = inicio.AddMinutes(filme.DuracaoMinutos);
            var sessao = dto.ToSessaoModel();
            sessao.DataHoraFim = fim;

            if (_sessaoRepo.SessaoHasConflict(dto.SalaId, dto.DataHoraInicio, sessao.DataHoraFim))
                return ServiceResult<SessaoDto>.Fail("Já existe uma sessão nesta sala neste horário.", 409);

            _sessaoRepo.AddSessao(sessao);
            return ServiceResult<SessaoDto>.Success(sessao.ToSessaoDto(), 201);
        }

        public ServiceResult<SessaoDto> Update(int id, UpdateSessaoDto dto)
        {
            if (id <= 0)
                return ServiceResult<SessaoDto>.Fail("Id inválido.", 400);

            var sessao = _sessaoRepo.GetSessaoById(id);
            if (sessao == null)
                return ServiceResult<SessaoDto>.Fail("Sessão não encontrada.", 404);

            if (dto.SalaId <= 0)
                return ServiceResult<SessaoDto>.Fail("SalaId inválido.", 400);

            if (!_salaRepo.SalaExists(dto.SalaId))
                return ServiceResult<SessaoDto>.Fail("Sala não encontrada.", 404);

            if (_sessaoRepo.SessaoHasIngressos(id))
                return ServiceResult<SessaoDto>.Fail("Não é possível atualizar sessão com ingressos vendidos.", 409);

            if (dto.FormatoSessaoId <= 0)
                return ServiceResult<SessaoDto>.Fail("FormatoSessaoId inválido.", 400);

            if (!_formatoSessaoRepo.FormatoSessaoExists(dto.FormatoSessaoId))
                return ServiceResult<SessaoDto>.Fail("Formato de sessão não encontrado.", 404);

            dto.UpdateSessaoModel(sessao);
            var filme = _filmeRepo.GetFilmeById(sessao.FilmeId);
            sessao.DataHoraFim = dto.DataHoraInicio.AddMinutes(filme.DuracaoMinutos);

            if (_sessaoRepo.SessaoHasConflict(dto.SalaId, dto.DataHoraInicio, sessao.DataHoraFim, id))
                return ServiceResult<SessaoDto>.Fail("Já existe uma sessão nesta sala neste horário.", 409);

            _sessaoRepo.UpdateSessao(sessao);

            return ServiceResult<SessaoDto>.Success(sessao.ToSessaoDto());
        }

        public ServiceResult<object> Delete(int id)
        {
            if (id <= 0)
                return ServiceResult<object>.Fail("Id inválido.", 400);

            var sessao = _sessaoRepo.GetSessaoById(id);
            if (sessao == null)
                return ServiceResult<object>.Fail("Sessão não encontrada.", 404);

            if (_sessaoRepo.SessaoHasIngressos(id))
                return ServiceResult<object>.Fail("Não é possível excluir sessão com ingressos vendidos.", 409);

            _sessaoRepo.DeleteSessao(id);
            return ServiceResult<object>.Success(new { }, 204);
        }
    }
}
