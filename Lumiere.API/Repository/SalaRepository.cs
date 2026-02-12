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

        public async Task<List<Sala>> GetSalasAsync()
        {
            return await _db.Salas.OrderBy(s => s.Nome).ToListAsync();
        }

        public async Task<Sala> GetSalaByIdAsync(int id)
        {
            return (await _db.Salas.FirstOrDefaultAsync(s => s.Id == id))!;
        }

        public async Task<Sala?> GetSalaByIdWithSessoesAsync(int id)
        {
            return await _db.Salas.Include(s => s.Sessoes).FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task AddSalaAsync(Sala sala)
        {
            _db.Salas.Add(sala);
            await _db.SaveChangesAsync();
        }

        public async Task UpdateSalaAsync(Sala sala)
        {
            _db.Salas.Update(sala);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteSalaAsync(int id)
        {
            _db.Salas.Remove(await GetSalaByIdAsync(id));
            await _db.SaveChangesAsync();
        }

        public async Task<bool> SalaExistsAsync(int id)
        {
            return await _db.Salas.AnyAsync(s => s.Id == id);
        }

        public async Task<bool> SalaNomeExistsAsync(string nome, int? ignoreId = null)
        {
            var nomeNorm = nome.Trim().ToUpper();

            return await _db.Salas.AnyAsync(s =>
                s.Nome.Trim().ToUpper() == nomeNorm &&
                (!ignoreId.HasValue || s.Id != ignoreId.Value)
            );
        }

        public async Task<bool> SalaHasSessoesAsync(int id)
        {
            return await _db.Sessoes.AnyAsync(s => s.SalaId == id);
        }

        public async Task AddAssentosRangeAsync(IEnumerable<Assento> assentos)
        {
            _db.Assentos.AddRange(assentos);
            await _db.SaveChangesAsync();
        }

    }
}
