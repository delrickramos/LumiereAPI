namespace Lumiere.API.Dtos.Sala
{
    public class CreateSalaDto
    {
        public string Nome { get; set; } = string.Empty;
        public int NumeroLinhas { get; set; }
        public int NumeroColunas { get; set; }
    }
}