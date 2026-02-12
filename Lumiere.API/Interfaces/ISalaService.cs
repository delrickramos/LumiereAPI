using Lumiere.API.Dtos.Sala;
using Lumiere.API.Common;

namespace Lumiere.API.Interfaces
{
    public interface ISalaService
    {
        Task<ServiceResult<IEnumerable<SalaDto>>> GetAllAsync();
        Task<ServiceResult<SalaDto>> GetByIdAsync(int id);
        Task<ServiceResult<SalaDto>> CreateAsync(CreateSalaDto dto);
        Task<ServiceResult<SalaDto>> UpdateAsync(int id, UpdateSalaDto dto);
        Task<ServiceResult<object>> DeleteAsync(int id);
    }
}
