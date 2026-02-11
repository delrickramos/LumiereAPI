using Lumiere.API.Dtos.Sessao;
using Lumiere.Models;
using Newtonsoft.Json;

namespace Lumiere.API.Dtos.Filme
{
    public class FilmeDto
    {
        public int Id { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public int DuracaoMinutos { get; set; }
        public ClassificacaoIndicativaEnum ClassificacaoIndicativa { get; set; }
        public string Sinopse { get; set; } = string.Empty;
        public string Direcao { get; set; } = string.Empty;
        public string Distribuidora { get; set; } = string.Empty;
        public int GeneroId { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<SessaoDto>? Sessoes { get; set; }

    }
}
