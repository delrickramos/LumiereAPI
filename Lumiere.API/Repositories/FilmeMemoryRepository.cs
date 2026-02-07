using Lumiere.Models;

namespace Lumiere.API.Repositories
{
    public class FilmeMemoryRepository : IFilmeRepository
    {
        public static List<Filme> filmes = new List<Filme>();
        public List<Filme> GetFilmes()
        {
            return filmes;
        }

        public Filme GetFilmeById(int id)
        {
            return filmes.Find(filmes => filmes.Id == id)!;
        }

        public void AddFilme(Filme filme)
        {
            filmes.Add(filme);
        }

        public void UpdateFilme(Filme filme)
        {
            filmes.Remove(GetFilmeById(filme.Id));
            filmes.Add(filme);
        }

        public void DeleteFilme(int id)
        {
            filmes.Remove(GetFilmeById(id));
        }
    }
}
