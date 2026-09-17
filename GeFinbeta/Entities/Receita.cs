namespace GeFinbeta.Entities
{
    public class Receita
    {
        public int Id { get; set; }

        public string Descricao { get; set; } = string.Empty;

        public decimal Valor { get; set; }

        public DateTime Data { get; set; }
    }
}