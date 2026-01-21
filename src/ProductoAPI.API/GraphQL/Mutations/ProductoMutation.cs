using HotChocolate;
using ProductoAPI_GraphQL.Domain.Entities;
using ProductoAPI_GraphQL.Domain.Repositories;

namespace ProductoAPI_GraphQL.API.GraphQL.Mutations;

public class ProductoMutation
{
    /// <summary>
    /// Crea un nuevo producto
    /// </summary>
    public async Task<Producto> CreateProducto(
        string nombre,
        string descripcion,
        decimal precio,
        [Service] IProductoRepository repository,
        CancellationToken cancellationToken)
    {
        var producto = new Producto
        {
            Nombre = nombre,
            Descripcion = descripcion,
            Precio = precio
        };

        return await repository.CreateAsync(producto, cancellationToken);
    }

    /// <summary>
    /// Actualiza un producto existente
    /// </summary>
    public async Task<Producto> UpdateProducto(
        int id,
        string nombre,
        string descripcion,
        decimal precio,
        [Service] IProductoRepository repository,
        CancellationToken cancellationToken)
    {
        var producto = await repository.GetByIdAsync(id, cancellationToken);
        
        if (producto == null)
        {
            throw new KeyNotFoundException($"Producto con ID {id} no encontrado.");
        }

        producto.Nombre = nombre;
        producto.Descripcion = descripcion;
        producto.Precio = precio;

        return await repository.UpdateAsync(producto, cancellationToken);
    }

    /// <summary>
    /// Elimina un producto
    /// </summary>
    public async Task<bool> DeleteProducto(
        int id,
        [Service] IProductoRepository repository,
        CancellationToken cancellationToken)
    {
        return await repository.DeleteAsync(id, cancellationToken);
    }
}
