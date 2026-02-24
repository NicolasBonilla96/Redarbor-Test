using FluentValidation;

namespace Redarbor.Application.Features.Auth.Login;

public class LoginCommandValidator : AbstractValidator<LoginCommand>
{
    public LoginCommandValidator()
    {
        RuleFor(d => d.Login.UserName)
            .NotEmpty()
            .WithMessage("El nombre de usuario es obligatorio.");

        RuleFor(d => d.Login.Password)
            .NotEmpty()
            .WithMessage("La contraseña es obligatoria.");
    }
}
