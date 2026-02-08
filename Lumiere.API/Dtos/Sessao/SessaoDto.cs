using Lumiere.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lumiere.API.Dtos.Sessao
{
    public class SessaoDto
    {
        public int Id { get; set; }
        public DateTimeOffset DataHoraInicio { get; set; }
        public DateTimeOffset DataHoraFim { get; set; }
        public string Versao { get; set; } = string.Empty;
        public decimal Preco { get; set; }
        public int Sala_Id { get; set; }
        public int FormatoSessao_Id { get; set; }
        public int Filme_Id { get; set; }
    }
}
