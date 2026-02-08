using Lumiere.API.Dtos.Genero;
using Lumiere.Models;

namespace Lumiere.API.Mappers
{
    public static class GeneroMappers
    {
        public static GeneroDto ToGeneroDto(this Genero genero)
        {
            return new GeneroDto
            {
                Id = genero.Id,
                Nome = genero.Nome
            };
        }

        public static Genero ToGeneroModel(this CreateGeneroDto generoDto)
        {
            return new Genero
            {
                Nome = generoDto.Nome
            };
        }

        public static void UpdateGeneroModel(this UpdateGeneroDto generoDto, Genero genero)
        {
            genero.Nome = generoDto.Nome;
        }
    }
}