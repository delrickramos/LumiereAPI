using Lumiere.API.Dtos.TipoIngresso;
using Lumiere.API.Common;

namespace Lumiere.API.Interfaces
{
    public interface ITipoIngressoService
    {
        Task<ServiceResult<IEnumerable<TipoIngressoDto>>> GetAllAsync();
        Task<ServiceResult<TipoIngressoDto>> GetByIdAsync(int id);
        Task<ServiceResult<TipoIngressoDto>> CreateAsync(CreateTipoIngressoDto dto);
        Task<ServiceResult<TipoIngressoDto>> UpdateAsync(int id, UpdateTipoIngressoDto dto);
        Task<ServiceResult<object>> DeleteAsync(int id);
    }
}
