using CustomerManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CustomerManagement.Infra.Contexts
{
    public class DataContext : DbContext
    {
        public DbSet<Cliente> Clientes { get; set; }
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cliente>()
            .HasKey(c => c.Id);

            modelBuilder.Entity<Cliente>()
            .Property(c => c.Nome)
            .HasMaxLength(100)
            .IsRequired();

            modelBuilder.Entity<Cliente>()
            .Property(c => c.Idade)
            .HasColumnType("int")
            .IsRequired();
        }
    }
}