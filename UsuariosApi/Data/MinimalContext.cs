using Microsoft.EntityFrameworkCore;

namespace UsuariosApi.Data
{
    public class MinimalContext : DbContext
    {
        public MinimalContext(DbContextOptions<MinimalContext> options) : base(options)
        {
        }

        public DbSet<Usuario> Usuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuario>()
                .HasKey(x => x.Id);

            modelBuilder.Entity<Usuario>()
                .Property(x => x.Email)
                .IsRequired()
                .HasColumnType("varchar(100)");

            modelBuilder.Entity<Usuario>()
                .Property(x => x.Senha)
                .IsRequired()
                .HasColumnType("varchar(100)");

            base.OnModelCreating(modelBuilder);
        }
    }
}
