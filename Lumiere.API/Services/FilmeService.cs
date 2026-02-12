using Lumiere.API.Dtos.Filme;
using Lumiere.API.Interfaces;
using Lumiere.API.Mappers;
using Lumiere.Models;
using Lumiere.API.Common;

namespace Lumiere.API.Services
{
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

        public async Task<ServiceResult<IEnumerable<FilmeDto>>> GetAllAsync()
        {
            var filmes = (await _repo.GetFilmesAsync()).Select(f => f.ToFilmeDto());
            return ServiceResult<IEnumerable<FilmeDto>>.Success(filmes);
        }

        public async Task<ServiceResult<FilmeDto>> GetByIdAsync(int id)
        {
            if (id <= 0)
                return ServiceResult<FilmeDto>.Fail("Id inválido.", 400);

            var filme = await _repo.GetFilmeByIdWithSessoesAsync(id);
            if (filme == null)
                return ServiceResult<FilmeDto>.Fail("Filme não encontrado.", 404);

            return ServiceResult<FilmeDto>.Success(filme.ToFilmeDto());
        }

        public async Task<ServiceResult<IEnumerable<FilmeDto>>> GetEmCartazAsync()
        {
            var inicio = DateTime.Now;
            var fim = inicio.AddDays(7);

            var filmes = (await _repo
                .GetFilmesEmCartazAsync(inicio, fim))
                .Select(f => f.ToFilmeDto());

            return ServiceResult<IEnumerable<FilmeDto>>.Success(filmes);
        }

        public async Task<ServiceResult<FilmeDto>> CreateAsync(CreateFilmeDto dto)
        {
            var titulo = (dto.Titulo ?? "").Trim();
            var sinopse = (dto.Sinopse ?? "").Trim();
            var direcao = (dto.Direcao ?? "").Trim();
            var distribuidora = (dto.Distribuidora ?? "").Trim();

            var tituloVal = ValidateTexto("Título", titulo, TituloMin, TituloMax);
            if (tituloVal != null) return ServiceResult<FilmeDto>.Fail(tituloVal, 400);

            var sinopseVal = ValidateTexto("Sinopse", sinopse, SinopseMin, SinopseMax);
            if (sinopseVal != null) return ServiceResult<FilmeDto>.Fail(sinopseVal, 400);

            var direcaoVal = ValidateTexto("Direção", direcao, NomePessoaMin, NomePessoaMax);
            if (direcaoVal != null) return ServiceResult<FilmeDto>.Fail(direcaoVal, 400);

            var distVal = ValidateTexto("Distribuidora", distribuidora, NomePessoaMin, NomePessoaMax);
            if (distVal != null) return ServiceResult<FilmeDto>.Fail(distVal, 400);

            var durVal = ValidateDuracao(dto.DuracaoMinutos);
            if (durVal != null) return ServiceResult<FilmeDto>.Fail(durVal, 400);

            if (dto.GeneroId <= 0)
                return ServiceResult<FilmeDto>.Fail("GeneroId inválido.", 400);

            if (!await _generoRepo.GeneroExistsAsync(dto.GeneroId))
                return ServiceResult<FilmeDto>.Fail("Gênero não encontrado.", 404);

            if (await _repo.FilmeTituloExistsAsync(titulo))
                return ServiceResult<FilmeDto>.Fail("Já existe um filme com esse título.", 409);

            if (!Enum.IsDefined(typeof(ClassificacaoIndicativaEnum), dto.ClassificacaoIndicativa))
                return ServiceResult<FilmeDto>.Fail("Classificação indicativa inválida.", 400);

            var filme = dto.ToFilmeModel();
            filme.Titulo = titulo;
            filme.Sinopse = sinopse;
            filme.Direcao = direcao;
            filme.Distribuidora = distribuidora;

            await _repo.AddFilmeAsync(filme);

            return ServiceResult<FilmeDto>.Success(filme.ToFilmeDto(), 201);
        }

        public async Task<ServiceResult<FilmeDto>> UpdateAsync(int id, UpdateFilmeDto dto)
        {
            if (id <= 0)
                return ServiceResult<FilmeDto>.Fail("Id inválido.", 400);

            var filme = await _repo.GetFilmeByIdAsync(id);
            if (filme == null)
                return ServiceResult<FilmeDto>.Fail("Filme não encontrado.", 404);

            var titulo = (dto.Titulo ?? "").Trim();
            var sinopse = (dto.Sinopse ?? "").Trim();
            var direcao = (dto.Direcao ?? "").Trim();
            var distribuidora = (dto.Distribuidora ?? "").Trim();

            var tituloVal = ValidateTexto("Título", titulo, TituloMin, TituloMax);
            if (tituloVal != null) return ServiceResult<FilmeDto>.Fail(tituloVal, 400);

            var sinopseVal = ValidateTexto("Sinopse", sinopse, SinopseMin, SinopseMax);
            if (sinopseVal != null) return ServiceResult<FilmeDto>.Fail(sinopseVal, 400);

            var direcaoVal = ValidateTexto("Direção", direcao, NomePessoaMin, NomePessoaMax);
            if (direcaoVal != null) return ServiceResult<FilmeDto>.Fail(direcaoVal, 400);

            var distVal = ValidateTexto("Distribuidora", distribuidora, NomePessoaMin, NomePessoaMax);
            if (distVal != null) return ServiceResult<FilmeDto>.Fail(distVal, 400);

            if (dto.GeneroId <= 0)
                return ServiceResult<FilmeDto>.Fail("GeneroId inválido.", 400);

            if (!await _generoRepo.GeneroExistsAsync(dto.GeneroId))
                return ServiceResult<FilmeDto>.Fail("Gênero não encontrado.", 404);

            if (await _repo.FilmeTituloExistsAsync(titulo, ignoreId: id))
                return ServiceResult<FilmeDto>.Fail("Já existe um filme com esse título.", 409);

            if (!Enum.IsDefined(typeof(ClassificacaoIndicativaEnum), dto.ClassificacaoIndicativa))
                return ServiceResult<FilmeDto>.Fail("Classificação indicativa inválida.", 400);

            dto.UpdateFilmeModel(filme);

            filme.Titulo = titulo;
            filme.Sinopse = sinopse;
            filme.Direcao = direcao;
            filme.Distribuidora = distribuidora;

            await _repo.UpdateFilmeAsync(filme);
            return ServiceResult<FilmeDto>.Success(filme.ToFilmeDto());
        }

        public async Task<ServiceResult<object>> DeleteAsync(int id)
        {
            if (id <= 0)
                return ServiceResult<object>.Fail("Id inválido.", 400);

            var filme = await _repo.GetFilmeByIdAsync(id);
            if (filme == null)
                return ServiceResult<object>.Fail("Filme não encontrado.", 404);

            // Não permite excluir filme que possui sessões vinculadas
            if (await _repo.FilmeHasSessoesAsync(id))
                return ServiceResult<object>.Fail("Não é possível excluir um filme que possui sessões vinculadas.", 409);

            await _repo.DeleteFilmeAsync(id);
            return ServiceResult<object>.Success(new { }, 204);
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
    }
}

