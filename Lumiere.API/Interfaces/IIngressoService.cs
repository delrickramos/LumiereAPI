using Lumiere.API.Dtos.Ingresso;
using Lumiere.API.Common;

namespace Lumiere.API.Interfaces
{
    public interface IIngressoService
    {
        Task<ServiceResult<IEnumerable<IngressoDto>>> GetAllAsync();
        Task<ServiceResult<IngressoDto>> GetByIdAsync(int id);
        Task<ServiceResult<IEnumerable<IngressoDto>>> GetBySessaoAsync(int sessaoId);

        Task<ServiceResult<IngressoDto>> VenderAsync(CreateIngressoDto dto);
    }
}
