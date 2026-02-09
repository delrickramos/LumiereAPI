using Lumiere.API.Dtos.Filme;
using Lumiere.API.Services;

namespace Lumiere.API.Interfaces
{
    public interface IFilmeService
    {
        ServiceResult<IEnumerable<FilmeDto>> GetAll();
        ServiceResult<FilmeDto> GetById(int id);
        ServiceResult<FilmeDto> Create(CreateFilmeDto dto);
        ServiceResult<FilmeDto> Update(int id, UpdateFilmeDto dto);
        ServiceResult<object> Delete(int id);
    }
}
