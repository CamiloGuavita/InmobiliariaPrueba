using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Infraestructura.Persistencia.Modelos;

#nullable disable

namespace Infraestructura.Persistencia
{
    public partial class InmobiliariaContext : DbContext
    {
        public InmobiliariaContext()
        {
        }

        public InmobiliariaContext(DbContextOptions<InmobiliariaContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Owner> Owners { get; set; }
        public virtual DbSet<Property> Properties { get; set; }
        public virtual DbSet<PropertyImage> PropertyImages { get; set; }
        public virtual DbSet<PropertyTrace> PropertyTraces { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS_2019;Database=Inmobiliaria;User Id=prueba;Password=prueba;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Modern_Spanish_CI_AS");

            modelBuilder.Entity<Owner>(entity =>
            {
                entity.HasKey(e => e.IdOwner);

                entity.ToTable("Owner");

                entity.Property(e => e.Address)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Birthday).HasColumnType("date");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Photo)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Property>(entity =>
            {
                entity.HasKey(e => e.IdProperty);

                entity.ToTable("Property");

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.CodeInternal)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Price).HasColumnType("numeric(18, 2)");

                entity.HasOne(d => d.IdOwnerNavigation)
                    .WithMany(p => p.Properties)
                    .HasForeignKey(d => d.IdOwner)
                    .HasConstraintName("FK_Property_Owner");
            });

            modelBuilder.Entity<PropertyImage>(entity =>
            {
                entity.HasKey(e => e.IdPropetryImage);

                entity.ToTable("PropertyImage");

                entity.Property(e => e.File)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdPropertyNavigation)
                    .WithMany(p => p.PropertyImages)
                    .HasForeignKey(d => d.IdProperty)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PropertyImage_Property");
            });

            modelBuilder.Entity<PropertyTrace>(entity =>
            {
                entity.HasKey(e => e.IdPropertyTrace);

                entity.ToTable("PropertyTrace");

                entity.Property(e => e.DateSale).HasColumnType("date");

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Tax).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.Value).HasColumnType("numeric(18, 2)");

                entity.HasOne(d => d.IdPropertyNavigation)
                    .WithMany(p => p.PropertyTraces)
                    .HasForeignKey(d => d.IdProperty)
                    .HasConstraintName("FK_PropertyTrace_Property");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
