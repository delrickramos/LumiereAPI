using Lumiere.Models;

namespace Lumiere.API.Interfaces
{
    public interface IGeneroRepository
    {
        Task<List<Genero>> GetGenerosAsync();
        Task<Genero> GetGeneroByIdAsync(int id);
        Task AddGeneroAsync(Genero genero);
        Task UpdateGeneroAsync(Genero genero);
        Task DeleteGeneroAsync(int id);
        Task<bool> GeneroExistsAsync(int id);
        Task<bool> GeneroNomeExistsAsync(string nome, int? ignoreId = null);
        Task<bool> GeneroHasFilmesAsync(int id);
    }
}