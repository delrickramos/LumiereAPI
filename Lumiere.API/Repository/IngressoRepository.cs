using Lumiere.API.Database;
using Lumiere.API.Interfaces;
using Lumiere.Models;
using Microsoft.EntityFrameworkCore;

namespace Lumiere.API.Repository
{
    public class IngressoRepository : IIngressoRepository
    {
        private readonly LumiereContext _db;
        public IngressoRepository(LumiereContext db)
        {
            _db = db;
        }

        public async Task<List<Ingresso>> GetIngressosAsync()
        {
            return await _db.Ingressos.OrderBy(i => i.SessaoId).ThenBy(i => i.Id).ToListAsync();
        }

        public async Task<Ingresso> GetIngressoByIdAsync(int id)
        {
            return (await _db.Ingressos.FirstOrDefaultAsync(i => i.Id == id))!;
        }

        public async Task<List<Ingresso>> GetIngressosBySessaoAsync(int sessaoId)
        {
            return await _db.Ingressos.Where(i => i.SessaoId == sessaoId).Include(i => i.Assento).Include(i => i.TipoIngresso).ToListAsync();
        }

        public async Task AddIngressoAsync(Ingresso ingresso)
        {
            _db.Ingressos.Add(ingresso);
            await _db.SaveChangesAsync();
        }
        public async Task<bool> IngressoExistsAsync(int id)
        {
            return await _db.Ingressos.AnyAsync(i => i.Id == id);
        }

        public async Task<bool> AssentoOcupadoNaSessaoAsync(int sessaoId, int assentoId)
        {
            return await _db.Ingressos.AnyAsync(i => i.SessaoId == sessaoId && i.AssentoId == assentoId && i.Status == StatusIngressoEnum.Confirmado);
        }
    }
}