using Lumiere.API.Dtos.Filme;
using Lumiere.API.Interfaces;
using Lumiere.API.Mappers;

namespace Lumiere.API.Services;
public class FilmeService : IFilmeService
{
    private readonly IFilmeRepository _repo;
    private readonly IGeneroRepository _generoRepo;

    private const int TituloMin = 2;
    private const int TituloMax = 120;

    private const int SinopseMin = 10;
    private const int SinopseMax = 2000;

    private const int NomePessoaMin = 2;
    private const int NomePessoaMax = 120;

    private const int DuracaoMin = 1;
    private const int DuracaoMax = 600;

    public FilmeService(IFilmeRepository repo, IGeneroRepository generoRepo)
    {
        _repo = repo;
        _generoRepo = generoRepo;
    }

    public ServiceResult<IEnumerable<FilmeDto>> GetAll()
    {
        var filmes = _repo.GetFilmes().Select(f => f.ToFilmeDto());
        return ServiceResult<IEnumerable<FilmeDto>>.Success(filmes);
    }

    public ServiceResult<FilmeDto> GetById(int id)
    {
        if (id <= 0)
            return ServiceResult<FilmeDto>.Fail("Id inválido.");

        var filme = _repo.GetFilmeByIdWithSessoes(id);
        if (filme == null)
            return ServiceResult<FilmeDto>.Fail("Filme não encontrado.");

        return ServiceResult<FilmeDto>.Success(filme.ToFilmeDto());
    }

    public ServiceResult<FilmeDto> Create(CreateFilmeDto dto)
    {
        var titulo = (dto.Titulo ?? "").Trim();
        var sinopse = (dto.Sinopse ?? "").Trim();
        var direcao = (dto.Direcao ?? "").Trim();
        var distribuidora = (dto.Distribuidora ?? "").Trim();
        var classificacao = (dto.ClassificacaoIndicativa ?? "").Trim();

        var tituloVal = ValidateTexto("Título", titulo, TituloMin, TituloMax);
        if (tituloVal != null) return ServiceResult<FilmeDto>.Fail(tituloVal);

        var sinopseVal = ValidateTexto("Sinopse", sinopse, SinopseMin, SinopseMax);
        if (sinopseVal != null) return ServiceResult<FilmeDto>.Fail(sinopseVal);

        var direcaoVal = ValidateTexto("Direção", direcao, NomePessoaMin, NomePessoaMax);
        if (direcaoVal != null) return ServiceResult<FilmeDto>.Fail(direcaoVal);

        var distVal = ValidateTexto("Distribuidora", distribuidora, NomePessoaMin, NomePessoaMax);
        if (distVal != null) return ServiceResult<FilmeDto>.Fail(distVal);

        var classVal = ValidateClassificacao(classificacao);
        if (classVal != null) return ServiceResult<FilmeDto>.Fail(classVal);

        var durVal = ValidateDuracao(dto.DuracaoMinutos);
        if (durVal != null) return ServiceResult<FilmeDto>.Fail(durVal);

        if (dto.GeneroId <= 0)
            return ServiceResult<FilmeDto>.Fail("GeneroId inválido.");

        if (!_generoRepo.GeneroExists(dto.GeneroId))
            return ServiceResult<FilmeDto>.Fail("Gênero não encontrado.");

        if (_repo.FilmeTituloExists(titulo))
            return ServiceResult<FilmeDto>.Fail("Já existe um filme com esse título.");

        var filme = dto.ToFilmeModel();
        filme.Titulo = titulo;
        filme.Sinopse = sinopse;
        filme.Direcao = direcao;
        filme.Distribuidora = distribuidora;
        filme.ClassificacaoIndicativa = classificacao;

        _repo.AddFilme(filme);

        return ServiceResult<FilmeDto>.Success(filme.ToFilmeDto());
    }

