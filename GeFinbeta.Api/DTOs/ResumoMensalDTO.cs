namespace GeFinbeta.Api.DTOs
{
    public class ResumoMensalDTO
    {
        public int Mes { get; set; }

        public int Ano { get; set; }

        public decimal TotalGasto { get; set; }

        public int QuantidadeGastos { get; set; }

        public decimal TotalReceitas { get; set; }

        public int QuantidadeReceitas { get; set; }

        public decimal Saldo { get; set; }

        public string? MaiorCategoria { get; set; }

        public decimal ValorMaiorCategoria { get; set; }
    }
}