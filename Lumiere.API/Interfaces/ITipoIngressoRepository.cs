using Lumiere.Models;

namespace Lumiere.API.Interfaces
{
    public interface ITipoIngressoRepository
    {
        Task<List<TipoIngresso>> GetTiposIngressoAsync();
        Task<TipoIngresso> GetTipoIngressoByIdAsync(int id);
        Task AddTipoIngressoAsync(TipoIngresso TipoIngresso);
        Task UpdateTipoIngressoAsync(TipoIngresso TipoIngresso);
        Task DeleteTipoIngressoAsync(int id);
        Task<bool> TipoIngressoExistsAsync(int id);
        Task<bool> TipoIngressoNomeExistsAsync(string nome, int? ignoreId = null);
        Task<bool> TipoIngressoHasIngressosAsync(int id);
    }
}
