using MediatR;

namespace ProductoAPI_GraphQL.Application.Commands;

public class DeleteProductoCommand : IRequest<bool>
{
    public int Id { get; set; }
}

