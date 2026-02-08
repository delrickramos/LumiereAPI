using Lumiere.API.Database;
using Lumiere.API.Interfaces;
using Lumiere.Models;
using Microsoft.EntityFrameworkCore;

namespace Lumiere.API.Repository
{
    public class SalaRepository : ISalaRepository
    {
        private readonly LumiereContext _db;
        public SalaRepository(LumiereContext db)
        {
            _db = db;
        }

        public List<Sala> GetSalas()
        {
            return _db.Salas.OrderBy(s => s.Id).Include(s => s.Sessoes).ToList();
        }

        public Sala GetSalaById(int id)
        {
            return _db.Salas.Include(s => s.Sessoes).FirstOrDefault(s => s.Id == id)!;
        }

        public void AddSala(Sala sala)
        {
            _db.Salas.Add(sala);
            _db.SaveChanges();
        }

        public void UpdateSala(Sala sala)
        {
            _db.Salas.Update(sala);
            _db.SaveChanges();
        }

        public void DeleteSala(int id)
        {
            _db.Salas.Remove(GetSalaById(id));
            _db.SaveChanges();
        }

        public bool SalaExists(int id)
        {
            return _db.Salas.Any(s => s.Id == id);
        }
    }
}
