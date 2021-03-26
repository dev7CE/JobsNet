using Microsoft.EntityFrameworkCore;
using Solution.DO.Objects;

namespace Solution.DAL.EF
{
    public partial class SolutionDbContext : DbContext
    {
       
        public SolutionDbContext(DbContextOptions<SolutionDbContext> options)
            : base(options)
        { }

        public virtual DbSet<Cantones> Cantones { get; set; }
        public virtual DbSet<Empresas> Empresas { get; set; }
        public virtual DbSet<Provincias> Provincias { get; set; }
        public virtual DbSet<Usuarios> Usuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cantones>(entity =>
            {
                entity.HasKey(e => e.IdCanton)
                    .HasName("PK__Cantones__6DCE0F29984D3C80");

                entity.Property(e => e.NombreCanton).IsUnicode(false);

                entity.HasOne(d => d.Provincia)
                    .WithMany(p => p.Cantones)
                    .HasForeignKey(d => d.IdProvincia)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PROVINCIAS");
            });
            
            modelBuilder.Entity<Empresas>(entity =>
            {
                entity.HasKey(e => e.IdEmpresa)
                    .HasName("PK__Empresas__5EF4033EA0D65791");

                entity.Property(e => e.Descripcion).IsUnicode(false);

                entity.Property(e => e.NombreEmpresa).IsUnicode(false);

                entity.Property(e => e.Telefono).IsUnicode(false);

                entity.HasOne(d => d.Canton)
                    .WithMany(p => p.Empresas)
                    .HasForeignKey(d => d.IdCanton)
                    .HasConstraintName("FK_CANTON");

                entity.HasOne(d => d.Usuario)
                    .WithMany(p => p.Empresas)
                    .HasForeignKey(d => d.UserName)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_USUARIO_EMPRESA");
            });

            modelBuilder.Entity<Provincias>(entity =>
            {
                entity.HasKey(e => e.IdProvincia)
                    .HasName("PK__Provinci__EED74455C86E2F8A");

                entity.Property(e => e.NombreProvincia).IsUnicode(false);
            });
            
            modelBuilder.Entity<Usuarios>(entity =>
            {
                entity.HasKey(e => e.UserName)
                    .HasName("PK__Usuarios__C9F28457FE19CC98");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
