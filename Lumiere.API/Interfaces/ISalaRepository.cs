using Lumiere.Models;

namespace Lumiere.API.Interfaces
{
    public interface ISalaRepository
    {
        List<Sala> GetSalas();
        Sala GetSalaById(int id);
        Sala? GetSalaByIdWithSessoes(int id);
        void AddSala(Sala sala);
        void UpdateSala(Sala sala);
        void DeleteSala(int id);
        bool SalaExists(int id);
        bool SalaNomeExists(string nome, int? ignoreId = null);
        bool SalaHasSessoes(int id);
    }
}
