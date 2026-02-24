using FluentValidation;

namespace Redarbor.Application.Features.Portals.Commands.Create;

public class CreatePortalCommandValidator : AbstractValidator<CreatePortalCommand>
{
    public CreatePortalCommandValidator()
    {
        RuleFor(d => d.Name)
            .NotEmpty()
            .WithMessage("El nombre para el portal es obligatorio.");
    }
}
