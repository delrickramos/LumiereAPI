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

        public List<Ingresso> GetIngressos()
        {
            return _db.Ingressos.OrderBy(i => i.Id).Include(i => i.Sessao).Include(i => i.Assento).Include(i => i.TipoIngresso).ToList();
        }

        public Ingresso GetIngressoById(int id)
        {
            return _db.Ingressos.Include(i => i.Sessao).Include(i => i.Assento).Include(i => i.TipoIngresso).FirstOrDefault(i => i.Id == id)!;
        }

        public List<Ingresso> GetIngressosBySessao(int sessaoId)
        {
            return _db.Ingressos.Where(i => i.SessaoId == sessaoId).Include(i => i.Assento).Include(i => i.TipoIngresso).ToList();
        }

        public void AddIngresso(Ingresso ingresso)
        {
            _db.Ingressos.Add(ingresso);
            _db.SaveChanges();
        }

        public void UpdateIngresso(Ingresso ingresso)
        {
            _db.Ingressos.Update(ingresso);
            _db.SaveChanges();
        }

        public bool IngressoExists(int id)
        {
            return _db.Ingressos.Any(i => i.Id == id);
        }

        public bool AssentoOcupadoNaSessao(int sessaoId, int assentoId)
        {
            return _db.Ingressos.Any(i => i.SessaoId == sessaoId && i.AssentoId == assentoId && i.Status == StatusIngressoEnum.Confirmado);
        }

        public int GetQuantidadeIngressosVendidos(int sessaoId)
        {
            return _db.Ingressos.Count(i => i.SessaoId == sessaoId && i.Status == StatusIngressoEnum.Confirmado);
        }

        public List<Ingresso> GetIngressosConfirmados(int sessaoId)
        {
            return _db.Ingressos.Where(i => i.SessaoId == sessaoId && i.Status == StatusIngressoEnum.Confirmado).ToList();
        }
    }
}