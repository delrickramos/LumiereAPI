using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lumiere.Models
{
    public class Ingresso
    {
        public int Id { get; set; }
        
        [Column(TypeName = "decimal(18,2)")]
        public decimal PrecoFinal { get; set; }

        [Required]
        public StatusIngressoEnum Status { get; set; }

        public int SessaoId { get; set; }
        public int AssentoId { get; set; }
        public int TipoIngressoId { get; set; }
        public Sessao? Sessao { get; set; }
        public TipoIngresso? TipoIngresso { get; set; }
        public Assento? Assento { get; set; }
    }
}
