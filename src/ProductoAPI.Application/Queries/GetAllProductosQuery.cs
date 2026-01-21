using MediatR;
using ProductoAPI_GraphQL.Domain.Entities;

namespace ProductoAPI_GraphQL.Application.Queries;

public class GetAllProductosQuery : IRequest<IEnumerable<Producto>>
{
}

