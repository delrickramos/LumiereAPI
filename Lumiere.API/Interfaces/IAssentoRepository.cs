using Lumiere.Models;

namespace Lumiere.API.Interfaces
{
    public interface IAssentoRepository
    {
        List<Assento> GetAssentos();
        Assento GetAssentoById(int id);
        List<Assento> GetAssentosBySala(int salaId);
        void AddAssento(Assento assento);
        void UpdateAssento(Assento assento);
        void DeleteAssento(int id);
        bool AssentoExists(int id);
        bool AssentoPosicaoExists(int salaId, string fileira, int coluna, int? ignoreId = null);
        bool AssentoHasIngressos(int assentoId);
    }
}
