using Lumiere.Models;

namespace Lumiere.API.Repositories
{
    public interface IFilmeRepository
    {
        List<Filme> GetFilmes();
         Filme GetFilmeById(int id);
         void AddFilme(Filme filme);
         void UpdateFilme(Filme filme);
         void DeleteFilme(int id);
    }
}
