using Lumiere.API.Dtos.TipoIngresso;
using Lumiere.API.Interfaces;
using Lumiere.API.Mappers;
using Lumiere.API.Common;

namespace Lumiere.API.Services
{
    public class TipoIngressoService : ITipoIngressoService
    {
        private readonly ITipoIngressoRepository _repo;

        private const int NomeMin = 2;
        private const int NomeMax = 60;
        private const decimal DescontoMin = 0m;
        private const decimal DescontoMax = 100m;

        public TipoIngressoService(ITipoIngressoRepository tipoIngressoRepository)
        {
            _repo = tipoIngressoRepository;
        }

        public async Task<ServiceResult<IEnumerable<TipoIngressoDto>>> GetAllAsync()
        {
            var tipos = (await _repo.GetTiposIngressoAsync()).Select(t => t.ToTipoIngressoDto());
            return ServiceResult<IEnumerable<TipoIngressoDto>>.Success(tipos);
        }

        public async Task<ServiceResult<TipoIngressoDto>> GetByIdAsync(int id)
        {
            if (id <= 0)
                return ServiceResult<TipoIngressoDto>.Fail("Id inválido.", 400);

            var tipo = await _repo.GetTipoIngressoByIdAsync(id);
            if (tipo == null)
                return ServiceResult<TipoIngressoDto>.Fail("Tipo de ingresso não encontrado.", 404);

            return ServiceResult<TipoIngressoDto>.Success(tipo.ToTipoIngressoDto());
        }

        public async Task<ServiceResult<TipoIngressoDto>> CreateAsync(CreateTipoIngressoDto dto)
        {
            var nome = (dto.Nome ?? "").Trim();

            var nomeValidation = ValidateNome(nome);
            if (nomeValidation != null)
                return ServiceResult<TipoIngressoDto>.Fail(nomeValidation, 400);

            var descontoValidation = ValidateDesconto(dto.DescontoPercentual);
            if (descontoValidation != null)
                return ServiceResult<TipoIngressoDto>.Fail(descontoValidation, 400);

            if (await _repo.TipoIngressoNomeExistsAsync(nome))
                return ServiceResult<TipoIngressoDto>.Fail("Já existe um tipo de ingresso com esse nome.", 409);

            var tipo = dto.ToTipoIngressoModel();
            tipo.Nome = nome;
            await _repo.AddTipoIngressoAsync(tipo);
            return ServiceResult<TipoIngressoDto>.Success(tipo.ToTipoIngressoDto(), 201);
        }

        public async Task<ServiceResult<TipoIngressoDto>> UpdateAsync(int id, UpdateTipoIngressoDto dto)
        {
            if (id <= 0)
                return ServiceResult<TipoIngressoDto>.Fail("Id inválido.", 400);

            var tipo = await _repo.GetTipoIngressoByIdAsync(id);
            if (tipo == null)
                return ServiceResult<TipoIngressoDto>.Fail("Tipo de ingresso não encontrado.", 404);

            var nome = (dto.Nome ?? "").Trim();

            var nomeValidation = ValidateNome(nome);
            if (nomeValidation != null)
                return ServiceResult<TipoIngressoDto>.Fail(nomeValidation, 400);

            var descontoValidation = ValidateDesconto(dto.DescontoPercentual);
            if (descontoValidation != null)
                return ServiceResult<TipoIngressoDto>.Fail(descontoValidation, 400);

            if (await _repo.TipoIngressoNomeExistsAsync(nome, ignoreId: id))
                return ServiceResult<TipoIngressoDto>.Fail("Já existe um tipo de ingresso com esse nome.", 409);

            tipo.Nome = nome;
            tipo.DescontoPercentual = dto.DescontoPercentual;

            await _repo.UpdateTipoIngressoAsync(tipo);
            return ServiceResult<TipoIngressoDto>.Success(tipo.ToTipoIngressoDto());
        }

        public async Task<ServiceResult<object>> DeleteAsync(int id)
        {
            if (id <= 0)
                return ServiceResult<object>.Fail("Id inválido.", 400);

            var tipo = await _repo.GetTipoIngressoByIdAsync(id);
            if (tipo == null)
                return ServiceResult<object>.Fail("Tipo de ingresso não encontrado.", 404);

            if (await _repo.TipoIngressoHasIngressosAsync(id))
                return ServiceResult<object>.Fail("Não é possível excluir um tipo de ingresso que possui ingressos vinculados.", 409);

            await _repo.DeleteTipoIngressoAsync(id);
            return ServiceResult<object>.Success(new { }, 204);
        }

        private string? ValidateNome(string nome)
        {
            if (string.IsNullOrWhiteSpace(nome))
                return "Nome é obrigatório.";

            if (nome.Length < NomeMin || nome.Length > NomeMax)
                return $"Nome deve ter entre {NomeMin} e {NomeMax} caracteres.";

            return null;
        }

        private string? ValidateDesconto(decimal descontoPercentual)
        {
            if (descontoPercentual < DescontoMin || descontoPercentual > DescontoMax)
                return "DescontoPercentual deve estar entre 0 e 100.";

            return null;
        }
    }
}
