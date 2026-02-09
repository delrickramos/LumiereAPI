using Lumiere.Models;

namespace Lumiere.API.Dtos.Ingresso
{
    public class IngressoDto
    {
        public int Id { get; set; }
        public decimal PrecoFinal { get; set; }
        public DateTimeOffset ExpiraEm { get; set; }
        public StatusIngressoEnum Status { get; set; }
        public int SessaoId { get; set; }
        public int AssentoId { get; set; }
        public int TipoIngressoId { get; set; }
    }
}