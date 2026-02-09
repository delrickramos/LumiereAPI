namespace Lumiere.API.Dtos.Sessao
{
    public class CreateSessaoDto
    {
        public DateTimeOffset DataHoraInicio { get; set; }
        public string Idioma { get; set; } = string.Empty;
        public decimal PrecoBase { get; set; }
        public int SalaId { get; set; }
        public int FormatoSessaoId { get; set; }
        public int FilmeId { get; set; }
    }
}
