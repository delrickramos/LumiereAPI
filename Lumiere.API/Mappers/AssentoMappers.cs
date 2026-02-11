using Lumiere.API.Dtos.Assento;
using Lumiere.Models;

namespace Lumiere.API.Mappers
{
    public static class AssentoMappers
    {
        public static AssentoDto ToAssentoDto(this Assento assento)
        {
            return new AssentoDto
            {
                Id = assento.Id,
                Nome = assento.Nome,    
                Coluna = assento.Coluna,
                Fileira = assento.Fileira,
                TipoAssento = assento.TipoAssento,
                SalaId = assento.SalaId
            };
        }

        public static Assento ToAssentoModel(this CreateAssentoDto assentoDto)
        {
            return new Assento
            {
                Coluna = assentoDto.Coluna,
                Fileira = assentoDto.Fileira,
                TipoAssento = assentoDto.TipoAssento,
                SalaId = assentoDto.SalaId
            };
        }
    }
}