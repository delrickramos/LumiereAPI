using Lumiere.API.Database;
using Lumiere.API.Interfaces;
using Lumiere.Models;
using Microsoft.EntityFrameworkCore;

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
            return _db.Generos.OrderBy(g => g.Id).Include(g => g.Filmes).ToList();
        }

        public Genero GetGeneroById(int id)
        {
            return _db.Generos.Include(g => g.Filmes).FirstOrDefault(g => g.Id == id)!;
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
    }
}