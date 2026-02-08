using Lumiere.Models;

namespace Lumiere.API.Interfaces
{
    public interface ISessaoRepository
    {
        List<Sessao> GetSessoes();
        Sessao GetSessaoById(int id);
        void AddSessao(Sessao sessao);
    }
}
