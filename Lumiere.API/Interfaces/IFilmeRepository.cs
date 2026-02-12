using Lumiere.Models;

namespace Lumiere.API.Interfaces
{
    public interface IFilmeRepository
    {
        Task<List<Filme>> GetFilmesAsync();
        Task<Filme> GetFilmeByIdAsync(int id);
        public Task<Filme?> GetFilmeByIdWithSessoesAsync(int id);
        Task<List<Filme>> GetFilmesEmCartazAsync(DateTime inicio, DateTime fim);
        Task AddFilmeAsync(Filme filme);
        Task UpdateFilmeAsync(Filme filme);
        Task DeleteFilmeAsync(int id);
        Task<bool> FilmeExistsAsync(int id);            
        Task<bool> FilmeHasSessoesAsync(int id);

        Task<bool> FilmeTituloExistsAsync(string titulo, int? ignoreId = null);
    }
}