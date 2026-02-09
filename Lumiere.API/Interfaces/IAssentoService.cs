using Lumiere.API.Dtos.Assento;
using Lumiere.API.Services;

namespace Lumiere.API.Interfaces
{
    public interface IAssentoService
    {
        ServiceResult<IEnumerable<AssentoDto>> GetAll();
        ServiceResult<AssentoDto> GetById(int id);
        ServiceResult<IEnumerable<AssentoDto>> GetBySala(int salaId);

        ServiceResult<AssentoDto> Create(CreateAssentoDto dto);
        ServiceResult<AssentoDto> Update(int id, UpdateAssentoDto dto);
        ServiceResult<object> Delete(int id);
    }
}
