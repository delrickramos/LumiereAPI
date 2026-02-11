using Lumiere.API.Database;
using Lumiere.API.Interfaces;
using Lumiere.Models;

public class TipoIngressoRepository : ITipoIngressoRepository
{

    private readonly LumiereContext _db;

    public TipoIngressoRepository(LumiereContext db)
    {   
        _db = db;
    }

    public void AddTipoIngresso(TipoIngresso TipoIngresso)
    {
        _db.TiposIngresso.Add(TipoIngresso);
        _db.SaveChanges();
    }

    public void DeleteTipoIngresso(int id)
    {
        var tipoIngresso = GetTipoIngressoById(id);
        _db.TiposIngresso.Remove(tipoIngresso);
        _db.SaveChanges();
    }

    public TipoIngresso GetTipoIngressoById(int id)
    {
        return _db.TiposIngresso.Find(id)!;
    }

    public List<TipoIngresso> GetTiposIngresso()
    {
        return _db.TiposIngresso.OrderBy(t => t.Nome).ToList();
    }

    public bool TipoIngressoExists(int id)
    {
        return _db.TiposIngresso.Any(s => s.Id == id);
    }

    public void UpdateTipoIngresso(TipoIngresso tipoIngresso)
    {
        _db.TiposIngresso.Update(tipoIngresso);
        _db.SaveChanges();
    }

    public bool TipoIngressoNomeExists(string nome, int? ignoreId = null)
    {
        var nomeNorm = nome.Trim().ToUpper();

        return _db.TiposIngresso.Any(t =>
            t.Nome.Trim().ToUpper() == nomeNorm &&
            (!ignoreId.HasValue || t.Id != ignoreId.Value)
        );
    }
    public bool TipoIngressoHasIngressos(int id)
    {
        return _db.Ingressos.Any(i => i.TipoIngressoId == id);
    }

}