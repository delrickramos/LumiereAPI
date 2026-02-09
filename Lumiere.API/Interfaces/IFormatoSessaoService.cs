using Lumiere.API.Dtos.FormatoSessao;
using Lumiere.API.Services;

namespace Lumiere.API.Services.Interfaces
{
    public interface IFormatoSessaoService
    {
        ServiceResult<FormatoSessaoDto> Create(CreateFormatoSessaoDto dto);
        ServiceResult<FormatoSessaoDto> Update(int id, UpdateFormatoSessaoDto dto);
        ServiceResult<FormatoSessaoDto> GetById(int id);
        ServiceResult<IEnumerable<FormatoSessaoDto>> GetAll();
        ServiceResult<object> Delete(int id);
    }
}
