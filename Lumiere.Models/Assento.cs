using System.ComponentModel.DataAnnotations;

namespace Lumiere.Models
{
    public class Assento
    {
        public int Id { get; set; }
        public int Coluna { get; set; }

        [MaxLength(5)]
        public string Fileira { get; set; } = string.Empty;

        [MaxLength(10)]
        public string Nome { get; set; } = string.Empty;    

        [Required]
        public TipoAssentoEnum TipoAssento { get; set; }
        
        public int SalaId { get; set; }
        public Sala? Sala { get; set; }
        public List<Ingresso>? Ingressos { get; set; }
    }
}
