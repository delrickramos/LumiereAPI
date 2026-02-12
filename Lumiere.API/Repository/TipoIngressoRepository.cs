using Lumiere.API.Database;
using Lumiere.API.Interfaces;
using Lumiere.Models;
using Microsoft.EntityFrameworkCore;

public class TipoIngressoRepository : ITipoIngressoRepository
{

    private readonly LumiereContext _db;

    public TipoIngressoRepository(LumiereContext db)
    {   
        _db = db;
    }

    public async Task AddTipoIngressoAsync(TipoIngresso TipoIngresso)
    {
        _db.TiposIngresso.Add(TipoIngresso);
        await _db.SaveChangesAsync();
    }

    public async Task DeleteTipoIngressoAsync(int id)
    {
        var tipoIngresso = await GetTipoIngressoByIdAsync(id);
        _db.TiposIngresso.Remove(tipoIngresso);
        await _db.SaveChangesAsync();
    }

    public async Task<TipoIngresso> GetTipoIngressoByIdAsync(int id)
    {
        return (await _db.TiposIngresso.FindAsync(id))!;
    }

    public async Task<List<TipoIngresso>> GetTiposIngressoAsync()
    {
        return await _db.TiposIngresso.OrderBy(t => t.Nome).ToListAsync();
    }

    public async Task<bool> TipoIngressoExistsAsync(int id)
    {
        return await _db.TiposIngresso.AnyAsync(s => s.Id == id);
    }

    public async Task UpdateTipoIngressoAsync(TipoIngresso tipoIngresso)
    {
        _db.TiposIngresso.Update(tipoIngresso);
        await _db.SaveChangesAsync();
    }

    public async Task<bool> TipoIngressoNomeExistsAsync(string nome, int? ignoreId = null)
    {
        var nomeNorm = nome.Trim().ToUpper();

        return await _db.TiposIngresso.AnyAsync(t =>
            t.Nome.Trim().ToUpper() == nomeNorm &&
            (!ignoreId.HasValue || t.Id != ignoreId.Value)
        );
    }
    public async Task<bool> TipoIngressoHasIngressosAsync(int id)
    {
        return await _db.Ingressos.AnyAsync(i => i.TipoIngressoId == id);
    }

}