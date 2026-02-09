using Lumiere.Models;

namespace Lumiere.API.Interfaces
{
    public interface IFilmeRepository
    {
        List<Filme> GetFilmes();
        Filme GetFilmeById(int id);
        public Filme? GetFilmeByIdWithSessoes(int id);
        void AddFilme(Filme filme);
        void UpdateFilme(Filme filme);
        void DeleteFilme(int id);
        bool FilmeExists(int id);            
        bool FilmeHasSessoes(int id);

        bool FilmeTituloExists(string titulo, int? ignoreId = null);
    }
}