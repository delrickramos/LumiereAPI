using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Lumiere.Models
{
    // Representa tipos de ingresso com diferentes descontos (Inteira, Meia, Estudante, Idoso, Criança)
    public class TipoIngresso
    {
        public int Id { get; set; }

        [MaxLength(60)]
        public string Nome { get; set; } = string.Empty;

        // Percentual de desconto aplicado ao preço base (0 a 100)
        [Column(TypeName = "decimal(5,2)")]
        public decimal DescontoPercentual { get; set; }

        public List<Ingresso>? Ingressos { get; set; }
    }
}
