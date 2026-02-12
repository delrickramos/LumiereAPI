using Lumiere.API.Dtos.FormatoSessao;
using Lumiere.API.Common;

namespace Lumiere.API.Interfaces
{
    public interface IFormatoSessaoService
    {
        Task<ServiceResult<FormatoSessaoDto>> CreateAsync(CreateFormatoSessaoDto dto);
        Task<ServiceResult<FormatoSessaoDto>> UpdateAsync(int id, UpdateFormatoSessaoDto dto);
        Task<ServiceResult<FormatoSessaoDto>> GetByIdAsync(int id);
        Task<ServiceResult<IEnumerable<FormatoSessaoDto>>> GetAllAsync();
        Task<ServiceResult<object>> DeleteAsync(int id);
    }
}
