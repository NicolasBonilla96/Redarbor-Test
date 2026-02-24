using FluentValidation;

namespace Redarbor.Application.Features.Employees.Command.Delete;

public class DeleteEmployeCommandValidator : AbstractValidator<DeleteEmployeeCommand>
{
    public DeleteEmployeCommandValidator()
    {
        RuleFor(d => d.EmployeeId)
            .NotEmpty()
            .WithMessage("El id es obligatorio.");
    }
}
