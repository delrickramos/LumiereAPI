using System.ComponentModel.DataAnnotations;

namespace Lumiere.Models
{
    // Representa um assento em uma sala do cinema
    public class Assento
    {
        public int Id { get; set; }
        public int Coluna { get; set; }

        [MaxLength(5)]
        public string Fileira { get; set; } = string.Empty;

        // Nome formatado como "A1", "B5", etc
        [MaxLength(10)]
        public string Nome { get; set; } = string.Empty;    

        // Tipo define características especiais (Normal, Cadeirante, Obeso)
        [Required]
        public TipoAssentoEnum TipoAssento { get; set; }

        public int SalaId { get; set; }
        public Sala? Sala { get; set; }
        public List<Ingresso>? Ingressos { get; set; }
    }
}
