using Lumiere.Models;

namespace Lumiere.API.Interfaces
{
    public interface ISalaRepository
    {
        Task<List<Sala>> GetSalasAsync();
        Task<Sala> GetSalaByIdAsync(int id);
        Task<Sala?> GetSalaByIdWithSessoesAsync(int id);
        Task AddSalaAsync(Sala sala);
        Task UpdateSalaAsync(Sala sala);
        Task DeleteSalaAsync(int id);
        Task<bool> SalaExistsAsync(int id);
        Task<bool> SalaNomeExistsAsync(string nome, int? ignoreId = null);
        Task<bool> SalaHasSessoesAsync(int id);
        Task AddAssentosRangeAsync(IEnumerable<Assento> assentos);
    }
}
