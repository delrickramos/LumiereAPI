namespace Lumiere.API.Dtos.TipoIngresso;

public class UpdateTipoIngressoDto
{
    public string Nome {get; set;} = string.Empty;
    public decimal DescontoPercentual { get; set; }
}