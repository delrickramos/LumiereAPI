using Lumiere.API.Dtos.FormatoSessao;
using Lumiere.API.Interfaces;
using Lumiere.API.Mappers;

namespace Lumiere.API.Services;

public class FormatoSessaoService : IFormatoSessaoService
{

    private readonly IFormatoSessaoRepository _repo;

    private const int NomeMin = 2;
    private const int NomeMax = 60;

    public FormatoSessaoService(IFormatoSessaoRepository repo)
    {
        _repo = repo;
    }

    public ServiceResult<FormatoSessaoDto> Create(CreateFormatoSessaoDto dto)
    {
        var nome = (dto.Nome ?? "").Trim();

        var nomeValidation = ValidateNome(nome);
        if (nomeValidation != null)
            return ServiceResult<FormatoSessaoDto>.Fail(nomeValidation, 400);

        if (_repo.FormatoSessaoNomeExists(nome))
            return ServiceResult<FormatoSessaoDto>.Fail("Já existe um formato de sessão com esse nome.", 409);

        var formato = dto.ToFormatoSessaoModel();
        formato.Nome = nome;
        _repo.AddFormatoSessao(formato);
        return ServiceResult<FormatoSessaoDto>.Success(formato.ToFormatoSessaoDto(), 201);
    }

    public ServiceResult<object> Delete(int id)
    {
        if (id <= 0)
            return ServiceResult<object>.Fail("Id inválido.", 400);

        var formato = _repo.GetFormatoSessaoById(id);
        if (formato == null)
            return ServiceResult<object>.Fail("Formato de sessão não encontrado.", 404);

        if (_repo.FormatoSessaoHasSessoes(id))
            return ServiceResult<object>.Fail("Não é possível excluir um formato de sessão que possui sessões vinculadas.", 409);

        _repo.DeleteFormatoSessao(id);
        return ServiceResult<object>.Success(new { }, 204);
    }

    public ServiceResult<IEnumerable<FormatoSessaoDto>> GetAll()
    {
        var formatos = _repo.GetFormatosSessoes().Select(f => f.ToFormatoSessaoDto());
        return ServiceResult<IEnumerable<FormatoSessaoDto>>.Success(formatos);}

    public ServiceResult<FormatoSessaoDto> GetById(int id)
    {
        if (id <= 0)
            return ServiceResult<FormatoSessaoDto>.Fail("Id inválido.", 400);

        var formato = _repo.GetFormatoSessaoById(id);
        if (formato == null)
            return ServiceResult<FormatoSessaoDto>.Fail("Formato de sessão não encontrado.", 404);

        return ServiceResult<FormatoSessaoDto>.Success(formato.ToFormatoSessaoDto());    
    }

    public ServiceResult<FormatoSessaoDto> Update(int id, UpdateFormatoSessaoDto dto)
    {
        if (id <= 0)
            return ServiceResult<FormatoSessaoDto>.Fail("Id inválido.", 400);

        var formato = _repo.GetFormatoSessaoById(id);
        if (formato == null)
            return ServiceResult<FormatoSessaoDto>.Fail("Formato de sessão não encontrado.", 404);

        var nome = (dto.Nome ?? "").Trim();

        var nomeValidation = ValidateNome(nome);
        if (nomeValidation != null)
            return ServiceResult<FormatoSessaoDto>.Fail(nomeValidation, 400);

        if (_repo.FormatoSessaoNomeExists(nome, ignoreId: id))
            return ServiceResult<FormatoSessaoDto>.Fail("Já existe um formato de sessão com esse nome.", 409);

        dto.UpdateFormatoSessaoModel(formato);
        formato.Nome = nome;
        _repo.UpdateFormatoSessao(formato);
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