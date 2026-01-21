using MediatR;
using ProductoAPI_GraphQL.Domain.Entities;

namespace ProductoAPI_GraphQL.Application.Queries;

public class GetProductoByIdQuery : IRequest<Producto?>
{
    public int Id { get; set; }
}

