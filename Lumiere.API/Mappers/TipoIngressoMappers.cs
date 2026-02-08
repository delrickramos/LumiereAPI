using Lumiere.API.Dtos.TipoIngresso;
using Lumiere.Models;


namespace Lumiere.API.Mappers;

public static class TipoIngressoMappers 
{
        public static TipoIngressoDto ToTipoIngressoDto(this TipoIngresso TipoIngresso)
        {
            return new TipoIngressoDto
            {
                Id = TipoIngresso.Id,
                Nome = TipoIngresso.Nome,
                DescontoPercentual = TipoIngresso.DescontoPercentual
            };
        }
        public static TipoIngresso ToTipoIngressoModel(this CreateTipoIngressoDto TipoIngressoDto)
        {
            return new TipoIngresso
            {
                Nome = TipoIngressoDto.Nome,
                DescontoPercentual = TipoIngressoDto.DescontoPercentual
            };
        }
        public static void UpdateTipoIngressoModel(this TipoIngressoDto TipoIngressoDto, TipoIngresso TipoIngresso)
        {
            TipoIngresso.Nome = TipoIngressoDto.Nome;
            TipoIngresso.DescontoPercentual = TipoIngressoDto.DescontoPercentual;
        }
    }