using MediatR;
using ProductoAPI_GraphQL.Application.Commands;
using ProductoAPI_GraphQL.Domain.Repositories;

namespace ProductoAPI_GraphQL.Application.Commands;

public class DeleteProductoCommandHandler : IRequestHandler<DeleteProductoCommand, bool>
{
    private readonly IProductoRepository _repository;

    public DeleteProductoCommandHandler(IProductoRepository repository)
    {
        _repository = repository;
    }

    public async Task<bool> Handle(DeleteProductoCommand request, CancellationToken cancellationToken)
    {
        var exists = await _repository.ExistsAsync(request.Id, cancellationToken);
        
        if (!exists)
        {
            throw new KeyNotFoundException($"Producto con ID {request.Id} no encontrado.");
        }

        return await _repository.DeleteAsync(request.Id, cancellationToken);
    }
}

