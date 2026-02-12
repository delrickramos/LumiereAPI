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

        public async Task<List<Genero>> GetGenerosAsync()
        {
            return await _db.Generos.OrderBy(g => g.Nome).ToListAsync();
        }

        public async Task<Genero> GetGeneroByIdAsync(int id)
        {
            return (await _db.Generos.FirstOrDefaultAsync(g => g.Id == id))!;
        }

        public async Task AddGeneroAsync(Genero genero)
        {
            _db.Generos.Add(genero);
            await _db.SaveChangesAsync();
        }

        public async Task UpdateGeneroAsync(Genero genero)
        {
            _db.Generos.Update(genero);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteGeneroAsync(int id)
        {
            _db.Generos.Remove(await GetGeneroByIdAsync(id));
            await _db.SaveChangesAsync();
        }

        public async Task<bool> GeneroExistsAsync(int id)
        {
            return await _db.Generos.AnyAsync(g => g.Id == id);
        }

        public async Task<bool> GeneroNomeExistsAsync(string nome, int? ignoreId = null)
        {
            var nomeNorm = nome.Trim().ToUpper();

            return await _db.Generos.AnyAsync(g =>
                g.Nome.Trim().ToUpper() == nomeNorm &&
                (!ignoreId.HasValue || g.Id != ignoreId.Value)
            );
        }

        public async Task<bool> GeneroHasFilmesAsync(int id)
        {
            return await _db.Filmes.AnyAsync(f => f.GeneroId == id);
        }
    }
}