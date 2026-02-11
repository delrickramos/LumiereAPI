using System;
using System.Collections.Generic;
using System.Text;

namespace Lumiere.Models
{
    public class Filme
    {
        public int Id { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public int DuracaoMinutos { get; set; }
        public ClassificacaoIndicativaEnum ClassificacaoIndicativa { get; set; }
        public string Sinopse { get; set; } = string.Empty;
        public string Direcao { get; set; } = string.Empty;
        public string Distribuidora { get; set; } = string.Empty;
        public int GeneroId { get; set; }
        public List<Sessao>? Sessoes { get; set; }
        public Genero? Genero { get; set; }

    }
}
