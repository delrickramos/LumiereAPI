using Lumiere.API.Dtos.Sessao;
using Lumiere.API.Common;

namespace Lumiere.API.Interfaces;

public interface ISessaoService
{
    Task<ServiceResult<IEnumerable<SessaoDto>>> GetAllAsync();
    Task<ServiceResult<SessaoDto>> GetByIdAsync(int id);
    Task<ServiceResult<SessaoDto>> CreateAsync(CreateSessaoDto dto);
    Task<ServiceResult<SessaoDto>> UpdateAsync(int id, UpdateSessaoDto dto);
    Task<ServiceResult<object>> DeleteAsync(int id);
}
