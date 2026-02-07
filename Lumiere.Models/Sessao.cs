using System;
using System.Collections.Generic;
using System.Text;

namespace Lumiere.Models
{
    public class Sessao
    {
        public int Id { get; set; }
        public DateTimeOffset DataHoraInicio { get; set; }
        public DateTimeOffset DataHoraFim { get; set; }
        public string? Versao { get; set; }
        public decimal Preco { get; set; }
        public int Sala_Id { get; set; }
        public int FormatoSessao_Id { get; set; }
        public int Filme_Id { get; set; }

        public Filme? Filme { get; set; }
    }
}
