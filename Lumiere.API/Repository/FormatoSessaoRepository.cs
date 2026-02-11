using Lumiere.API.Database;
using Lumiere.API.Interfaces;
using Lumiere.Models;

namespace Lumiere.API.Repository
{
    public class FormatoSessaoRepository : IFormatoSessaoRepository
    {
        private readonly LumiereContext _db;
        public FormatoSessaoRepository(LumiereContext db)
        {
            _db = db;
        }

        public List<FormatoSessao> GetFormatosSessoes()
        {
            return _db.FormatosSessao.OrderBy(f => f.Nome).ToList();
        }

        public FormatoSessao GetFormatoSessaoById(int id)
        {
            return _db.FormatosSessao.FirstOrDefault(f => f.Id == id)!;
        }
        public void AddFormatoSessao(FormatoSessao formatoSessao)
        {
            _db.FormatosSessao.Add(formatoSessao);
            _db.SaveChanges();
        }
        public void UpdateFormatoSessao(FormatoSessao formatoSessao)
        {
            _db.FormatosSessao.Update(formatoSessao);
            _db.SaveChanges();
        }

        public void DeleteFormatoSessao(int id)
        {
                _db.FormatosSessao.Remove(GetFormatoSessaoById(id));
                _db.SaveChanges();
        }

        public bool FormatoSessaoExists(int id)
        {
            return _db.FormatosSessao.Any(f => f.Id == id);
        }

        public bool FormatoSessaoNomeExists(string nome, int? ignoreId = null)
        {
            var nomeNorm = nome.Trim().ToUpper();

            return _db.FormatosSessao.Any(f =>
                f.Nome.Trim().ToUpper() == nomeNorm &&
                (!ignoreId.HasValue || f.Id != ignoreId.Value)
            );
        }

        public bool FormatoSessaoHasSessoes(int id)
        {
            return _db.Sessoes.Any(s => s.FormatoSessaoId == id);
        }
    }
}
