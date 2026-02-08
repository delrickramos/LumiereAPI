using Lumiere.Models;

namespace Lumiere.API.Dtos.Sala
{
    public class SalaDto
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public int Capacidade { get; set; }
        public string Tipo { get; set; } = string.Empty;
        //public List<Sessao>? Sessoes { get; set; }
        //public List<Assento>? Assentos { get; set; }
    }
}
