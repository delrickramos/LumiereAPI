using Lumiere.API.Database;
using Lumiere.API.Interfaces;
using Lumiere.Models;

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
            return _db.Sessoes.Find(id)!;
        }

        public List<Sessao> GetSessoes()
        {
            return _db.Sessoes.ToList();
        }
    }
}
