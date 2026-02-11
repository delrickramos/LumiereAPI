using Lumiere.API.Database;
using Lumiere.API.Interfaces;
using Lumiere.Models;
using Microsoft.EntityFrameworkCore;

namespace Lumiere.API.Repository
{
    public class FilmeRepository : IFilmeRepository
    {
        private readonly LumiereContext _db;
        public FilmeRepository(LumiereContext db) 
        {
            _db = db;
        }
        public List<Filme> GetFilmes()
        {
            return _db.Filmes.OrderBy(f => f.Titulo).ToList();
        }

        public Filme GetFilmeById(int id)
        {
            return _db.Filmes.FirstOrDefault(i => i.Id == id)!;
        }

        public Filme? GetFilmeByIdWithSessoes(int id)
        {
            return _db.Filmes
                .Include(f => f.Sessoes)
                .FirstOrDefault(f => f.Id == id);
        }

        public List<Filme> GetFilmesEmCartaz(DateTime inicio, DateTime fim)
        {
            return _db.Filmes
                .Where(f =>
                    f.Sessoes != null &&
                    f.Sessoes.Any(s =>
                        s.DataHoraInicio >= inicio &&
                        s.DataHoraInicio <= fim
                    )
                )
                .Include(f => f.Sessoes)
                .OrderBy(f => f.Titulo)
                .ToList();
        }

        public void AddFilme(Filme filme)
        {
            _db.Filmes.Add(filme);
            _db.SaveChanges();
        }

        public void UpdateFilme(Filme filme)
        {
            _db.Filmes.Update(filme);
            _db.SaveChanges();
        }

        public void DeleteFilme(int id)
        {
            _db.Filmes.Remove(GetFilmeById(id));
            _db.SaveChanges();
        }

        public bool FilmeExists(int id)
        {
            return _db.Filmes.Any(f => f.Id == id);
        }

        public bool FilmeHasSessoes(int id)
        {
            return _db.Sessoes.Any(s => s.FilmeId == id);
        }

        public bool FilmeTituloExists(string titulo, int? ignoreId = null)
        {
            var titNorm = titulo.Trim().ToUpper();

            return _db.Filmes.Any(f =>
                f.Titulo.Trim().ToUpper() == titNorm &&
                (!ignoreId.HasValue || f.Id != ignoreId.Value)
            );
        }

    }
}
