namespace RotaViagem.Context
{
    using Microsoft.EntityFrameworkCore;
    using RotaViagemModel.Model;

    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<TabRota> RotasViagem { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TabRota>()
                .Property(r => r.Valor)
                .HasColumnType("decimal(18,2)");
        }

    }
}
