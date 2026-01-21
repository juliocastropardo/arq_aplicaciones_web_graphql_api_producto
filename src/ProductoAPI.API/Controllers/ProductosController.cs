using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProductoAPI_GraphQL.Application.Commands;
using ProductoAPI_GraphQL.Application.Queries;
using ProductoAPI_GraphQL.Domain.Entities;
using FluentValidation;

namespace ProductoAPI_GraphQL.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductosController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<ProductosController> _logger;

    public ProductosController(IMediator mediator, ILogger<ProductosController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    /// <summary>
    /// Obtiene todos los productos
    /// </summary>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<Producto>>> GetAll()
    {
        try
        {
            var query = new GetAllProductosQuery();
            var productos = await _mediator.Send(query);
            return Ok(productos);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al obtener todos los productos");
            return StatusCode(500, new { message = "Error interno del servidor", error = ex.Message });
        }
    }

    /// <summary>
    /// Obtiene un producto por su ID
    /// </summary>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Producto>> GetById(int id)
    {
        try
        {
            var query = new GetProductoByIdQuery { Id = id };
            var producto = await _mediator.Send(query);

            if (producto == null)
            {
                return NotFound(new { message = $"Producto con ID {id} no encontrado" });
            }

            return Ok(producto);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al obtener el producto con ID {ProductoId}", id);
            return StatusCode(500, new { message = "Error interno del servidor", error = ex.Message });
        }
    }

    /// <summary>
    /// Crea un nuevo producto
    /// </summary>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Producto>> Create([FromBody] CreateProductoCommand command)
    {
        try
        {
            var producto = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id = producto.Id }, producto);
        }
        catch (ValidationException ex)
        {
            _logger.LogWarning(ex, "Error de validación al crear producto");
            return BadRequest(new { message = "Error de validación", errors = ex.Errors });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al crear producto");
            return StatusCode(500, new { message = "Error interno del servidor", error = ex.Message });
        }
    }

    /// <summary>
    /// Actualiza un producto existente
    /// </summary>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Producto>> Update(int id, [FromBody] UpdateProductoCommand command)
    {
        try
        {
            if (id != command.Id)
            {
                return BadRequest(new { message = "El ID de la URL no coincide con el ID del cuerpo de la solicitud" });
            }

            var producto = await _mediator.Send(command);
            return Ok(producto);
        }
        catch (KeyNotFoundException ex)
        {
            _logger.LogWarning(ex, "Producto no encontrado para actualizar");
            return NotFound(new { message = ex.Message });
        }
        catch (ValidationException ex)
        {
            _logger.LogWarning(ex, "Error de validación al actualizar producto");
            return BadRequest(new { message = "Error de validación", errors = ex.Errors });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al actualizar producto con ID {ProductoId}", id);
            return StatusCode(500, new { message = "Error interno del servidor", error = ex.Message });
        }
    }

    /// <summary>
    /// Elimina un producto
    /// </summary>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var command = new DeleteProductoCommand { Id = id };
            var result = await _mediator.Send(command);

            if (!result)
            {
                return NotFound(new { message = $"Producto con ID {id} no encontrado" });
            }

            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            _logger.LogWarning(ex, "Producto no encontrado para eliminar");
            return NotFound(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al eliminar producto con ID {ProductoId}", id);
            return StatusCode(500, new { message = "Error interno del servidor", error = ex.Message });
        }
    }
}

