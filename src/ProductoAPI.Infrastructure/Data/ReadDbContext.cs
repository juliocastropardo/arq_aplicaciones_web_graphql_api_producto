using Microsoft.EntityFrameworkCore;
using ProductoAPI_GraphQL.Domain.Entities;

namespace ProductoAPI_GraphQL.Infrastructure.Data;

public class ReadDbContext : DbContext
{
    public ReadDbContext(DbContextOptions<ReadDbContext> options)
        : base(options)
    {
    }

    public DbSet<Producto> Productos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Producto>(entity =>
        {
            entity.ToTable("Productos", "dbo");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id)
                .HasColumnName("Id")
                .ValueGeneratedOnAdd()
                .UseIdentityColumn();
            entity.Property(e => e.Nombre)
                .IsRequired()
                .HasMaxLength(200)
                .HasColumnName("Nombre");
            entity.Property(e => e.Descripcion)
                .IsRequired()
                .HasMaxLength(1000)
                .HasColumnName("Descripcion");
            entity.Property(e => e.Precio)
                .HasColumnType("decimal(18,2)")
                .IsRequired()
                .HasColumnName("Precio");
        });
    }
}

