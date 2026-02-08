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
                Versao = sessao.Versao,
                Preco = sessao.Preco,
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
                Versao = sessaoDto.Versao,
                Preco = sessaoDto.Preco,
                //SessaoId = sessao.SessaoId,
                //FormatoSessaoId = sessao.FormatoSessaoId,
                FilmeId = FilmeId
            };
        }
    }
}
