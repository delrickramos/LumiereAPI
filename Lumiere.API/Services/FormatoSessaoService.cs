using Lumiere.API.Dtos.FormatoSessao;
using Lumiere.API.Interfaces;
using Lumiere.API.Mappers;
using Lumiere.API.Common;

namespace Lumiere.API.Services
{
    public class FormatoSessaoService : IFormatoSessaoService
    {

        private readonly IFormatoSessaoRepository _repo;

        private const int NomeMin = 2;
        private const int NomeMax = 60;

        public FormatoSessaoService(IFormatoSessaoRepository repo)
        {
            _repo = repo;
        }

        public async Task<ServiceResult<FormatoSessaoDto>> CreateAsync(CreateFormatoSessaoDto dto)
        {
            var nome = (dto.Nome ?? "").Trim();

            var nomeValidation = ValidateNome(nome);
            if (nomeValidation != null)
                return ServiceResult<FormatoSessaoDto>.Fail(nomeValidation, 400);

            if (await _repo.FormatoSessaoNomeExistsAsync(nome))
                return ServiceResult<FormatoSessaoDto>.Fail("Já existe um formato de sessão com esse nome.", 409);

            var formato = dto.ToFormatoSessaoModel();
            formato.Nome = nome;
            await _repo.AddFormatoSessaoAsync(formato);
            return ServiceResult<FormatoSessaoDto>.Success(formato.ToFormatoSessaoDto(), 201);
        }

        public async Task<ServiceResult<object>> DeleteAsync(int id)
        {
            if (id <= 0)
                return ServiceResult<object>.Fail("Id inválido.", 400);

            var formato = await _repo.GetFormatoSessaoByIdAsync(id);
            if (formato == null)
                return ServiceResult<object>.Fail("Formato de sessão não encontrado.", 404);

            if (await _repo.FormatoSessaoHasSessoesAsync(id))
                return ServiceResult<object>.Fail("Não é possível excluir um formato de sessão que possui sessões vinculadas.", 409);

            await _repo.DeleteFormatoSessaoAsync(id);
            return ServiceResult<object>.Success(new { }, 204);
        }

        public async Task<ServiceResult<IEnumerable<FormatoSessaoDto>>> GetAllAsync()
        {
            var formatos = (await _repo.GetFormatosSessoesAsync()).Select(f => f.ToFormatoSessaoDto());
            return ServiceResult<IEnumerable<FormatoSessaoDto>>.Success(formatos);
        }

        public async Task<ServiceResult<FormatoSessaoDto>> GetByIdAsync(int id)
        {
            if (id <= 0)
                return ServiceResult<FormatoSessaoDto>.Fail("Id inválido.", 400);

            var formato = await _repo.GetFormatoSessaoByIdAsync(id);
            if (formato == null)
                return ServiceResult<FormatoSessaoDto>.Fail("Formato de sessão não encontrado.", 404);

            return ServiceResult<FormatoSessaoDto>.Success(formato.ToFormatoSessaoDto());
        }

        public async Task<ServiceResult<FormatoSessaoDto>> UpdateAsync(int id, UpdateFormatoSessaoDto dto)
        {
            if (id <= 0)
                return ServiceResult<FormatoSessaoDto>.Fail("Id inválido.", 400);

            var formato = await _repo.GetFormatoSessaoByIdAsync(id);
            if (formato == null)
                return ServiceResult<FormatoSessaoDto>.Fail("Formato de sessão não encontrado.", 404);

            var nome = (dto.Nome ?? "").Trim();

            var nomeValidation = ValidateNome(nome);
            if (nomeValidation != null)
                return ServiceResult<FormatoSessaoDto>.Fail(nomeValidation, 400);

            if (await _repo.FormatoSessaoNomeExistsAsync(nome, ignoreId: id))
                return ServiceResult<FormatoSessaoDto>.Fail("Já existe um formato de sessão com esse nome.", 409);

            dto.UpdateFormatoSessaoModel(formato);
            formato.Nome = nome;
            await _repo.UpdateFormatoSessaoAsync(formato);
            return ServiceResult<FormatoSessaoDto>.Success(formato.ToFormatoSessaoDto());
        }

        private string? ValidateNome(string nome)
        {
            if (string.IsNullOrWhiteSpace(nome))
                return "Nome é obrigatório.";

            if (nome.Length < NomeMin || nome.Length > NomeMax)
                return $"Nome deve ter entre {NomeMin} e {NomeMax} caracteres.";

            return null;
        }
    }
}

