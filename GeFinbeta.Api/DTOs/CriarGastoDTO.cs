using GeFinbeta.Entities.Enums;
using System.ComponentModel.DataAnnotations;

namespace GeFinbeta.Api.DTOs
{
    public class CriarGastoDto
    {
        [Required(ErrorMessage = "A descrição é obrigatória.")]
        [MinLength(2, ErrorMessage = "A descrição deve ter pelo menos 2 caracteres.")]
        public string Descricao { get; set; } = string.Empty;
        [Range(0.01, double.MaxValue,
        ErrorMessage = "O valor deve ser maior que zero.")]
        public decimal Valor { get; set; }

        public DateTime Data { get; set; }

        [EnumDataType(typeof(Categoria),
            ErrorMessage = "Categoria inválida.")]
        public Categoria Categoria { get; set; }
    }
}