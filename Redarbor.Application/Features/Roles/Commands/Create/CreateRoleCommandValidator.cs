using FluentValidation;

namespace Redarbor.Application.Features.Roles.Commands.Create;

public class CreateRoleCommandValidator : AbstractValidator<CreateRoleCommand>
{
    public CreateRoleCommandValidator()
    {
        RuleFor(d => d.Role.Name)
            .NotEmpty()
            .WithMessage("El nombre del rol es obligatorio.")
            .MaximumLength(20)
            .WithMessage("El nombre del rol debe tener máximo 20 carácteres.");

        RuleFor(d => d.Role.Description)
            .NotEmpty()
            .WithMessage("La descripción para el rol es obligatora.")
            .MaximumLength(150)
            .WithMessage("La descripción debe tener máximo 150 carácteres.");
    }
}
