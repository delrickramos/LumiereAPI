using Lumiere.API.Dtos.TipoIngresso;
using Lumiere.API.Common;

namespace Lumiere.API.Interfaces
{
    public interface ITipoIngressoService
    {
        ServiceResult<IEnumerable<TipoIngressoDto>> GetAll();
        ServiceResult<TipoIngressoDto> GetById(int id);
        ServiceResult<TipoIngressoDto> Create(CreateTipoIngressoDto dto);
        ServiceResult<TipoIngressoDto> Update(int id, UpdateTipoIngressoDto dto);
        ServiceResult<object> Delete(int id);
    }
}
