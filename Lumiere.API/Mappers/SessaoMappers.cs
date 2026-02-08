using Lumiere.API.Dtos.Sessao;
using Lumiere.Models;

namespace Lumiere.API.Mappers
{
    public static class SessaoMappers
    {
        public static SessaoDto ToSessaoDto(this Sessao sessao)
        {
            return new SessaoDto
            {
                Id = sessao.Id,
                DataHoraInicio = sessao.DataHoraInicio,
                DataHoraFim = sessao.DataHoraFim,
                Idioma = sessao.Idioma,
                PrecoBase = sessao.PrecoBase,
                SalaId = sessao.SalaId,
                FormatoSessaoId = sessao.FormatoSessaoId,
                FilmeId = sessao.FilmeId
            };
        }

        public static Sessao ToSessaoModel(this CreateSessaoDto sessaoDto, int FilmeId)
        {
            return new Sessao
            {
                DataHoraInicio = sessaoDto.DataHoraInicio,
                DataHoraFim = sessaoDto.DataHoraFim,
                Idioma = sessaoDto.Idioma,
                PrecoBase = sessaoDto.PrecoBase,
                SalaId = sessaoDto.SalaId,
                FormatoSessaoId = sessaoDto.FormatoSessaoId,
                FilmeId = FilmeId
            };
        }

        public static void UpdateSessaoModel(this UpdateSessaoDto sessaoDto, Sessao sessao)
        {
            sessao.DataHoraInicio = sessaoDto.DataHoraInicio;
            sessao.DataHoraFim = sessaoDto.DataHoraFim;
            sessao.Idioma = sessaoDto.Idioma;
            sessao.PrecoBase = sessaoDto.PrecoBase;
            sessao.SalaId = sessaoDto.SalaId;
            sessao.FormatoSessaoId = sessaoDto.FormatoSessaoId;
        }
    }
}
