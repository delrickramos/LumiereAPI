using System;
using System.Collections.Generic;
using System.Text;

namespace Lumiere.Models
{
    public class Sala
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public int NumeroLinhas { get; set; }
        public int NumeroColunas { get; set; }
        public int Capacidade { get; set; }
        public List<Sessao>? Sessoes { get; set; }
        public List<Assento>? Assentos { get; set; }
    }
}
