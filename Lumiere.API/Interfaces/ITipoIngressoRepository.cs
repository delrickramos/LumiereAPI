using Lumiere.Models;

namespace Lumiere.API.Interfaces
{
    public interface ITipoIngressoRepository
    {
        List<TipoIngresso> GetTiposIngresso();
        TipoIngresso GetTipoIngressoById(int id);
        void AddTipoIngresso(TipoIngresso TipoIngresso);
        void UpdateTipoIngresso(TipoIngresso TipoIngresso);
        void DeleteTipoIngresso(int id);
        bool TipoIngressoExists(int id);
    }
}
