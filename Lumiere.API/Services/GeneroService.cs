using Lumiere.API.Dtos.Genero;
using Lumiere.API.Interfaces;
using Lumiere.API.Mappers;

namespace Lumiere.API.Services;
public class GeneroService : IGeneroService
{
    private readonly IGeneroRepository _generoRepo;

    private const int NomeMin = 2;
    private const int NomeMax = 60;

    public GeneroService(IGeneroRepository repo)
    {
        _generoRepo = repo;
    }
    public ServiceResult<GeneroDto> Create(CreateGeneroDto dto)
    {
        var nome = (dto.Nome ?? "").Trim();

        var nomeValidation = ValidateNome(nome);

        if (nomeValidation != null)
            return ServiceResult<GeneroDto>.Fail(nomeValidation, 400);

        if (_generoRepo.GeneroNomeExists(nome))
            return ServiceResult<GeneroDto>.Fail("Já existe um gênero com esse nome.", 409);

        var genero = dto.ToGeneroModel();
        genero.Nome = nome;
        _generoRepo.AddGenero(genero);
        return ServiceResult<GeneroDto>.Success(genero.ToGeneroDto(), 201);
    }

    public ServiceResult<object> Delete(int id)
    {
        if (id <= 0)
            return ServiceResult<object>.Fail("Id inválido.", 400);

        if (!_generoRepo.GeneroExists(id))
            return ServiceResult<object>.Fail("Gênero não encontrado.", 404);

        if (_generoRepo.GeneroHasFilmes(id))
            return ServiceResult<object>.Fail("Não é possível excluir um gênero que possui filmes vinculados.", 409);

        _generoRepo.DeleteGenero(id);
        return ServiceResult<object>.Success(new { }, 204);
    }

    public ServiceResult<IEnumerable<GeneroDto>> GetAll()
    {
        var generos = _generoRepo.GetGeneros().Select(g => g.ToGeneroDto());
        return ServiceResult<IEnumerable<GeneroDto>>.Success(generos);
    }

    public ServiceResult<GeneroDto> GetById(int id)
    {
        if (id <= 0)
            return ServiceResult<GeneroDto>.Fail("Id inválido.", 400);

        var genero = _generoRepo.GetGeneroById(id);
        if (genero == null)
            return ServiceResult<GeneroDto>.Fail("Gênero não encontrado.", 404);

        return ServiceResult<GeneroDto>.Success(genero.ToGeneroDto());
    }

    public ServiceResult<GeneroDto> Update(int id, UpdateGeneroDto dto)
    {
        if (id <= 0)
            return ServiceResult<GeneroDto>.Fail("Id inválido.", 400);

        var genero = _generoRepo.GetGeneroById(id);
        if (genero == null)
            return ServiceResult<GeneroDto>.Fail("Gênero não encontrado.", 404);

        var nome = (dto.Nome ?? "").Trim();

        var nomeValidation = ValidateNome(nome);
        if (nomeValidation != null)
            return ServiceResult<GeneroDto>.Fail(nomeValidation, 400);

        if (_generoRepo.GeneroNomeExists(nome, ignoreId: id))
            return ServiceResult<GeneroDto>.Fail("Já existe um gênero com esse nome.", 409);

        dto.UpdateGeneroModel(genero);
        genero.Nome = nome;
        _generoRepo.UpdateGenero(genero);
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