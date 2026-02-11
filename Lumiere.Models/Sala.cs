using System.ComponentModel.DataAnnotations;

namespace Lumiere.Models
{
    public class Sala
    {
        public int Id { get; set; }

        [MaxLength(60)]
        public string Nome { get; set; } = string.Empty;

        public int NumeroLinhas { get; set; }
        public int NumeroColunas { get; set; }
        public int Capacidade { get; set; }
        public List<Sessao>? Sessoes { get; set; }
        public List<Assento>? Assentos { get; set; }
    }
}
