using Lumiere.API.Dtos.Sala;
using Lumiere.API.Interfaces;
using Lumiere.API.Mappers;
using Lumiere.Models;
using Lumiere.API.Common;

namespace Lumiere.API.Services
{
    public class SalaService : ISalaService
    {
        private readonly ISalaRepository _repo;

        private const int NomeMin = 2;
        private const int NomeMax = 60;

        public SalaService(ISalaRepository repo)
        {
            _repo = repo;
        }

        public ServiceResult<IEnumerable<SalaDto>> GetAll()
        {
            var salas = _repo.GetSalas().Select(s => s.ToSalaDto());
            return ServiceResult<IEnumerable<SalaDto>>.Success(salas);
        }

        public ServiceResult<SalaDto> GetById(int id)
        {
            if (id <= 0)
                return ServiceResult<SalaDto>.Fail("Id inválido.", 400);

            var sala = _repo.GetSalaById(id);
            if (sala == null)
                return ServiceResult<SalaDto>.Fail("Sala não encontrada.", 404);

            return ServiceResult<SalaDto>.Success(sala.ToSalaDto());
        }

        public ServiceResult<SalaDto> Create(CreateSalaDto dto)
        {
            var nome = (dto.Nome ?? "").Trim();

            var nomeVal = ValidateTexto("Nome", nome, NomeMin, NomeMax);
            if (nomeVal != null) return ServiceResult<SalaDto>.Fail(nomeVal, 400);

            var linhasVal = ValidateLinhas(dto.NumeroLinhas);
            if (linhasVal != null) return ServiceResult<SalaDto>.Fail(linhasVal, 400);

            var colunasVal = ValidateColunas(dto.NumeroColunas);
            if (colunasVal != null) return ServiceResult<SalaDto>.Fail(colunasVal, 400);

            if (_repo.SalaNomeExists(nome))
                return ServiceResult<SalaDto>.Fail("Já existe uma sala com esse nome.", 409);

            var sala = dto.ToSalaModel();
            sala.Nome = nome;
            sala.Capacidade = dto.NumeroLinhas * dto.NumeroColunas;

            _repo.AddSala(sala);

            var assentos = GerarAssentos(
                sala.Id,
                dto.NumeroLinhas,
                dto.NumeroColunas
            );

            _repo.AddAssentosRange(assentos);

            return ServiceResult<SalaDto>.Success(sala.ToSalaDto(), 201);
        }

        public ServiceResult<SalaDto> Update(int id, UpdateSalaDto dto)
        {
            if (id <= 0)
                return ServiceResult<SalaDto>.Fail("Id inválido.", 400);

            var sala = _repo.GetSalaById(id);
            if (sala == null)
                return ServiceResult<SalaDto>.Fail("Sala não encontrada.", 404);

            var nome = (dto.Nome ?? "").Trim();

            var nomeVal = ValidateTexto("Nome", nome, NomeMin, NomeMax);
            if (nomeVal != null) return ServiceResult<SalaDto>.Fail(nomeVal, 400);

            if (_repo.SalaNomeExists(nome, ignoreId: id))
                return ServiceResult<SalaDto>.Fail("Já existe uma sala com esse nome.", 409);

            dto.UpdateSalaModel(sala);

            sala.Nome = nome;

            _repo.UpdateSala(sala);
            return ServiceResult<SalaDto>.Success(sala.ToSalaDto());
        }

        public ServiceResult<object> Delete(int id)
        {
            if (id <= 0)
                return ServiceResult<object>.Fail("Id inválido.", 400);

            var sala = _repo.GetSalaById(id);
            if (sala == null)
                return ServiceResult<object>.Fail("Sala não encontrada.", 404);

            if (_repo.SalaHasSessoes(id))
                return ServiceResult<object>.Fail("Não é possível excluir uma sala que possui sessões vinculadas.", 409);

            _repo.DeleteSala(id);
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
