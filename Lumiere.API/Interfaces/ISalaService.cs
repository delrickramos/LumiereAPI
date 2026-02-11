using Lumiere.API.Dtos.Sala;
using Lumiere.API.Common;

namespace Lumiere.API.Interfaces
{
    public interface ISalaService
    {
        ServiceResult<IEnumerable<SalaDto>> GetAll();
        ServiceResult<SalaDto> GetById(int id);
        ServiceResult<SalaDto> Create(CreateSalaDto dto);
        ServiceResult<SalaDto> Update(int id, UpdateSalaDto dto);
        ServiceResult<object> Delete(int id);
    }
}
