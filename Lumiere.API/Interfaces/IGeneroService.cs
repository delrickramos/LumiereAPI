using Lumiere.API.Dtos.Genero;
using Lumiere.API.Common;

namespace Lumiere.API.Interfaces
{
    public interface IGeneroService
    {
        Task<ServiceResult<IEnumerable<GeneroDto>>> GetAllAsync();
        Task<ServiceResult<GeneroDto>> GetByIdAsync(int id);
        Task<ServiceResult<GeneroDto>> CreateAsync(CreateGeneroDto dto);
        Task<ServiceResult<GeneroDto>> UpdateAsync(int id, UpdateGeneroDto dto);
        Task<ServiceResult<object>> DeleteAsync(int id);
    }
}
