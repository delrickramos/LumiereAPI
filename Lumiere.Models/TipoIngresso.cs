using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Lumiere.Models
{
    public class TipoIngresso
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;

        [Column(TypeName = "decimal(5,2)")]
        public decimal DescontoPercentual { get; set; }
        public List<Ingresso>? Ingressos { get; set; }
    }
}
