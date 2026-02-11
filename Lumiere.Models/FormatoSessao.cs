using System.ComponentModel.DataAnnotations;

namespace Lumiere.Models
{
    // Representa o formato de exibição da sessão (2D, 3D, IMAX)
    public class FormatoSessao
    {
        public int Id { get; set; }

        [MaxLength(60)]
        public string Nome { get; set; } = string.Empty;

        public List<Sessao>? Sessoes { get; set; }
    }
}
