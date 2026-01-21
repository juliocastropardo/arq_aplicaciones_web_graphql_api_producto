using MediatR;
using ProductoAPI_GraphQL.Domain.Entities;

namespace ProductoAPI_GraphQL.Application.Commands;

public class CreateProductoCommand : IRequest<Producto>
{
    public string Nombre { get; set; } = string.Empty;
    public string Descripcion { get; set; } = string.Empty;
    public decimal Precio { get; set; }
}

