using System.ComponentModel.DataAnnotations;

namespace GeFinbeta.Api.DTOs
{
    public class CriarReceitaDTO
    {
        [Required(ErrorMessage = "A descrição é obrigatória.")]
        [MinLength(2, ErrorMessage = "A descrição deve ter pelo menos 2 caracteres.")]
        public string Descricao { get; set; } = string.Empty;

        [Range(0.01, double.MaxValue,
            ErrorMessage = "O valor deve ser maior que zero.")]
        public decimal Valor { get; set; }

        public DateTime Data { get; set; }
    }
}