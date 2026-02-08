namespace Lumiere.API.Dtos.TipoIngresso;

public class TipoIngressoDto
{
    public int Id {get; set;}
    public string Nome {get; set;} = string.Empty;
    public decimal DescontoPercentual { get; set; }
}