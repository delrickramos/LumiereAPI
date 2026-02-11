using Lumiere.API.Dtos.Assento;
using Lumiere.API.Common;

namespace Lumiere.API.Interfaces
{
    public interface IAssentoService
    {
        ServiceResult<AssentoDto> GetById(int id);
        ServiceResult<IEnumerable<AssentoDto>> GetBySala(int salaId);
        ServiceResult<AssentoDto> Create(CreateAssentoDto dto);
    }
}
