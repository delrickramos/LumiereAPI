using Lumiere.API.Dtos.FormatoSessao;
using Lumiere.API.Dtos.Genero;
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
        public static void UpdateFormatoSessaoModel(this FormatoSessaoDto formatoSessaoDto, FormatoSessao formatoSessao)
        {
            formatoSessao.Nome = formatoSessaoDto.Nome;
        }
    }
}
