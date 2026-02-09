using System.ComponentModel.DataAnnotations;

namespace Lumiere.API.Dtos.Ingresso
{
    public class CreateIngressoDto
    {
        [Required(ErrorMessage = "SessaoId é obrigatório")]
        [Range(1, int.MaxValue, ErrorMessage = "SessaoId deve ser maior que 0")]
        public int SessaoId { get; set; }

        [Required(ErrorMessage = "AssentoId é obrigatório")]
        [Range(1, int.MaxValue, ErrorMessage = "AssentoId deve ser maior que 0")]
        public int AssentoId { get; set; }

        [Required(ErrorMessage = "TipoIngressoId é obrigatório")]
        [Range(1, int.MaxValue, ErrorMessage = "TipoIngressoId deve ser maior que 0")]
        public int TipoIngressoId { get; set; }
    }
}