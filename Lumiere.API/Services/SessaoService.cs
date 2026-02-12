using Lumiere.API.Dtos.Sessao;
using Lumiere.API.Interfaces;
using Lumiere.API.Mappers;
using Lumiere.API.Common;

namespace Lumiere.API.Services
{
    // Serviço responsável pela lógica de negócio de sessões
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

        public async Task<ServiceResult<IEnumerable<SessaoDto>>> GetAllAsync()
        {
            var sessoes = (await _sessaoRepo.GetSessoesAsync()).Select(s => s.ToSessaoDto());
            return ServiceResult<IEnumerable<SessaoDto>>.Success(sessoes);
        }

        public async Task<ServiceResult<SessaoDto>> GetByIdAsync(int id)
        {
            if (id <= 0)
                return ServiceResult<SessaoDto>.Fail("Id inválido.", 400);

            var sessao = await _sessaoRepo.GetSessaoByIdAsync(id);
            if (sessao == null)
                return ServiceResult<SessaoDto>.Fail("Sessão não encontrada.", 404);

            return ServiceResult<SessaoDto>.Success(sessao.ToSessaoDto());
        }

        public async Task<ServiceResult<SessaoDto>> CreateAsync(CreateSessaoDto dto)
        {
            if (dto.FilmeId <= 0)
                return ServiceResult<SessaoDto>.Fail("FilmeId inválido.", 400);

            if (!await _filmeRepo.FilmeExistsAsync(dto.FilmeId))
                return ServiceResult<SessaoDto>.Fail("Filme não encontrado.", 404);

            if (dto.SalaId <= 0)
                return ServiceResult<SessaoDto>.Fail("SalaId inválido.", 400);

            if (!await _salaRepo.SalaExistsAsync(dto.SalaId))
                return ServiceResult<SessaoDto>.Fail("Sala não encontrada.", 404);

            if (dto.FormatoSessaoId <= 0)
                return ServiceResult<SessaoDto>.Fail("FormatoSessaoId inválido.", 400);

            if (!await _formatoSessaoRepo.FormatoSessaoExistsAsync(dto.FormatoSessaoId))
                return ServiceResult<SessaoDto>.Fail("Formato de sessão não encontrado.", 404);

            var filme = await _filmeRepo.GetFilmeByIdAsync(dto.FilmeId);

            var inicio = dto.DataHoraInicio;
            if (inicio <= DateTimeOffset.Now)
                return ServiceResult<SessaoDto>.Fail("A data e hora de início da sessão devem ser no futuro.");

            // Calcula automaticamente o horário de fim com base na duração do filme
            var fim = inicio.AddMinutes(filme.DuracaoMinutos);
            var sessao = dto.ToSessaoModel();
            sessao.DataHoraFim = fim;

            // Valida se não há conflito de horário na sala
            if (await _sessaoRepo.SessaoHasConflictAsync(dto.SalaId, dto.DataHoraInicio, sessao.DataHoraFim))
                return ServiceResult<SessaoDto>.Fail("Já existe uma sessão nesta sala neste horário.", 409);

            await _sessaoRepo.AddSessaoAsync(sessao);
            return ServiceResult<SessaoDto>.Success(sessao.ToSessaoDto(), 201);
        }

        public async Task<ServiceResult<SessaoDto>> UpdateAsync(int id, UpdateSessaoDto dto)
        {
            if (id <= 0)
                return ServiceResult<SessaoDto>.Fail("Id inválido.", 400);

            var sessao = await _sessaoRepo.GetSessaoByIdAsync(id);
            if (sessao == null)
                return ServiceResult<SessaoDto>.Fail("Sessão não encontrada.", 404);

            if (dto.SalaId <= 0)
                return ServiceResult<SessaoDto>.Fail("SalaId inválido.", 400);

            if (!await _salaRepo.SalaExistsAsync(dto.SalaId))
                return ServiceResult<SessaoDto>.Fail("Sala não encontrada.", 404);

            // Não permite atualizar sessão que já tem ingressos vendidos
            if (await _sessaoRepo.SessaoHasIngressosAsync(id))
                return ServiceResult<SessaoDto>.Fail("Não é possível atualizar sessão com ingressos vendidos.", 409);

            if (dto.FormatoSessaoId <= 0)
                return ServiceResult<SessaoDto>.Fail("FormatoSessaoId inválido.", 400);

            if (!await _formatoSessaoRepo.FormatoSessaoExistsAsync(dto.FormatoSessaoId))
                return ServiceResult<SessaoDto>.Fail("Formato de sessão não encontrado.", 404);

            dto.UpdateSessaoModel(sessao);
            var filme = await _filmeRepo.GetFilmeByIdAsync(sessao.FilmeId);
            sessao.DataHoraFim = dto.DataHoraInicio.AddMinutes(filme.DuracaoMinutos);

            if (await _sessaoRepo.SessaoHasConflictAsync(dto.SalaId, dto.DataHoraInicio, sessao.DataHoraFim, id))
                return ServiceResult<SessaoDto>.Fail("Já existe uma sessão nesta sala neste horário.", 409);

            await _sessaoRepo.UpdateSessaoAsync(sessao);

            return ServiceResult<SessaoDto>.Success(sessao.ToSessaoDto());
        }

        public async Task<ServiceResult<object>> DeleteAsync(int id)
        {
            if (id <= 0)
                return ServiceResult<object>.Fail("Id inválido.", 400);

            var sessao = await _sessaoRepo.GetSessaoByIdAsync(id);
            if (sessao == null)
                return ServiceResult<object>.Fail("Sessão não encontrada.", 404);

            // Não permite excluir sessão que já tem ingressos vendidos
            if (await _sessaoRepo.SessaoHasIngressosAsync(id))
                return ServiceResult<object>.Fail("Não é possível excluir sessão com ingressos vendidos.", 409);

            await _sessaoRepo.DeleteSessaoAsync(id);
            return ServiceResult<object>.Success(new { }, 204);
        }
    }
}
