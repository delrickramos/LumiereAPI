using Lumiere.API.Dtos.Sessao;

namespace Lumiere.API.Dtos.Filme
{
    public class FilmeDto
    {
        public int Id { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public int DuracaoMinutos { get; set; }
        public string ClassificacaoIndicativa { get; set; } = string.Empty;
        public string Sinopse { get; set; } = string.Empty;
        public string Direcao { get; set; } = string.Empty;
        public string Distribuidora { get; set; } = string.Empty;
        public int GeneroId { get; set; }
        public List<SessaoDto>? Sessoes { get; set; }

    }
}
