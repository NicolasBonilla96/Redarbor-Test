using FluentValidation;

namespace Redarbor.Application.Features.Companies.Commands.Create;

public class CreateCompanyCommandValidator : AbstractValidator<CreateCompanyCommand>
{
    public CreateCompanyCommandValidator()
    {
        RuleFor(d => d.Name)
            .NotEmpty()
            .WithMessage("El nombre para la compañía es obligatorio.");
    }
}
