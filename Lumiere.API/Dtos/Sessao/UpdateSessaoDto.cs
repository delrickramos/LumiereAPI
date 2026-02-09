namespace Lumiere.API.Dtos.Sessao
{
    public class UpdateSessaoDto
    {
        public DateTimeOffset DataHoraInicio { get; set; }
        public string Idioma { get; set; } = string.Empty;
        public decimal PrecoBase { get; set; }
        public int SalaId { get; set; }
        public int FormatoSessaoId { get; set; }
    }
}   