using Lumiere.API.Dtos.Genero;
using Lumiere.API.Services;

namespace Lumiere.API.Interfaces
{
    public interface IGeneroService
    {
        ServiceResult<IEnumerable<GeneroDto>> GetAll();
        ServiceResult<GeneroDto> GetById(int id);
        ServiceResult<GeneroDto> Create(CreateGeneroDto dto);
        ServiceResult<GeneroDto> Update(int id, UpdateGeneroDto dto);
        ServiceResult<object> Delete(int id);
    }
}
