using Lumiere.Models;

namespace Lumiere.API.Dtos.Filme
{
    public class UpdateFilmeDto
    {
        public string Titulo { get; set; } = string.Empty;
        public ClassificacaoIndicativaEnum ClassificacaoIndicativa { get; set; }
        public string Sinopse { get; set; } = string.Empty;
        public string Direcao { get; set; } = string.Empty;
        public string Distribuidora { get; set; } = string.Empty;
        public int GeneroId { get; set; }
    }
}