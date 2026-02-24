using FluentValidation;

namespace Redarbor.Application.Features.Employees.Command.Update;

public class UpdateEmployeeCommandValidator : AbstractValidator<UpdateEmployeeCommand>
{
    public UpdateEmployeeCommandValidator()
    {
        RuleFor(d => d.Employee.EmployeeId)
            .NotEmpty()
            .WithMessage("El id del empleado es obligatorio para actualizar la información.");

        RuleFor(d => d.Employee.CompanyId)
            .NotEmpty()
            .WithMessage("El id de la compañía es obligatorio para el empleado.");

        RuleFor(d => d.Employee.Email)
            .NotEmpty()
            .WithMessage("El correo es obligatorio para el empleado.")
            .EmailAddress()
            .WithMessage("El correo debe tener un formato valido.");

        RuleFor(d => d.Employee.Phone)
            .NotEmpty()
            .WithMessage("El teléfono es obligatorio para el empleado.")
            .MinimumLength(10)
            .MaximumLength(10)
            .WithMessage("El teléfono debe ser de 10 digitos.");

        RuleFor(d => d.Employee.Fax)
            .NotEmpty()
            .WithMessage("El fax es obligatorio para el empleado.")
            .MinimumLength(10)
            .MaximumLength(10)
            .WithMessage("El fax debe contar con 10 carácteres.");

        RuleFor(d => d.Employee.Password)
            .NotEmpty()
            .WithMessage("La contraseña es obligatoria para el empleado.")
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

        RuleFor(d => d.Employee.PortalId)
            .NotEmpty()
            .WithMessage("El id del portal es obligatorio para el empleado.");

        RuleFor(d => d.Employee.StateId)
            .NotEmpty()
            .WithMessage("El id del estado es obligatorio para el empleado.");

        RuleFor(d => d.Employee.RoleId)
            .NotEmpty()
            .WithMessage("El id del rol es obligatorio para el empleado.");

        RuleFor(d => d.Employee.Name)
            .NotEmpty()
            .WithMessage("El nombre es obligatorio para el empleado.");

        RuleFor(d => d.Employee.Username)
            .NotEmpty()
            .WithMessage("El nombre de usuario es obligatorio para el empleado.");
    }
}
