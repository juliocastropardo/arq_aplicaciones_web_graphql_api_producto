using Microsoft.EntityFrameworkCore;
using ProductoAPI_GraphQL.Domain.Entities;
using ProductoAPI_GraphQL.Domain.Repositories;
using ProductoAPI_GraphQL.Infrastructure.Data;

namespace ProductoAPI_GraphQL.Infrastructure.Repositories;

public class ProductoRepository : IProductoRepository
{
    private readonly ReadDbContext _readContext;
    private readonly WriteDbContext _writeContext;

    public ProductoRepository(ReadDbContext readContext, WriteDbContext writeContext)
    {
        _readContext = readContext;
        _writeContext = writeContext;
    }

    public async Task<Producto?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _readContext.Productos.FindAsync(new object[] { id }, cancellationToken);
    }

    public async Task<IEnumerable<Producto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _readContext.Productos.ToListAsync(cancellationToken);
    }

    public async Task<Producto> CreateAsync(Producto producto, CancellationToken cancellationToken = default)
    {
        _writeContext.Productos.Add(producto);
        await _writeContext.SaveChangesAsync(cancellationToken);
        return producto;
    }

    public async Task<Producto> UpdateAsync(Producto producto, CancellationToken cancellationToken = default)
    {
        _writeContext.Productos.Update(producto);
        await _writeContext.SaveChangesAsync(cancellationToken);
        return producto;
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var producto = await _writeContext.Productos.FindAsync(new object[] { id }, cancellationToken);
        if (producto == null)
        {
            return false;
        }

        _writeContext.Productos.Remove(producto);
        await _writeContext.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task<bool> ExistsAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _readContext.Productos.AnyAsync(p => p.Id == id, cancellationToken);
    }
}

