using Lumiere.Models;

namespace Lumiere.API.Interfaces
{
    public interface IAssentoRepository
    {
        Task<Assento> GetAssentoByIdAsync(int id);
        Task<List<Assento>> GetAssentosBySalaAsync(int salaId);
        Task AddAssentoAsync(Assento assento);
        Task UpdateAssentoAsync(Assento assento);
        Task DeleteAssentoAsync(int id);
        Task<bool> AssentoExistsAsync(int id);
        Task<bool> AssentoPosicaoExistsAsync(int salaId, string fileira, int coluna, int? ignoreId = null);
        Task<bool> AssentoHasIngressosAsync(int assentoId);
    }
}
