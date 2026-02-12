using Lumiere.API.Dtos.Filme;
using Lumiere.API.Common;

namespace Lumiere.API.Interfaces
{
    public interface IFilmeService
    {
        Task<ServiceResult<IEnumerable<FilmeDto>>> GetAllAsync();
        Task<ServiceResult<FilmeDto>> GetByIdAsync(int id);
        Task<ServiceResult<IEnumerable<FilmeDto>>> GetEmCartazAsync();
        Task<ServiceResult<FilmeDto>> CreateAsync(CreateFilmeDto dto);
        Task<ServiceResult<FilmeDto>> UpdateAsync(int id, UpdateFilmeDto dto);
        Task<ServiceResult<object>> DeleteAsync(int id);
    }
}
