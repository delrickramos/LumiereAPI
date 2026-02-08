namespace Lumiere.API.Dtos.Sala
{
    public class UpdateSalaDto
    {
        public string Nome { get; set; } = string.Empty;
        public int Capacidade { get; set; }
        public string Tipo { get; set; } = string.Empty;
    }
}