using MediatR;
using ProductoAPI_GraphQL.Application.Queries;
using ProductoAPI_GraphQL.Domain.Entities;
using ProductoAPI_GraphQL.Domain.Repositories;

namespace ProductoAPI_GraphQL.Application.Queries;

public class GetProductoByIdQueryHandler : IRequestHandler<GetProductoByIdQuery, Producto?>
{
    private readonly IProductoRepository _repository;

    public GetProductoByIdQueryHandler(IProductoRepository repository)
    {
        _repository = repository;
    }

    public async Task<Producto?> Handle(GetProductoByIdQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetByIdAsync(request.Id, cancellationToken);
    }
}

