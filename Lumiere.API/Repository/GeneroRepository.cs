using Lumiere.API.Database;
using Lumiere.API.Interfaces;
using Lumiere.Models;

namespace Lumiere.API.Repository
{
    public class GeneroRepository : IGeneroRepository
    {
        private readonly LumiereContext _db;
        public GeneroRepository(LumiereContext db)
        {
            _db = db;
        }

        public List<Genero> GetGeneros()
        {
            return _db.Generos.OrderBy(g => g.Nome).ToList();
        }

        public Genero GetGeneroById(int id)
        {
            return _db.Generos.FirstOrDefault(g => g.Id == id)!;
        }

        public void AddGenero(Genero genero)
        {
            _db.Generos.Add(genero);
            _db.SaveChanges();
        }

        public void UpdateGenero(Genero genero)
        {
            _db.Generos.Update(genero);
            _db.SaveChanges();
        }

        public void DeleteGenero(int id)
        {
            _db.Generos.Remove(GetGeneroById(id));
            _db.SaveChanges();
        }

        public bool GeneroExists(int id)
        {
            return _db.Generos.Any(g => g.Id == id);
        }

        public bool GeneroNomeExists(string nome, int? ignoreId = null)
        {
            var nomeNorm = nome.Trim().ToUpper();

            return _db.Generos.Any(g =>
                g.Nome.Trim().ToUpper() == nomeNorm &&
                (!ignoreId.HasValue || g.Id != ignoreId.Value)
            );
        }

        public bool GeneroHasFilmes(int id)
        {
            return _db.Filmes.Any(f => f.GeneroId == id);
        }
    }
}