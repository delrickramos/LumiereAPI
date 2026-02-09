using Lumiere.Models;

namespace Lumiere.API.Interfaces
{
    public interface IIngressoRepository
    {
        List<Ingresso> GetIngressos();
        Ingresso GetIngressoById(int id);
        List<Ingresso> GetIngressosBySessao(int sessaoId);
        void AddIngresso(Ingresso ingresso);
        void UpdateIngresso(Ingresso ingresso);
        bool IngressoExists(int id);
        bool AssentoOcupadoNaSessao(int sessaoId, int assentoId);
        int GetQuantidadeIngressosVendidos(int sessaoId);
        List<Ingresso> GetIngressosConfirmados(int sessaoId);
    }
}