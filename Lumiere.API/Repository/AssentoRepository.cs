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
        public async Task<Assento> GetAssentoByIdAsync(int id)
        {
            return (await _db.Assentos.Include(a => a.Sala).Include(a => a.Ingressos).FirstOrDefaultAsync(a => a.Id == id))!;
        }

        public async Task<List<Assento>> GetAssentosBySalaAsync(int salaId)
        {
            return await _db.Assentos.Where(a => a.SalaId == salaId).OrderBy(a => a.Fileira).ThenBy(a => a.Coluna).Include(a => a.Sala).ToListAsync();
        }

        public async Task AddAssentoAsync(Assento assento)
        {
            _db.Assentos.Add(assento);
            await _db.SaveChangesAsync();
        }

        public async Task UpdateAssentoAsync(Assento assento)
        {
            _db.Assentos.Update(assento);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteAssentoAsync(int id)
        {
            var assento = await GetAssentoByIdAsync(id);
            _db.Assentos.Remove(assento);
            await _db.SaveChangesAsync();
        }

        public async Task<bool> AssentoExistsAsync(int id)
        {
            return await _db.Assentos.AnyAsync(a => a.Id == id);
        }

        public async Task<bool> AssentoPosicaoExistsAsync(int salaId, string fileira, int coluna, int? ignoreId = null)
        {
            var fileiraNorm = (fileira ?? "").Trim().ToUpper();

            return await _db.Assentos.AnyAsync(a =>
                a.SalaId == salaId &&
                a.Coluna == coluna &&
                a.Fileira.Trim().ToUpper() == fileiraNorm &&
                (!ignoreId.HasValue || a.Id != ignoreId.Value)
            );
        }

        public async Task<bool> AssentoHasIngressosAsync(int assentoId)
        {
            return await _db.Ingressos.AnyAsync(i => i.AssentoId == assentoId);
        }
    }
}
