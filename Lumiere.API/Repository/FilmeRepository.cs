using Lumiere.API.Database;
using Lumiere.API.Interfaces;
using Lumiere.Models;
using Microsoft.EntityFrameworkCore;

namespace Lumiere.API.Repository
{
    // Repositório responsável pelo acesso a dados de filmes
    public class FilmeRepository : IFilmeRepository
    {
        private readonly LumiereContext _db;
        public FilmeRepository(LumiereContext db) 
        {
            _db = db;
        }
        public async Task<List<Filme>> GetFilmesAsync()
        {
            return await _db.Filmes.OrderBy(f => f.Titulo).ToListAsync();
        }

        public async Task<Filme> GetFilmeByIdAsync(int id)
        {
            return (await _db.Filmes.FirstOrDefaultAsync(i => i.Id == id))!;
        }

        public async Task<Filme?> GetFilmeByIdWithSessoesAsync(int id)
        {
            return await _db.Filmes
                .Include(f => f.Sessoes)
                .FirstOrDefaultAsync(f => f.Id == id);
        }

        // Requisito: filmes em cartaz (com sessões nos próximos 7 dias)
        public async Task<List<Filme>> GetFilmesEmCartazAsync(DateTime inicio, DateTime fim)
        {
            return await _db.Filmes
                .Where(f =>
                    f.Sessoes != null &&
                    f.Sessoes.Any(s =>
                        s.DataHoraInicio >= inicio &&
                        s.DataHoraInicio <= fim
                    )
                )
                .Include(f => f.Sessoes)
                .OrderBy(f => f.Titulo)
                .ToListAsync();
        }

        public async Task AddFilmeAsync(Filme filme)
        {
            _db.Filmes.Add(filme);
            await _db.SaveChangesAsync();
        }

        public async Task UpdateFilmeAsync(Filme filme)
        {
            _db.Filmes.Update(filme);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteFilmeAsync(int id)
        {
            _db.Filmes.Remove(await GetFilmeByIdAsync(id));
            await _db.SaveChangesAsync();
        }

        public async Task<bool> FilmeExistsAsync(int id)
        {
            return await _db.Filmes.AnyAsync(f => f.Id == id);
        }

        public async Task<bool> FilmeHasSessoesAsync(int id)
        {
            return await _db.Sessoes.AnyAsync(s => s.FilmeId == id);
        }

        public async Task<bool> FilmeTituloExistsAsync(string titulo, int? ignoreId = null)
        {
            var titNorm = titulo.Trim().ToUpper();

            return await _db.Filmes.AnyAsync(f =>
                f.Titulo.Trim().ToUpper() == titNorm &&
                (!ignoreId.HasValue || f.Id != ignoreId.Value)
            );
        }

    }
}
