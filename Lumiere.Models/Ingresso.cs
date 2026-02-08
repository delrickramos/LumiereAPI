using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata;
using System.Text;

namespace Lumiere.Models
{
    public class Ingresso
    {
        public int Id { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal PrecoFinal { get; set; }
        public DateTimeOffset ExpiraEm { get; set; }
        public string Status { get; set; } = string.Empty;
        public int SessaoId { get; set; }
        public int AssentoId { get; set; }
        public int TipoIngressoId { get; set; }
        public Sessao? Sessao { get; set; }
        public TipoIngresso? TipoIngresso { get; set; }
        public Assento? Assento { get; set; }
    }
}
