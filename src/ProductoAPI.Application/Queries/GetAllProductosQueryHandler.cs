using MediatR;
using ProductoAPI_GraphQL.Application.Queries;
using ProductoAPI_GraphQL.Domain.Entities;
using ProductoAPI_GraphQL.Domain.Repositories;

namespace ProductoAPI_GraphQL.Application.Queries;

public class GetAllProductosQueryHandler : IRequestHandler<GetAllProductosQuery, IEnumerable<Producto>>
{
    private readonly IProductoRepository _repository;

    public GetAllProductosQueryHandler(IProductoRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<Producto>> Handle(GetAllProductosQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetAllAsync(cancellationToken);
    }
}

