using GeFinbeta.Entities;
using Microsoft.EntityFrameworkCore;

namespace GeFinbeta.Data
{
    public class GefinContext : DbContext
    {
        public GefinContext()
        {
        }

        public GefinContext(DbContextOptions<GefinContext> options)
            : base(options)
        {
        }

        public DbSet<Gastos> Gastos { get; set; }
        public DbSet<Receita> Receitas { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite("Data Source=gefin.db");
            }
        }
    }
}