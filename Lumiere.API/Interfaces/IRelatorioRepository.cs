using Lumiere.API.Dtos.Relatorio;

namespace Lumiere.API.Interfaces
{
    public interface IRelatorioRepository
    {
        List<SalaOcupacaoDto> GetTaxaOcupacaoSalas();
    }
}