    namespace Lumiere.API.Dtos.Filme
{
    public class CreateFilmeDto
    {
        public string Titulo { get; set; } = string.Empty;
        public int DuracaoMinutos { get; set; }
        public string ClassificacaoIndicativa { get; set; } = string.Empty;
        public string Sinopse { get; set; } = string.Empty;
        public string Direcao { get; set; } = string.Empty;
        public string Distribuidora { get; set; } = string.Empty;
        public int Genero_Id { get; set; }
    }
}
