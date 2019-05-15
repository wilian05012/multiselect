using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace sampledbDAL {
    public partial class SampleDbContext : DbContext {
        public SampleDbContext() {
        }

        public SampleDbContext(DbContextOptions<SampleDbContext> options)
            : base(options) {
        }

        public virtual DbSet<County> Counties { get; set; }
        public virtual DbSet<Hurricane> Hurricanes { get; set; }
        public virtual DbSet<Affectation> Affectations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.4-servicing-10062");

            modelBuilder.Entity<County>(entity => {
                entity.ToTable("lktCounties");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Hurricane>(entity => {
                entity.ToTable("tblHurricanes");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.LandfallDate).HasColumnType("date");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Affectation>(entity => {
                entity.HasKey(e => new { e.HurricaneId, e.CountyId })
                    .HasName("PK_xtblHurricaneCounties");

                entity.ToTable("xtblHurricanCounties");

                entity.HasOne(d => d.County)
                    .WithMany(p => p.Affectations)
                    .HasForeignKey(d => d.CountyId)
                    .HasConstraintName("FK_xtblHurricaneCounties_tblCounties");

                entity.HasOne(d => d.Hurricane)
                    .WithMany(p => p.Affectations)
                    .HasForeignKey(d => d.HurricaneId)
                    .HasConstraintName("FK_xtblHurricaneCounties_tblHurricanes");
            });
        }
    }
}
