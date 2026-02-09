using Lumiere.API.Dtos.Sessao;
using Lumiere.API.Interfaces;
using Lumiere.API.Mappers;
using Lumiere.API.Services.Interfaces;

namespace Lumiere.API.Services
{
    public class SessaoService : ISessaoService
    {
        private readonly ISessaoRepository _sessaoRepo;
        private readonly IFilmeRepository _filmeRepo;
        private readonly ISalaRepository _salaRepo;

        public SessaoService(ISessaoRepository sessaoRepo, IFilmeRepository filmeRepo, ISalaRepository salaRepo)
        {
            _sessaoRepo = sessaoRepo;
            _filmeRepo = filmeRepo;
            _salaRepo = salaRepo;
        }

        public ServiceResult<IEnumerable<SessaoDto>> GetAll()
        {
            var sessoes = _sessaoRepo.GetSessoes().Select(s => s.ToSessaoDto());
            return ServiceResult<IEnumerable<SessaoDto>>.Success(sessoes);
        }

        public ServiceResult<SessaoDto> GetById(int id)
        {
            if (id <= 0)
                return ServiceResult<SessaoDto>.Fail("Id inválido.");

            var sessao = _sessaoRepo.GetSessaoById(id);
            if (sessao == null)
                return ServiceResult<SessaoDto>.Fail("Sessão não encontrada.");

            return ServiceResult<SessaoDto>.Success(sessao.ToSessaoDto());
        }

        public ServiceResult<SessaoDto> Create(CreateSessaoDto dto)
        {
            if (dto.FilmeId <= 0)
                return ServiceResult<SessaoDto>.Fail("FilmeId inválido.");

            if (!_filmeRepo.FilmeExists(dto.FilmeId))
                return ServiceResult<SessaoDto>.Fail("Filme não encontrado.");

            if (dto.SalaId <= 0)
                return ServiceResult<SessaoDto>.Fail("SalaId inválido.");

            if (!_salaRepo.SalaExists(dto.SalaId))
                return ServiceResult<SessaoDto>.Fail("Sala não encontrada.");

            var sessao = dto.ToSessaoModel();

            _sessaoRepo.AddSessao(sessao);
            return ServiceResult<SessaoDto>.Success(sessao.ToSessaoDto());
        }

        public ServiceResult<SessaoDto> Update(int id, UpdateSessaoDto dto)
        {
            if (id <= 0)
                return ServiceResult<SessaoDto>.Fail("Id inválido.");

            var sessao = _sessaoRepo.GetSessaoById(id);
            if (sessao == null)
                return ServiceResult<SessaoDto>.Fail("Sessão não encontrada.");

            if (dto.SalaId <= 0)
                return ServiceResult<SessaoDto>.Fail("SalaId inválido.");

            if (!_salaRepo.SalaExists(dto.SalaId))
                return ServiceResult<SessaoDto>.Fail("Sala não encontrada.");

            if (_sessaoRepo.SessaoHasIngressos(id))
                return ServiceResult<SessaoDto>.Fail("Não é possível atualizar sessão com ingressos vendidos.");

            dto.UpdateSessaoModel(sessao);
            _sessaoRepo.UpdateSessao(sessao);

            return ServiceResult<SessaoDto>.Success(sessao.ToSessaoDto());
        }

        public ServiceResult<object> Delete(int id)
        {
            if (id <= 0)
                return ServiceResult<object>.Fail("Id inválido.");

            var sessao = _sessaoRepo.GetSessaoById(id);
            if (sessao == null)
                return ServiceResult<object>.Fail("Sessão não encontrada.");

            if (_sessaoRepo.SessaoHasIngressos(id))
                return ServiceResult<object>.Fail("Não é possível excluir sessão com ingressos vendidos.");

            _sessaoRepo.DeleteSessao(id);
            return ServiceResult<object>.Success(new { });
        }
    }
}
