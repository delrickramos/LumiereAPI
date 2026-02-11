using Lumiere.API.Dtos.Ingresso;
using Lumiere.Models;

namespace Lumiere.API.Mappers
{
    public static class IngressoMappers
    {
        public static IngressoDto ToIngressoDto(this Ingresso ingresso)
        {
            return new IngressoDto
            {
                Id = ingresso.Id,
                PrecoFinal = ingresso.PrecoFinal,
                Status = ingresso.Status,
                SessaoId = ingresso.SessaoId,
                AssentoId = ingresso.AssentoId,
                TipoIngressoId = ingresso.TipoIngressoId
            };
        }
    }
}