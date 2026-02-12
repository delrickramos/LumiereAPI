using Lumiere.API.Database;
using Lumiere.API.Interfaces;
using Lumiere.Models;
using Microsoft.EntityFrameworkCore;

namespace Lumiere.API.Repository
{
    // Repositório responsável pelo acesso a dados de sessões
    public class SessaoRepository : ISessaoRepository
    {
        private readonly LumiereContext _db;
        public SessaoRepository(LumiereContext db)
        {
            _db = db;
        }

        public async Task AddSessaoAsync(Sessao sessao)
        {
            _db.Sessoes.Add(sessao);
            await _db.SaveChangesAsync();
        }
        public async Task<Sessao> GetSessaoByIdAsync(int id)
        {
            return (await _db.Sessoes.FirstOrDefaultAsync(s => s.Id == id))!;
        }

        public async Task<List<Sessao>> GetSessoesAsync()
        {
            return await _db.Sessoes.OrderBy(s => s.DataHoraInicio).ToListAsync();
        }

        public async Task<bool> SessaoExistsAsync(int id)
        {
            return await _db.Sessoes.AnyAsync(s => s.Id == id);
        }

        public async Task UpdateSessaoAsync(Sessao sessao)
        {
            _db.Sessoes.Update(sessao);
            await _db.SaveChangesAsync();
        }
        public async Task DeleteSessaoAsync(int id)
        {
            _db.Sessoes.Remove(await GetSessaoByIdAsync(id));
            await _db.SaveChangesAsync();
        }

        public async Task<bool> SessaoHasIngressosAsync(int id)
        {
            return await _db.Ingressos.AnyAsync(i => i.SessaoId == id);
        }

        // Verifica se há conflito de horário na sala (interseção de horários)
        public async Task<bool> SessaoHasConflictAsync(int salaId, DateTimeOffset dataHoraInicio, DateTimeOffset dataHoraFim, int? sessaoId = null)
        {
            return await _db.Sessoes.AnyAsync(s => s.SalaId == salaId && (sessaoId == null || s.Id != sessaoId) &&
            ((s.DataHoraInicio <= dataHoraInicio && s.DataHoraFim > dataHoraInicio) || 
            (s.DataHoraInicio < dataHoraFim && s.DataHoraFim >= dataHoraFim) ||
            (s.DataHoraInicio >= dataHoraInicio && s.DataHoraFim <= dataHoraFim))
            );
        }
    }
}
