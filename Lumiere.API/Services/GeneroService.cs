using Lumiere.API.Dtos.Genero;
using Lumiere.API.Interfaces;
using Lumiere.API.Mappers;
using Lumiere.API.Common;

namespace Lumiere.API.Services
{
    public class GeneroService : IGeneroService
    {
        private readonly IGeneroRepository _generoRepo;

        private const int NomeMin = 2;
        private const int NomeMax = 60;

        public GeneroService(IGeneroRepository repo)
        {
            _generoRepo = repo;
        }
        public async Task<ServiceResult<GeneroDto>> CreateAsync(CreateGeneroDto dto)
        {
            var nome = (dto.Nome ?? "").Trim();

            var nomeValidation = ValidateNome(nome);

            if (nomeValidation != null)
                return ServiceResult<GeneroDto>.Fail(nomeValidation, 400);

            if (await _generoRepo.GeneroNomeExistsAsync(nome))
                return ServiceResult<GeneroDto>.Fail("Já existe um gênero com esse nome.", 409);

            var genero = dto.ToGeneroModel();
            genero.Nome = nome;
            await _generoRepo.AddGeneroAsync(genero);
            return ServiceResult<GeneroDto>.Success(genero.ToGeneroDto(), 201);
        }

        public async Task<ServiceResult<object>> DeleteAsync(int id)
        {
            if (id <= 0)
                return ServiceResult<object>.Fail("Id inválido.", 400);

            if (!await _generoRepo.GeneroExistsAsync(id))
                return ServiceResult<object>.Fail("Gênero não encontrado.", 404);

            if (await _generoRepo.GeneroHasFilmesAsync(id))
                return ServiceResult<object>.Fail("Não é possível excluir um gênero que possui filmes vinculados.", 409);

            await _generoRepo.DeleteGeneroAsync(id);
            return ServiceResult<object>.Success(new { }, 204);
        }

        public async Task<ServiceResult<IEnumerable<GeneroDto>>> GetAllAsync()
        {
            var generos = (await _generoRepo.GetGenerosAsync()).Select(g => g.ToGeneroDto());
            return ServiceResult<IEnumerable<GeneroDto>>.Success(generos);
        }

        public async Task<ServiceResult<GeneroDto>> GetByIdAsync(int id)
        {
            if (id <= 0)
                return ServiceResult<GeneroDto>.Fail("Id inválido.", 400);

            var genero = await _generoRepo.GetGeneroByIdAsync(id);
            if (genero == null)
                return ServiceResult<GeneroDto>.Fail("Gênero não encontrado.", 404);

            return ServiceResult<GeneroDto>.Success(genero.ToGeneroDto());
        }

        public async Task<ServiceResult<GeneroDto>> UpdateAsync(int id, UpdateGeneroDto dto)
        {
            if (id <= 0)
                return ServiceResult<GeneroDto>.Fail("Id inválido.", 400);

            var genero = await _generoRepo.GetGeneroByIdAsync(id);
            if (genero == null)
                return ServiceResult<GeneroDto>.Fail("Gênero não encontrado.", 404);

            var nome = (dto.Nome ?? "").Trim();

            var nomeValidation = ValidateNome(nome);
            if (nomeValidation != null)
                return ServiceResult<GeneroDto>.Fail(nomeValidation, 400);

            if (await _generoRepo.GeneroNomeExistsAsync(nome, ignoreId: id))
                return ServiceResult<GeneroDto>.Fail("Já existe um gênero com esse nome.", 409);

            dto.UpdateGeneroModel(genero);
            genero.Nome = nome;
            await _generoRepo.UpdateGeneroAsync(genero);
            return ServiceResult<GeneroDto>.Success(genero.ToGeneroDto());
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