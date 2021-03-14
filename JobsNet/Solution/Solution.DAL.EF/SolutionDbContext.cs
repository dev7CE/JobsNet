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
        public virtual DbSet<Provincias> Provincias { get; set; }

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
            
            modelBuilder.Entity<Provincias>(entity =>
            {
                entity.HasKey(e => e.IdProvincia)
                    .HasName("PK__Provinci__EED74455C86E2F8A");

                entity.Property(e => e.NombreProvincia).IsUnicode(false);
            });
            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
