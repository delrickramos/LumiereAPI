using System.ComponentModel.DataAnnotations;

namespace Lumiere.Models
{
    public class Genero
    {
        public int Id { get; set; }

        [MaxLength(60)]
        public string Nome { get; set; } = string.Empty;

        public List<Filme>? Filmes { get; set; }
    }
}
