using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Lumiere.Models
{
    public class Sessao
    {
        public int Id { get; set; }
        public DateTimeOffset DataHoraInicio { get; set; }
        public DateTimeOffset DataHoraFim { get; set; }
        public string Idioma { get; set; } = string.Empty;
        [Column(TypeName = "decimal(18,2)")]
        public decimal PrecoBase { get; set; }
        public int SalaId { get; set; }
        public int FormatoSessaoId { get; set; }
        public int FilmeId { get; set; }

        public Filme? Filme { get; set; }
        public Sala? Sala { get; set; }
        public FormatoSessao? FormatoSessao { get; set; }
        public List<Ingresso>? Ingressos { get; set; }
    }
}
