using System;
using System.Collections.Generic;
using System.Text;

namespace Lumiere.Models
{
    public class Genero
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public ICollection<Filme>? Filmes { get; set; }
    }
}
