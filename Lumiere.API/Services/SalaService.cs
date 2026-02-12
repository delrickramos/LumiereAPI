using Lumiere.API.Dtos.Sala;
using Lumiere.API.Interfaces;
using Lumiere.API.Mappers;
using Lumiere.Models;
using Lumiere.API.Common;

namespace Lumiere.API.Services
{
    // Serviço responsável pela lógica de negócio de salas
    public class SalaService : ISalaService
    {
        private readonly ISalaRepository _repo;

        private const int NomeMin = 2;
        private const int NomeMax = 60;

        public SalaService(ISalaRepository repo)
        {
            _repo = repo;
        }

        public async Task<ServiceResult<IEnumerable<SalaDto>>> GetAllAsync()
        {
            var salas = (await _repo.GetSalasAsync()).Select(s => s.ToSalaDto());
            return ServiceResult<IEnumerable<SalaDto>>.Success(salas);
        }

        public async Task<ServiceResult<SalaDto>> GetByIdAsync(int id)
        {
            if (id <= 0)
                return ServiceResult<SalaDto>.Fail("Id inválido.", 400);

            var sala = await _repo.GetSalaByIdAsync(id);
            if (sala == null)
                return ServiceResult<SalaDto>.Fail("Sala não encontrada.", 404);

            return ServiceResult<SalaDto>.Success(sala.ToSalaDto());
        }

        public async Task<ServiceResult<SalaDto>> CreateAsync(CreateSalaDto dto)
        {
            var nome = (dto.Nome ?? "").Trim();

            var nomeVal = ValidateTexto("Nome", nome, NomeMin, NomeMax);
            if (nomeVal != null) return ServiceResult<SalaDto>.Fail(nomeVal, 400);

            var linhasVal = ValidateLinhas(dto.NumeroLinhas);
            if (linhasVal != null) return ServiceResult<SalaDto>.Fail(linhasVal, 400);

            var colunasVal = ValidateColunas(dto.NumeroColunas);
            if (colunasVal != null) return ServiceResult<SalaDto>.Fail(colunasVal, 400);

            if (await _repo.SalaNomeExistsAsync(nome))
                return ServiceResult<SalaDto>.Fail("Já existe uma sala com esse nome.", 409);

            var sala = dto.ToSalaModel();
            sala.Nome = nome;
            sala.Capacidade = dto.NumeroLinhas * dto.NumeroColunas;

            await _repo.AddSalaAsync(sala);

            // Gera automaticamente os assentos da sala com base em linhas e colunas
            var assentos = GerarAssentos(
                sala.Id,
                dto.NumeroLinhas,
                dto.NumeroColunas
            );

            await _repo.AddAssentosRangeAsync(assentos);

            return ServiceResult<SalaDto>.Success(sala.ToSalaDto(), 201);
        }

        public async Task<ServiceResult<SalaDto>> UpdateAsync(int id, UpdateSalaDto dto)
        {
            if (id <= 0)
                return ServiceResult<SalaDto>.Fail("Id inválido.", 400);

            var sala = await _repo.GetSalaByIdAsync(id);
            if (sala == null)
                return ServiceResult<SalaDto>.Fail("Sala não encontrada.", 404);

            var nome = (dto.Nome ?? "").Trim();

            var nomeVal = ValidateTexto("Nome", nome, NomeMin, NomeMax);
            if (nomeVal != null) return ServiceResult<SalaDto>.Fail(nomeVal, 400);

            if (await _repo.SalaNomeExistsAsync(nome, ignoreId: id))
                return ServiceResult<SalaDto>.Fail("Já existe uma sala com esse nome.", 409);

            dto.UpdateSalaModel(sala);

            sala.Nome = nome;

            await _repo.UpdateSalaAsync(sala);
            return ServiceResult<SalaDto>.Success(sala.ToSalaDto());
        }

        public async Task<ServiceResult<object>> DeleteAsync(int id)
        {
            if (id <= 0)
                return ServiceResult<object>.Fail("Id inválido.", 400);

            var sala = await _repo.GetSalaByIdAsync(id);
            if (sala == null)
                return ServiceResult<object>.Fail("Sala não encontrada.", 404);

            // Não permite excluir sala que possui sessões vinculadas
            if (await _repo.SalaHasSessoesAsync(id))
                return ServiceResult<object>.Fail("Não é possível excluir uma sala que possui sessões vinculadas.", 409);

            await _repo.DeleteSalaAsync(id);
            return ServiceResult<object>.Success(new { }, 204);
        }

        private string? ValidateTexto(string campo, string valor, int min, int max)
        {
            if (string.IsNullOrWhiteSpace(valor))
                return $"{campo} é obrigatório.";

            if (valor.Length < min || valor.Length > max)
                return $"{campo} deve ter entre {min} e {max} caracteres.";

            return null;
        }

        // Define o tipo de assento com base na posição (acessibilidade)
        private static TipoAssentoEnum CalcularTipoAssento(int fileiraIndex, int coluna, int linhas, int colunas)
        {
            bool ehPrimeira = fileiraIndex == 0;
            bool ehUltima = fileiraIndex == linhas - 1;
            bool ehPonta = (coluna == 1) || (coluna == colunas);

            if ((ehPrimeira || ehUltima) && ehPonta)
                return TipoAssentoEnum.Obeso;

            if (ehPrimeira)
                return TipoAssentoEnum.Cadeirante;

            return TipoAssentoEnum.Normal;
        }

        // Converte índice numérico em letra(s) (0 = A, 25 = Z, 26 = AA)
        private static string LinhaParaLetras(int index)
        {
            var sb = new System.Text.StringBuilder();
            index += 1;
            while (index > 0)
            {
                index--;
                sb.Insert(0, (char)('A' + (index % 26)));
                index /= 26;
            }
            return sb.ToString();
        }

        // Gera todos os assentos da sala com nomes formatados (A1, A2, B1, etc)
        private static List<Assento> GerarAssentos(int salaId, int linhas, int colunas)
        {
            var list = new List<Assento>(linhas * colunas);

            for (int i = 0; i < linhas; i++)
            {
                var fileira = LinhaParaLetras(i);

                for (int coluna = 1; coluna <= colunas; coluna++)
                {
                    list.Add(new Assento
                    {
                        SalaId = salaId,
                        Fileira = fileira,
                        Coluna = coluna,
                        Nome = $"{fileira}{coluna}",
                        TipoAssento = CalcularTipoAssento(i, coluna, linhas, colunas)
                    });
                }
            }

            return list;
        }

        private string? ValidateLinhas(int linhas)
        {
            if (linhas <= 0) return "NumeroLinhas deve ser maior que zero.";
            if (linhas > 52) return "NumeroLinhas muito grande (máximo 52).";
            return null;
        }

        private string? ValidateColunas(int colunas)
        {
            if (colunas <= 0) return "NumeroColunas deve ser maior que zero.";
            if (colunas > 60) return "NumeroColunas muito grande (máximo 60).";
            return null;
        }

    }
}
