using Lumiere.Models;

namespace Lumiere.API.Interfaces
{
    public interface IFormatoSessaoRepository
    {
        List<FormatoSessao> GetFormatosSessoes();
        FormatoSessao GetFormatoSessaoById(int id);
        void AddFormatoSessao(FormatoSessao formatoSessao);
        void UpdateFormatoSessao(FormatoSessao formatoSessao);
        void DeleteFormatoSessao(int id);
        bool FormatoSessaoExists(int id);
    }
}
