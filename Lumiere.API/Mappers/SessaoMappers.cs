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
                Sala_Id = sessao.Sala_Id,
                FormatoSessao_Id = sessao.FormatoSessao_Id,
                Filme_Id = sessao.Filme_Id
            };
        }
    }
}
