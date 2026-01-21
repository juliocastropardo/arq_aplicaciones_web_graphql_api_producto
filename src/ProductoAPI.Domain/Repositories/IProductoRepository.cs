using ProductoAPI_GraphQL.Domain.Entities;

namespace ProductoAPI_GraphQL.Domain.Repositories;

public interface IProductoRepository
{
    Task<Producto?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<IEnumerable<Producto>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Producto> CreateAsync(Producto producto, CancellationToken cancellationToken = default);
    Task<Producto> UpdateAsync(Producto producto, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default);
    Task<bool> ExistsAsync(int id, CancellationToken cancellationToken = default);
}

