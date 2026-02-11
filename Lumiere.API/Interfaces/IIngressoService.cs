using Lumiere.API.Dtos.Ingresso;
using Lumiere.API.Common;

namespace Lumiere.API.Interfaces
{
    public interface IIngressoService
    {
        ServiceResult<IEnumerable<IngressoDto>> GetAll();
        ServiceResult<IngressoDto> GetById(int id);
        ServiceResult<IEnumerable<IngressoDto>> GetBySessao(int sessaoId);

        ServiceResult<IngressoDto> Vender(CreateIngressoDto dto);
    }
}
