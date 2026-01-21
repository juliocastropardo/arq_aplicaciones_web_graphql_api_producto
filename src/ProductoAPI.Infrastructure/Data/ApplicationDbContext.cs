using Microsoft.EntityFrameworkCore;
using ProductoAPI_GraphQL.Domain.Entities;

namespace ProductoAPI_GraphQL.Infrastructure.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Producto> Productos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Producto>(entity =>
        {
            entity.ToTable("Productos", "dbo"); // Especificar esquema explícitamente
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id)
                .HasColumnName("Id")
                .ValueGeneratedOnAdd()
                .UseIdentityColumn(); // Asegurar que sea IDENTITY en SQL Server
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

