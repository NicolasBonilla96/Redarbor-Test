using FluentValidation;

namespace Redarbor.Application.Features.States.Commands;

public class CreateStateCommandValidator : AbstractValidator<CreateStateCommand>
{
    public CreateStateCommandValidator()
    {
        RuleFor(d => d.Name)
            .NotEmpty()
            .WithMessage("El nombre para el estado es obligatorio.");
    }
}
