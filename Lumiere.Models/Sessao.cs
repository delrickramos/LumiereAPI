using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Lumiere.Models
{
    // Representa uma sessão de exibição de filme em uma sala específica
    public class Sessao
    {
        public int Id { get; set; }
        public DateTimeOffset DataHoraInicio { get; set; }
        // DataHoraFim é calculado automaticamente com base na duração do filme
        public DateTimeOffset DataHoraFim { get; set; }

        [MaxLength(20)]
        public string Idioma { get; set; } = string.Empty;

        [Column(TypeName = "decimal(18,2)")]
        public decimal PrecoBase { get; set; }

        public int SalaId { get; set; }
        public int FormatoSessaoId { get; set; }
        public int FilmeId { get; set; }

        public Filme? Filme { get; set; }
        public Sala? Sala { get; set; }
        public FormatoSessao? FormatoSessao { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public List<Ingresso>? Ingressos { get; set; }
    }
}
