using FluentValidation;

namespace Redarbor.Application.Features.Users.Commands.Create;

public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        RuleFor(d => d.User.UserName)
            .NotEmpty()
            .WithMessage("El nombre de usuario es obligatorio.")
            .MaximumLength(30)
            .WithMessage("El nombre de usuario no puede ser mayor a 30 caracteres.");

        RuleFor(d => d.User.Password)
            .NotEmpty()
            .WithMessage("La contraseña es obligatoria.")
            .MinimumLength(8)
            .WithMessage("La contraseña debe ser de mínimo 8 carácteres.")
            .MaximumLength(15)
            .WithMessage("La contraseña debe ser de máximo 15 carácteres.")
            .Matches("[A-Z]")
            .WithMessage("La contraseña debe contar con al menos una letra mayúscula.")
            .Matches("[a-z]")
            .WithMessage("La contraseña debe contar con al menos una letra minúscula.")
            .Matches("[0-9]")
            .WithMessage("La contraseña debe contar con al menos un número.")
            .Matches("[^a-zA-Z0-9]")
            .WithMessage("La contraseña debe contar con al menos un carácter especial.");

        RuleFor(d => d.User.Email)
            .NotEmpty()
            .WithMessage("El correo es obligatorio.")
            .EmailAddress()
            .WithMessage("El correo debe tener un formato valido.");

        RuleFor(d => d.User.PhoneNumber)
            .NotEmpty()
            .WithMessage("El número de teléfono es obligatorio.")
            .Matches("[0-9]")
            .WithMessage("El número de teléfono solo debe contener números.");
    }
}
