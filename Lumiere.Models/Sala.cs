using System.ComponentModel.DataAnnotations;

namespace Lumiere.Models
{
    // Representa uma sala de exibição do cinema
    public class Sala
    {
        public int Id { get; set; }

        [MaxLength(60)]
        public string Nome { get; set; } = string.Empty;

        public int NumeroLinhas { get; set; }
        public int NumeroColunas { get; set; }
        // Capacidade total calculada como NumeroLinhas * NumeroColunas
        public int Capacidade { get; set; }
        public List<Sessao>? Sessoes { get; set; }
        public List<Assento>? Assentos { get; set; }
    }
}
