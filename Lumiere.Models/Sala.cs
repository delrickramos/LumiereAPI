using System;
using System.Collections.Generic;
using System.Text;

namespace Lumiere.Models
{
    public class Sala
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public int Capacidade { get; set; }
        public string Tipo { get; set; } = string.Empty;
        public ICollection<Sessao>? Sessoes { get; set; }
        public ICollection<Assento>? Assentos { get; set; }
    }
}
