using System;
using System.Collections.Generic;
using System.Text;

namespace Lumiere.Models
{
    public class Filme
    {
        public int Id { get; set; }
        public string? Titulo { get; set; }
        public int DuracaoMinutos { get; set; }
        public string? ClassificacaoIndicativa { get; set; }
        public string? Sinopse { get; set; }
        public string? Direcao { get; set; }
        public string? Distribuidora { get; set; }
        public int Genero_Id { get; set; }
        public ICollection<Sessao>? Sessoes { get; set; }

    }
}
