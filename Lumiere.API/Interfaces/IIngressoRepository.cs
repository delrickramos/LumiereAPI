using Lumiere.Models;

namespace Lumiere.API.Interfaces
{
    public interface IIngressoRepository
    {
        Task<List<Ingresso>> GetIngressosAsync();
        Task<Ingresso> GetIngressoByIdAsync(int id);
        Task<List<Ingresso>> GetIngressosBySessaoAsync(int sessaoId);
        Task AddIngressoAsync(Ingresso ingresso);
        Task<bool> IngressoExistsAsync(int id);
        Task<bool> AssentoOcupadoNaSessaoAsync(int sessaoId, int assentoId);
    }
}