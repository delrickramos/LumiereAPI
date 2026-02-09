using Lumiere.API.Database;
using Lumiere.API.Interfaces;
using Lumiere.Models;
using Microsoft.EntityFrameworkCore;

namespace Lumiere.API.Repository
{
    public class AssentoRepository : IAssentoRepository
    {
        private readonly LumiereContext _db;
        public AssentoRepository(LumiereContext db)
        {
            _db = db;
        }

        public List<Assento> GetAssentos()
        {
            return _db.Assentos.OrderBy(a => a.SalaId).ThenBy(a => a.Fileira).ThenBy(a => a.Coluna).Include(a => a.Sala).ToList();
        }

        public Assento GetAssentoById(int id)
        {
            return _db.Assentos.Include(a => a.Sala).Include(a => a.Ingressos).FirstOrDefault(a => a.Id == id)!;
        }

        public List<Assento> GetAssentosBySala(int salaId)
        {
            return _db.Assentos.Where(a => a.SalaId == salaId).OrderBy(a => a.Fileira).ThenBy(a => a.Coluna).Include(a => a.Sala).ToList();
        }

        public void AddAssento(Assento assento)
        {
            _db.Assentos.Add(assento);
            _db.SaveChanges();
        }

        public void UpdateAssento(Assento assento)
        {
            _db.Assentos.Update(assento);
            _db.SaveChanges();
        }

        public void DeleteAssento(int id)
        {
            var assento = GetAssentoById(id);
            _db.Assentos.Remove(assento);
            _db.SaveChanges();
        }

        public bool AssentoExists(int id)
        {
            return _db.Assentos.Any(a => a.Id == id);
        }

                public bool AssentoPosicaoExists(int salaId, string fileira, int coluna, int? ignoreId = null)
        {
            var fileiraNorm = (fileira ?? "").Trim().ToUpper();

            return _db.Assentos.Any(a =>
                a.SalaId == salaId &&
                a.Coluna == coluna &&
                a.Fileira.Trim().ToUpper() == fileiraNorm &&
                (!ignoreId.HasValue || a.Id != ignoreId.Value)
            );
        }

        public bool AssentoHasIngressos(int assentoId)
        {
            return _db.Ingressos.Any(i => i.AssentoId == assentoId);
        }
    }
}
