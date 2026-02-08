namespace Lumiere.API.Dtos.Sessao
{
    public class CreateSessaoDto
    {
        public DateTimeOffset DataHoraInicio { get; set; }
        public DateTimeOffset DataHoraFim { get; set; }
        public string Versao { get; set; } = string.Empty;
        public decimal Preco { get; set; }

    }
}
