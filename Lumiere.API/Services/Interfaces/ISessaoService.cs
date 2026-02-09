using Lumiere.API.Dtos.Sessao;
using Lumiere.API.Services;

namespace Lumiere.API.Services.Interfaces;

public interface ISessaoService
{
    ServiceResult<IEnumerable<SessaoDto>> GetAll();
    ServiceResult<SessaoDto> GetById(int id);
    ServiceResult<SessaoDto> Create(CreateSessaoDto dto);
    ServiceResult<SessaoDto> Update(int id, UpdateSessaoDto dto);
    ServiceResult<object> Delete(int id);
}
