using System;
using System.Collections.Generic;
using System.Text;

namespace Lumiere.Models
{
    public class FormatoSessao
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public List<Sessao>? Sessoes { get; set; }
    }
}
