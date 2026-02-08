using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Lumiere.Models
{
    public class Assento
    {
        public int Id { get; set; }
        public int Coluna { get; set; }
        public string Fileira { get; set; } = string.Empty;
    
        [Required]
        public TipoAssentoEnum TipoAssento { get; set; }
        
        public int SalaId { get; set; }
        public Sala? Sala { get; set; }
        public List<Ingresso>? Ingressos { get; set; }
    }
}
