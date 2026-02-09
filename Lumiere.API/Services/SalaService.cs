using Lumiere.API.Dtos.Sala;
using Lumiere.API.Interfaces;
using Lumiere.API.Mappers;

namespace Lumiere.API.Services
{
    public class SalaService : ISalaService
    {
        private readonly ISalaRepository _repo;

        private const int NomeMin = 2;
        private const int NomeMax = 60;

        private const int CapacidadeMin = 1;
        private const int CapacidadeMax = 1000;

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
                return ServiceResult<SalaDto>.Fail("Id inválido.");

            var sala = _repo.GetSalaById(id);
            if (sala == null)
                return ServiceResult<SalaDto>.Fail("Sala não encontrada.");

            return ServiceResult<SalaDto>.Success(sala.ToSalaDto());
        }

        public ServiceResult<SalaDto> Create(CreateSalaDto dto)
        {
            var nome = (dto.Nome ?? "").Trim();
            var tipo = (dto.Tipo ?? "").Trim();

            var nomeVal = ValidateTexto("Nome", nome, NomeMin, NomeMax);
            if (nomeVal != null) return ServiceResult<SalaDto>.Fail(nomeVal);

            var tipoVal = ValidateTipo(tipo);
            if (tipoVal != null) return ServiceResult<SalaDto>.Fail(tipoVal);

            var capVal = ValidateCapacidade(dto.Capacidade);
            if (capVal != null) return ServiceResult<SalaDto>.Fail(capVal);

            if (_repo.SalaNomeExists(nome))
                return ServiceResult<SalaDto>.Fail("Já existe uma sala com esse nome.");

            var sala = dto.ToSalaModel();
            sala.Nome = nome;
            sala.Tipo = tipo;

            _repo.AddSala(sala);
            return ServiceResult<SalaDto>.Success(sala.ToSalaDto());
        }

        public ServiceResult<SalaDto> Update(int id, UpdateSalaDto dto)
        {
            if (id <= 0)
                return ServiceResult<SalaDto>.Fail("Id inválido.");

            var sala = _repo.GetSalaById(id);
            if (sala == null)
                return ServiceResult<SalaDto>.Fail("Sala não encontrada.");

            var nome = (dto.Nome ?? "").Trim();
            var tipo = (dto.Tipo ?? "").Trim();

            var nomeVal = ValidateTexto("Nome", nome, NomeMin, NomeMax);
            if (nomeVal != null) return ServiceResult<SalaDto>.Fail(nomeVal);

            var tipoVal = ValidateTipo(tipo);
            if (tipoVal != null) return ServiceResult<SalaDto>.Fail(tipoVal);

            var capVal = ValidateCapacidade(dto.Capacidade);
            if (capVal != null) return ServiceResult<SalaDto>.Fail(capVal);

            if (_repo.SalaNomeExists(nome, ignoreId: id))
                return ServiceResult<SalaDto>.Fail("Já existe uma sala com esse nome.");

            dto.UpdateSalaModel(sala);

            sala.Nome = nome;
            sala.Tipo = tipo;

            _repo.UpdateSala(sala);
            return ServiceResult<SalaDto>.Success(sala.ToSalaDto());
        }

        public ServiceResult<object> Delete(int id)
        {
            if (id <= 0)
                return ServiceResult<object>.Fail("Id inválido.");

            var sala = _repo.GetSalaById(id);
            if (sala == null)
                return ServiceResult<object>.Fail("Sala não encontrada.");

            if (_repo.SalaHasSessoes(id))
                return ServiceResult<object>.Fail("Não é possível excluir uma sala que possui sessões vinculadas.");

            _repo.DeleteSala(id);
            return ServiceResult<object>.Success(new { });
        }

        private string? ValidateTexto(string campo, string valor, int min, int max)
        {
            if (string.IsNullOrWhiteSpace(valor))
                return $"{campo} é obrigatório.";

            if (valor.Length < min || valor.Length > max)
                return $"{campo} deve ter entre {min} e {max} caracteres.";

            return null;
        }

        private string? ValidateCapacidade(int capacidade)
        {
            if (capacidade < CapacidadeMin || capacidade > CapacidadeMax)
                return $"Capacidade deve estar entre {CapacidadeMin} e {CapacidadeMax}.";
            return null;
        }

        private string? ValidateTipo(string tipo)
        {
            if (string.IsNullOrWhiteSpace(tipo))
                return "Tipo é obrigatório.";

            if (tipo.Length < 2 || tipo.Length > 20)
                return "Tipo inválido.";

            return null;
        }
    }
}
