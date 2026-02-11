namespace Lumiere.API.Dtos.Relatorio
{
    public class SalaOcupacaoDto
    {
        public int SalaId { get; set; }
        public string Nome { get; set; } = string.Empty;
        public int Capacidade { get; set; }
        public int TotalIngressosVendidos { get; set; }
        public decimal TaxaOcupacao { get; set; }
    }
}