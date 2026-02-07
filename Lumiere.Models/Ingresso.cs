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
        public int Sessao_Id { get; set; }
        public int Assento_Id { get; set; }
        public int TipoIngresso_Id { get; set; }
        public Sessao? Sessao { get; set; }
        public TipoIngresso? TipoIngresso { get; set; }
        public Assento? Assento { get; set; }
    }
}
