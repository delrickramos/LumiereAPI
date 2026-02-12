using Lumiere.API.Database;
using Lumiere.API.Interfaces;
using Lumiere.Models;
using Microsoft.EntityFrameworkCore;

namespace Lumiere.API.Repository
{
    public class FormatoSessaoRepository : IFormatoSessaoRepository
    {
        private readonly LumiereContext _db;
        public FormatoSessaoRepository(LumiereContext db)
        {
            _db = db;
        }

        public async Task<List<FormatoSessao>> GetFormatosSessoesAsync()
        {
            return await _db.FormatosSessao.OrderBy(f => f.Nome).ToListAsync();
        }

        public async Task<FormatoSessao> GetFormatoSessaoByIdAsync(int id)
        {
            return (await _db.FormatosSessao.FirstOrDefaultAsync(f => f.Id == id))!;
        }
        public async Task AddFormatoSessaoAsync(FormatoSessao formatoSessao)
        {
            _db.FormatosSessao.Add(formatoSessao);
            await _db.SaveChangesAsync();
        }
        public async Task UpdateFormatoSessaoAsync(FormatoSessao formatoSessao)
        {
            _db.FormatosSessao.Update(formatoSessao);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteFormatoSessaoAsync(int id)
        {
                _db.FormatosSessao.Remove(await GetFormatoSessaoByIdAsync(id));
                await _db.SaveChangesAsync();
        }

        public async Task<bool> FormatoSessaoExistsAsync(int id)
        {
            return await _db.FormatosSessao.AnyAsync(f => f.Id == id);
        }

        public async Task<bool> FormatoSessaoNomeExistsAsync(string nome, int? ignoreId = null)
        {
            var nomeNorm = nome.Trim().ToUpper();

            return await _db.FormatosSessao.AnyAsync(f =>
                f.Nome.Trim().ToUpper() == nomeNorm &&
                (!ignoreId.HasValue || f.Id != ignoreId.Value)
            );
        }

        public async Task<bool> FormatoSessaoHasSessoesAsync(int id)
        {
            return await _db.Sessoes.AnyAsync(s => s.FormatoSessaoId == id);
        }
    }
}
