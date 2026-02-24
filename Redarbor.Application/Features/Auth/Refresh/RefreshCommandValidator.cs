using FluentValidation;

namespace Redarbor.Application.Features.Auth.Refresh;

public class RefreshCommandValidator : AbstractValidator<RefreshCommand>
{
    public RefreshCommandValidator()
    {
        RuleFor(d => d.AccessToken)
            .NotEmpty()
            .WithMessage("El token de acceso es obligatorio.");

        RuleFor(d => d.RefreshToken)
            .NotEmpty()
            .WithMessage("El refresh token es obligatorio.");
    }
}
