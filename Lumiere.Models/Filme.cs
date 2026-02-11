using System.ComponentModel.DataAnnotations;


namespace Lumiere.Models
{
    // Representa um filme exibido no cinema
    public class Filme
    {
        public int Id { get; set; }

        [MaxLength(150)]
        public string Titulo { get; set; } = string.Empty;

        public int DuracaoMinutos { get; set; }
        [Required]
        public ClassificacaoIndicativaEnum ClassificacaoIndicativa { get; set; }

        [MaxLength(2000)]
        public string Sinopse { get; set; } = string.Empty;

        [MaxLength(120)]
        public string Direcao { get; set; } = string.Empty;

        [MaxLength(150)]
        public string Distribuidora { get; set; } = string.Empty;

        public int GeneroId { get; set; }
        // Relacionamento: um filme pode ter várias sessões
        public List<Sessao>? Sessoes { get; set; }
        public Genero? Genero { get; set; }

    }
}
