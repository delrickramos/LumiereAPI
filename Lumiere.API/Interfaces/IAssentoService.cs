using Lumiere.API.Dtos.Assento;
using Lumiere.API.Common;

namespace Lumiere.API.Interfaces
{
    public interface IAssentoService
    {
        Task<ServiceResult<AssentoDto>> GetByIdAsync(int id);
        Task<ServiceResult<IEnumerable<AssentoDto>>> GetBySalaAsync(int salaId);
        Task<ServiceResult<AssentoDto>> CreateAsync(CreateAssentoDto dto);
    }
}
