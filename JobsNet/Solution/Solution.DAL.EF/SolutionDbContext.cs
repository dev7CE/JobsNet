using Microsoft.EntityFrameworkCore;
using Solution.DO.Objects;

namespace Solution.DAL.EF
{
    public partial class SolutionDbContext : DbContext
    {
        public SolutionDbContext() { }

        public SolutionDbContext(DbContextOptions<SolutionDbContext> options)
            : base(options)
        { }

        public virtual DbSet<Provincias> Provincias { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
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
