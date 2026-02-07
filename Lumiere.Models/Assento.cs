using System;
using System.Collections.Generic;
using System.Text;

namespace Lumiere.Models
{
    public class Assento
    {
        public int Id { get; set; }
        public int Coluna { get; set; }
        public string Fileira { get; set; } = string.Empty;
        public string TipoAssento { get; set; } = string.Empty;
        public int Sala_Id { get; set; }
        public Sala? Sala { get; set; }
        public ICollection<Ingresso>? Ingressos { get; set; }
    }
}
