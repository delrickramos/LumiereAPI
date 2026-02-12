using Lumiere.Models;

namespace Lumiere.API.Interfaces
{
    public interface ISessaoRepository
    {
        Task<List<Sessao>> GetSessoesAsync();
        Task<Sessao> GetSessaoByIdAsync(int id);
        Task AddSessaoAsync(Sessao sessao);
        Task UpdateSessaoAsync(Sessao sessao);
        Task DeleteSessaoAsync(int id);
        Task<bool> SessaoExistsAsync(int id);
        Task<bool> SessaoHasIngressosAsync(int id);
        Task<bool> SessaoHasConflictAsync(int salaId, DateTimeOffset dataHoraInicio, DateTimeOffset dataHoraFim, int? sessaoId = null);
    }
}
