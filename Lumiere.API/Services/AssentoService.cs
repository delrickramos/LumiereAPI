using Lumiere.API.Dtos.Assento;
using Lumiere.API.Interfaces;
using Lumiere.API.Mappers;
using Lumiere.API.Services;

namespace Lumiere.API.Services
{
    public class AssentoService : IAssentoService
    {
        private readonly IAssentoRepository _assentoRepo;
        private readonly ISalaRepository _salaRepo;

        private const int FileiraMin = 1;
        private const int FileiraMax = 5;
        private const int ColunaMin = 1;
        private const int ColunaMax = 200;

        public AssentoService(IAssentoRepository assentoRepo, ISalaRepository salaRepo)
        {
            _assentoRepo = assentoRepo;
            _salaRepo = salaRepo;
        }

        public ServiceResult<IEnumerable<AssentoDto>> GetAll()
        {
            var assentos = _assentoRepo.GetAssentos().Select(a => a.ToAssentoDto());
            return ServiceResult<IEnumerable<AssentoDto>>.Success(assentos);
        }

        public ServiceResult<AssentoDto> GetById(int id)
        {
            if (id <= 0)
                return ServiceResult<AssentoDto>.Fail("Id inválido.", 400);

            var assento = _assentoRepo.GetAssentoById(id);
            if (assento == null)
                return ServiceResult<AssentoDto>.Fail("Assento não encontrado.", 404);

            return ServiceResult<AssentoDto>.Success(assento.ToAssentoDto());
        }

        public ServiceResult<IEnumerable<AssentoDto>> GetBySala(int salaId)
        {
            if (salaId <= 0)
                return ServiceResult<IEnumerable<AssentoDto>>.Fail("SalaId inválido.", 400);

            if (!_salaRepo.SalaExists(salaId))
                return ServiceResult<IEnumerable<AssentoDto>>.Fail("Sala não encontrada.", 404);

            var assentos = _assentoRepo.GetAssentosBySala(salaId).Select(a => a.ToAssentoDto());
            return ServiceResult<IEnumerable<AssentoDto>>.Success(assentos);
        }

        public ServiceResult<AssentoDto> Create(CreateAssentoDto dto)
        {
            if (dto.SalaId <= 0)
                return ServiceResult<AssentoDto>.Fail("SalaId inválido.", 400);

            if (!_salaRepo.SalaExists(dto.SalaId))
                return ServiceResult<AssentoDto>.Fail("Sala não encontrada.", 404);

            var fileira = (dto.Fileira ?? "").Trim();
            var fileiraVal = ValidateFileira(fileira);
            if (fileiraVal != null) return ServiceResult<AssentoDto>.Fail(fileiraVal, 400);

            var colunaVal = ValidateColuna(dto.Coluna);
            if (colunaVal != null) return ServiceResult<AssentoDto>.Fail(colunaVal, 400);

            if (_assentoRepo.AssentoPosicaoExists(dto.SalaId, fileira, dto.Coluna))
                return ServiceResult<AssentoDto>.Fail("Já existe um assento com essa posição nessa sala.", 409);

            var assento = dto.ToAssentoModel();
            assento.Fileira = fileira;

            _assentoRepo.AddAssento(assento);
            return ServiceResult<AssentoDto>.Success(assento.ToAssentoDto(), 201);
        }

        private static string? ValidateFileira(string fileira)
        {
            if (string.IsNullOrWhiteSpace(fileira))
                return "Fileira é obrigatória.";

            if (fileira.Length < FileiraMin || fileira.Length > FileiraMax)
                return $"Fileira deve ter entre {FileiraMin} e {FileiraMax} caracteres.";

            return null;
        }

        private static string? ValidateColuna(int coluna)
        {
            if (coluna < ColunaMin || coluna > ColunaMax)
                return $"Coluna deve estar entre {ColunaMin} e {ColunaMax}.";

            return null;
        }
    }
}
