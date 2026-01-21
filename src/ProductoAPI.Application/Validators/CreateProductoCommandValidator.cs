using FluentValidation;
using ProductoAPI_GraphQL.Application.Commands;

namespace ProductoAPI_GraphQL.Application.Validators;

public class CreateProductoCommandValidator : AbstractValidator<CreateProductoCommand>
{
    public CreateProductoCommandValidator()
    {
        RuleFor(x => x.Nombre)
            .NotEmpty().WithMessage("El nombre es requerido")
            .MaximumLength(200).WithMessage("El nombre no puede exceder 200 caracteres");

        RuleFor(x => x.Descripcion)
            .NotEmpty().WithMessage("La descripción es requerida")
            .MaximumLength(1000).WithMessage("La descripción no puede exceder 1000 caracteres");

        RuleFor(x => x.Precio)
            .GreaterThan(0).WithMessage("El precio debe ser mayor a cero");
    }
}

