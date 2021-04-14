using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Solution.API.W.Models
{
    public partial class JobsNetDbContext : DbContext
    {
        public JobsNetDbContext()
        {
        }

        public JobsNetDbContext(DbContextOptions<JobsNetDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Cantones> Cantones { get; set; }
        public virtual DbSet<Documentos> Documentos { get; set; }
        public virtual DbSet<Empresas> Empresas { get; set; }
        public virtual DbSet<ListaOferentes> ListaOferentes { get; set; }
        public virtual DbSet<Oferentes> Oferentes { get; set; }
        public virtual DbSet<Provincias> Provincias { get; set; }
        public virtual DbSet<PuestosTrabajo> PuestosTrabajo { get; set; }
        public virtual DbSet<Usuarios> Usuarios { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("JobsNetConnString");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cantones>(entity =>
            {
                entity.HasKey(e => e.IdCanton)
                    .HasName("PK__Cantones__6DCE0F29984D3C80");

                entity.Property(e => e.NombreCanton).IsUnicode(false);

                entity.HasOne(d => d.IdProvinciaNavigation)
                    .WithMany(p => p.Cantones)
                    .HasForeignKey(d => d.IdProvincia)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PROVINCIAS");
            });

            modelBuilder.Entity<Documentos>(entity =>
            {
                entity.Property(e => e.FileContent).IsFixedLength();

                entity.Property(e => e.Type).IsUnicode(false);

                entity.HasOne(d => d.Usuario)
                    .WithMany(p => p.Documentos)
                    .HasForeignKey(d => d.UserName)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_USUARIO_DOCUEMNTO");
            });

            modelBuilder.Entity<Empresas>(entity =>
            {
                entity.HasKey(e => e.IdEmpresa)
                    .HasName("PK__Empresas__5EF4033EA0D65791");

                entity.Property(e => e.Descripcion).IsUnicode(false);

                entity.Property(e => e.NombreEmpresa).IsUnicode(false);

                entity.Property(e => e.Telefono).IsUnicode(false);

                entity.HasOne(d => d.IdCantonNavigation)
                    .WithMany(p => p.Empresas)
                    .HasForeignKey(d => d.IdCanton)
                    .HasConstraintName("FK_CANTON");

                entity.HasOne(d => d.UserNameNavigation)
                    .WithMany(p => p.Empresas)
                    .HasForeignKey(d => d.UserName)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_USUARIO_EMPRESA");
            });

            modelBuilder.Entity<ListaOferentes>(entity =>
            {
                entity.HasKey(e => new { e.IdOferente, e.IdPuesto })
                    .HasName("PK__ListaOfe__A1C3BA08AF628CDD");

                entity.Property(e => e.Descartado).HasDefaultValueSql("((0))");

                entity.HasOne(d => d.IdOferenteNavigation)
                    .WithMany(p => p.ListaOferentes)
                    .HasForeignKey(d => d.IdOferente)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ID_OFERENTE");

                entity.HasOne(d => d.IdPuestoNavigation)
                    .WithMany(p => p.ListaOferentes)
                    .HasForeignKey(d => d.IdPuesto)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ID_PUESTO");
            });

            modelBuilder.Entity<Oferentes>(entity =>
            {
                entity.HasKey(e => e.IdOferente)
                    .HasName("PK__Oferente__7B197CB1396F3383");

                entity.Property(e => e.Apellido1).IsUnicode(false);

                entity.Property(e => e.Apellido2).IsUnicode(false);

                entity.Property(e => e.Nombre).IsUnicode(false);

                entity.Property(e => e.Telefono).IsUnicode(false);

                entity.HasOne(d => d.UserNameNavigation)
                    .WithMany(p => p.Oferentes)
                    .HasForeignKey(d => d.UserName)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_USUARIO_OFERENTE");
            });

            modelBuilder.Entity<Provincias>(entity =>
            {
                entity.HasKey(e => e.IdProvincia)
                    .HasName("PK__Provinci__EED74455C86E2F8A");

                entity.Property(e => e.NombreProvincia).IsUnicode(false);
            });

            modelBuilder.Entity<PuestosTrabajo>(entity =>
            {
                entity.HasKey(e => e.IdPuesto)
                    .HasName("PK__PuestosT__ADAC6B9C335B449B");

                entity.Property(e => e.Descripcion).IsUnicode(false);

                entity.Property(e => e.FechaPublicacion).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Requisitos).IsUnicode(false);

                entity.Property(e => e.Titulo).IsUnicode(false);

                entity.HasOne(d => d.IdEmpresaNavigation)
                    .WithMany(p => p.PuestosTrabajo)
                    .HasForeignKey(d => d.IdEmpresa)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EMPRESA_ID");
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
