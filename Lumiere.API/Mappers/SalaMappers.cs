using System.Xml;
using Lumiere.API.Dtos.Sala;
using Lumiere.Models;

namespace Lumiere.API.Mappers
{
    public static class SalaMappers
    {
        public static SalaDto ToSalaDto(this Sala sala)
        {
            return new SalaDto
            {
                Id = sala.Id,
                Nome = sala.Nome,
                Capacidade = sala.Capacidade,
                Tipo = sala.Tipo
            };
        }

        public static Sala ToSalaModel(this CreateSalaDto salaDto)
        {
            return new Sala
            {
                Nome = salaDto.Nome,
                NumeroLinhas = salaDto.NumeroLinhas,
                NumeroColunas = salaDto.NumeroColunas,
                Tipo = salaDto.Tipo
            };
        }

        public static void UpdateSalaModel(this UpdateSalaDto salaDto, Sala sala)
        {
            sala.Nome = salaDto.Nome;
            sala.Capacidade = salaDto.Capacidade;
            sala.Tipo = salaDto.Tipo;
        }
    }
}
