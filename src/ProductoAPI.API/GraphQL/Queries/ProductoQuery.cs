using HotChocolate;
using ProductoAPI_GraphQL.Domain.Entities;
using ProductoAPI_GraphQL.Domain.Repositories;

namespace ProductoAPI_GraphQL.API.GraphQL.Queries;

public class ProductoQuery
{
    /// <summary>
    /// Obtiene todos los productos
    /// </summary>
    public async Task<IEnumerable<Producto>> GetProductos(
        [Service] IProductoRepository repository,
        CancellationToken cancellationToken)
    {
        return await repository.GetAllAsync(cancellationToken);
    }

    /// <summary>
    /// Obtiene un producto por su ID
    /// </summary>
    public async Task<Producto?> GetProductoById(
        int id,
        [Service] IProductoRepository repository,
        CancellationToken cancellationToken)
    {
        return await repository.GetByIdAsync(id, cancellationToken);
    }
}
