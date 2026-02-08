namespace Lumiere.API.Dtos.TipoIngresso;

public class CreateTipoIngressoDto
{
    public string Nome {get; set;} = string.Empty;
    public decimal DescontoPercentual { get; set; }
}