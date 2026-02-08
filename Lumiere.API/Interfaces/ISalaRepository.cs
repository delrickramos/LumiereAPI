using Lumiere.Models;

namespace Lumiere.API.Interfaces
{
    public interface ISalaRepository
    {
        List<Sala> GetSalas();
        Sala GetSalaById(int id);
        void AddSala(Sala sala);
        void UpdateSala(Sala sala);
        void DeleteSala(int id);
        bool SalaExists(int id);
    }
}
