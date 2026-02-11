using Lumiere.API.Dtos.Filme;
using Lumiere.Models;

namespace Lumiere.API.Mappers
{
    public static class FilmeMappers
    {
        public static FilmeDto ToFilmeDto(this Filme filme)
        {
            return new FilmeDto
            {
                Id = filme.Id,
                Titulo = filme.Titulo,
                DuracaoMinutos = filme.DuracaoMinutos,
                ClassificacaoIndicativa = filme.ClassificacaoIndicativa,
                Sinopse = filme.Sinopse,
                Direcao = filme.Direcao,
                Distribuidora = filme.Distribuidora,
                GeneroId = filme.GeneroId,
                Sessoes = filme.Sessoes?.Select(s => s.ToSessaoDto()).ToList()
            };
        }
        public static Filme ToFilmeModel(this CreateFilmeDto filmeDto)
        {
            return new Filme
            {
                Titulo = filmeDto.Titulo,
                DuracaoMinutos = filmeDto.DuracaoMinutos,
                ClassificacaoIndicativa = filmeDto.ClassificacaoIndicativa,
                Sinopse = filmeDto.Sinopse,
                Direcao = filmeDto.Direcao,
                Distribuidora = filmeDto.Distribuidora,
                GeneroId = filmeDto.GeneroId
            };
        }
        public static void UpdateFilmeModel(this UpdateFilmeDto filmeDto, Filme filme)
        {
            filme.Titulo = filmeDto.Titulo;
            filme.ClassificacaoIndicativa = filmeDto.ClassificacaoIndicativa;
            filme.Sinopse = filmeDto.Sinopse;
            filme.Direcao = filmeDto.Direcao;
            filme.Distribuidora = filmeDto.Distribuidora;
            filme.GeneroId = filmeDto.GeneroId;
        }
    }
}