    public ServiceResult<FilmeDto> Update(int id, UpdateFilmeDto dto)
    {
        if (id <= 0)
            return ServiceResult<FilmeDto>.Fail("Id inválido.");

        var filme = _repo.GetFilmeById(id);
        if (filme == null)
            return ServiceResult<FilmeDto>.Fail("Filme não encontrado.");

        var titulo = (dto.Titulo ?? "").Trim();
        var sinopse = (dto.Sinopse ?? "").Trim();
        var direcao = (dto.Direcao ?? "").Trim();
        var distribuidora = (dto.Distribuidora ?? "").Trim();
        var classificacao = (dto.ClassificacaoIndicativa ?? "").Trim();

        var tituloVal = ValidateTexto("Título", titulo, TituloMin, TituloMax);
        if (tituloVal != null) return ServiceResult<FilmeDto>.Fail(tituloVal);

        var sinopseVal = ValidateTexto("Sinopse", sinopse, SinopseMin, SinopseMax);
        if (sinopseVal != null) return ServiceResult<FilmeDto>.Fail(sinopseVal);

        var direcaoVal = ValidateTexto("Direção", direcao, NomePessoaMin, NomePessoaMax);
        if (direcaoVal != null) return ServiceResult<FilmeDto>.Fail(direcaoVal);

        var distVal = ValidateTexto("Distribuidora", distribuidora, NomePessoaMin, NomePessoaMax);
        if (distVal != null) return ServiceResult<FilmeDto>.Fail(distVal);

        var classVal = ValidateClassificacao(classificacao);
        if (classVal != null) return ServiceResult<FilmeDto>.Fail(classVal);

        var durVal = ValidateDuracao(dto.DuracaoMinutos);
        if (durVal != null) return ServiceResult<FilmeDto>.Fail(durVal);

        if (dto.GeneroId <= 0)
            return ServiceResult<FilmeDto>.Fail("GeneroId inválido.");

        if (!_generoRepo.GeneroExists(dto.GeneroId))
            return ServiceResult<FilmeDto>.Fail("Gênero não encontrado.");

        if (_repo.FilmeTituloExists(titulo, ignoreId: id))
            return ServiceResult<FilmeDto>.Fail("Já existe um filme com esse título.");

        dto.UpdateFilmeModel(filme);

        filme.Titulo = titulo;
        filme.Sinopse = sinopse;
        filme.Direcao = direcao;
        filme.Distribuidora = distribuidora;
        filme.ClassificacaoIndicativa = classificacao;

        _repo.UpdateFilme(filme);
        return ServiceResult<FilmeDto>.Success(filme.ToFilmeDto());
    }

    public ServiceResult<object> Delete(int id)
    {
        if (id <= 0)
            return ServiceResult<object>.Fail("Id inválido.");

        var filme = _repo.GetFilmeById(id);
        if (filme == null)
            return ServiceResult<object>.Fail("Filme não encontrado.");

        if (_repo.FilmeHasSessoes(id))
            return ServiceResult<object>.Fail("Não é possível excluir um filme que possui sessões vinculadas.");

        _repo.DeleteFilme(id);
        return ServiceResult<object>.Success(new { });
    }

    private static string? ValidateTexto(string campo, string valor, int min, int max)
    {
        if (string.IsNullOrWhiteSpace(valor))
            return $"{campo} é obrigatório.";

        if (valor.Length < min || valor.Length > max)
            return $"{campo} deve ter entre {min} e {max} caracteres.";

        return null;
    }

    private static string? ValidateDuracao(int duracaoMinutos)
    {
        if (duracaoMinutos < DuracaoMin || duracaoMinutos > DuracaoMax)
            return $"DuraçãoMinutos deve estar entre {DuracaoMin} e {DuracaoMax}.";
        return null;
    }

    private static string? ValidateClassificacao(string classificacao)
    {
        if (string.IsNullOrWhiteSpace(classificacao))
            return "ClassificaçãoIndicativa é obrigatória.";

        if (classificacao.Length > 10)
            return "ClassificaçãoIndicativa inválida.";

        return null;
    }
}
