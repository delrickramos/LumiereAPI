using Lumiere.Models;

namespace Lumiere.API.Interfaces
{
    public interface ISessaoRepository
    {
        List<Sessao> GetSessoes();
        Sessao GetSessaoById(int id);
        void AddSessao(Sessao sessao);
        void UpdateSessao(Sessao sessao);
        void DeleteSessao(int id);
        bool SessaoExists(int id);
        bool SessaoHasIngressos(int id);
        bool SessaoHasConflict (int salaId, DateTimeOffset dataHoraInicio, DateTimeOffset dataHoraFim, int? sessaoId = null);
    }
}
