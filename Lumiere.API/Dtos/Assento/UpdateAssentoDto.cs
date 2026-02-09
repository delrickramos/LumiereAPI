using Lumiere.Models;
using System.ComponentModel.DataAnnotations;

namespace Lumiere.API.Dtos.Assento
{
    public class UpdateAssentoDto
    {
        public int Coluna { get; set; }
        public string Fileira { get; set; } = string.Empty;

        [Required]
        public TipoAssentoEnum TipoAssento { get; set; }

        public int SalaId { get; set; }

    }
}
