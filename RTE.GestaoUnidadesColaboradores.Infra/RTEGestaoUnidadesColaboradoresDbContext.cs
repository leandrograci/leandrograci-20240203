using Microsoft.EntityFrameworkCore;
using RTE.GestaoUnidadesColaboradores.Domain.Entities;

namespace RTE.GestaoUnidadesColaboradores.Infra
{
    public class RTEGestaoUnidadesColaboradoresDbContext : DbContext
    {
        public DbSet<UsuarioEntity> Usuarios { get; set; }
        public DbSet<ColaboradorEntity> Colaboradores { get; set; }
        public DbSet<UnidadeEntity> Unidades { get; set; }

        public RTEGestaoUnidadesColaboradoresDbContext(DbContextOptions<RTEGestaoUnidadesColaboradoresDbContext> options)
        : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UnidadeEntity>().HasIndex(unidade => unidade.Codigo).IsUnique();

            modelBuilder.Entity<ColaboradorEntity>()
                .HasOne(colaborador => colaborador.Usuario)
                .WithOne(usuario => usuario.Colaborador)
                .HasForeignKey<ColaboradorEntity>(colaborador => colaborador.UsuarioId);

            modelBuilder.Entity<ColaboradorEntity>()
                .HasOne(colaborador => colaborador.Unidade)
                .WithMany(unidade => unidade.Colaboradores)
                .HasForeignKey(colaborador => colaborador.UnidadeId);
        }

    }
}   
