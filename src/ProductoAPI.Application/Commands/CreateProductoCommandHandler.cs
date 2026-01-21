using MediatR;
using ProductoAPI_GraphQL.Application.Commands;
using ProductoAPI_GraphQL.Domain.Entities;
using ProductoAPI_GraphQL.Domain.Repositories;

namespace ProductoAPI_GraphQL.Application.Commands;

public class CreateProductoCommandHandler : IRequestHandler<CreateProductoCommand, Producto>
{
    private readonly IProductoRepository _repository;

    public CreateProductoCommandHandler(IProductoRepository repository)
    {
        _repository = repository;
    }

    public async Task<Producto> Handle(CreateProductoCommand request, CancellationToken cancellationToken)
    {
        var producto = new Producto
        {
            Nombre = request.Nombre,
            Descripcion = request.Descripcion,
            Precio = request.Precio
        };

        return await _repository.CreateAsync(producto, cancellationToken);
    }
}

