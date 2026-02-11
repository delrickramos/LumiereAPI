using Lumiere.API.Database;
using Lumiere.API.Dtos.Relatorio;
using Lumiere.API.Interfaces;
using Lumiere.Models;

namespace Lumiere.API.Repository
{
    public class RelatorioRepository : IRelatorioRepository
    {
        private readonly LumiereContext _db;

        public RelatorioRepository(LumiereContext db)
        {
            _db = db;
        }

        public List<SalaOcupacaoDto> GetTaxaOcupacaoSalas()
        {
            return _db.Salas
                .Select(s => new SalaOcupacaoDto
                {
                    SalaId = s.Id,
                    Nome = s.Nome,
                    Capacidade = s.Capacidade,

                    TotalIngressosVendidos = s.Sessoes!
                        .Where(sessao => sessao.DataHoraFim < DateTimeOffset.Now)
                        .SelectMany(sessao => sessao.Ingressos!)
                        .Count(ingresso => ingresso.Status == StatusIngressoEnum.Confirmado),

                    TaxaOcupacao = s.Capacidade > 0
                        ? (decimal)s.Sessoes!
                            .Where(sessao => sessao.DataHoraFim < DateTimeOffset.Now)
                            .SelectMany(sessao => sessao.Ingressos!)
                            .Count(ingresso => ingresso.Status == StatusIngressoEnum.Confirmado)
                          / s.Capacidade * 100
                        : 0
                })
                .ToList();
        }
    }
}