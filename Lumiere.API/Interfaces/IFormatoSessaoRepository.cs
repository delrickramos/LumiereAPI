using Lumiere.Models;

namespace Lumiere.API.Interfaces
{
    public interface IFormatoSessaoRepository
    {
        Task<List<FormatoSessao>> GetFormatosSessoesAsync();
        Task<FormatoSessao> GetFormatoSessaoByIdAsync(int id);
        Task AddFormatoSessaoAsync(FormatoSessao formatoSessao);
        Task UpdateFormatoSessaoAsync(FormatoSessao formatoSessao);
        Task DeleteFormatoSessaoAsync(int id);
        Task<bool> FormatoSessaoExistsAsync(int id);
        Task<bool> FormatoSessaoNomeExistsAsync(string nome, int? ignoreId = null);
        Task<bool> FormatoSessaoHasSessoesAsync(int id);
    }
}
