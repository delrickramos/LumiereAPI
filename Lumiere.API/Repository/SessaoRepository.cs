using Lumiere.API.Database;
using Lumiere.API.Interfaces;
using Lumiere.Models;
using Microsoft.EntityFrameworkCore;

namespace Lumiere.API.Repository
{
    public class SessaoRepository : ISessaoRepository
    {
        private readonly LumiereContext _db;
        public SessaoRepository(LumiereContext db)
        {
            _db = db;
        }

        public void AddSessao(Sessao sessao)
        {
            _db.Sessoes.Add(sessao);
            _db.SaveChanges();
        }
        public Sessao GetSessaoById(int id)
        {
            return _db.Sessoes.FirstOrDefault(s => s.Id == id)!;
        }

        public List<Sessao> GetSessoes()
        {
            return _db.Sessoes.OrderBy(s => s.Id).ToList();
        }

        public bool SessaoExists(int id)
        {
            return _db.Sessoes.Any(s => s.Id == id);
        }

        public void UpdateSessao(Sessao sessao)
        {
            _db.Sessoes.Update(sessao);
            _db.SaveChanges();
        }
        public void DeleteSessao(int id)
        {
            _db.Sessoes.Remove(GetSessaoById(id));
            _db.SaveChanges();
        }

        public bool SessaoHasIngressos(int id)
        {
            return _db.Ingressos.Any(i => i.SessaoId == id);
        }

        public bool SessaoHasConflict(int salaId, DateTimeOffset dataHoraInicio, DateTimeOffset dataHoraFim, int? sessaoId = null)
        {
            return _db.Sessoes.Any(s => s.SalaId == salaId && (sessaoId == null || s.Id != sessaoId) &&
            ((s.DataHoraInicio <= dataHoraInicio && s.DataHoraFim > dataHoraInicio) || 
            (s.DataHoraInicio < dataHoraFim && s.DataHoraFim >= dataHoraFim) ||
            (s.DataHoraInicio >= dataHoraInicio && s.DataHoraFim <= dataHoraFim))
            );
        }
    }
}
