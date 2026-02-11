using Lumiere.API.Dtos.FormatoSessao;
using Lumiere.Models;

namespace Lumiere.API.Mappers
{
    public static class FormatoSessaoMappers
    {
        public static FormatoSessaoDto ToFormatoSessaoDto(this FormatoSessao formatoSessao)
        {
            return new FormatoSessaoDto
            {
                Id = formatoSessao.Id,
                Nome = formatoSessao.Nome
            };
        }
        public static FormatoSessao ToFormatoSessaoModel(this CreateFormatoSessaoDto formatoSessaoDto)
        {
            return new FormatoSessao
            {
                Nome = formatoSessaoDto.Nome
            };
        }
        public static void UpdateFormatoSessaoModel(this UpdateFormatoSessaoDto updateFormatoSessaoDto, FormatoSessao formatoSessao)
        {
            formatoSessao.Nome = updateFormatoSessaoDto.Nome;
        }
    }
}
