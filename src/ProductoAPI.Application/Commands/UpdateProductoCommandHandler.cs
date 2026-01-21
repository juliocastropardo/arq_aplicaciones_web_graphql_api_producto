using MediatR;
using ProductoAPI_GraphQL.Application.Commands;
using ProductoAPI_GraphQL.Domain.Entities;
using ProductoAPI_GraphQL.Domain.Repositories;

namespace ProductoAPI_GraphQL.Application.Commands;

public class UpdateProductoCommandHandler : IRequestHandler<UpdateProductoCommand, Producto>
{
    private readonly IProductoRepository _repository;

    public UpdateProductoCommandHandler(IProductoRepository repository)
    {
        _repository = repository;
    }

    public async Task<Producto> Handle(UpdateProductoCommand request, CancellationToken cancellationToken)
    {
        var producto = await _repository.GetByIdAsync(request.Id, cancellationToken);
        
        if (producto == null)
        {
            throw new KeyNotFoundException($"Producto con ID {request.Id} no encontrado.");
        }

        producto.Nombre = request.Nombre;
        producto.Descripcion = request.Descripcion;
        producto.Precio = request.Precio;

        return await _repository.UpdateAsync(producto, cancellationToken);
    }
}

