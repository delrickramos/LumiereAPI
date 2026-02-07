using Lumiere.API.Database;
using Lumiere.API.Interfaces;
using Lumiere.Models;

namespace Lumiere.API.Repository
{
    public class FilmeRepository : IFilmeRepository
    {
        private readonly LumiereContext _db;
        public FilmeRepository(LumiereContext db) 
        {
            _db = db;
        }
        public List<Filme> GetFilmes()
        {
            return _db.Filmes.OrderBy( a => a.Id).ToList();
        }

        public Filme GetFilmeById(int id)
        {
            return _db.Filmes.Find(id)!;
        }

        public void AddFilme(Filme filme)
        {
            _db.Filmes.Add(filme);
            _db.SaveChanges();
        }

        public void UpdateFilme(Filme filme)
        {
            _db.Filmes.Update(filme);
            _db.SaveChanges();
        }

        public void DeleteFilme(int id)
        {
            _db.Filmes.Remove(GetFilmeById(id));
            _db.SaveChanges();
        }
    }
}
