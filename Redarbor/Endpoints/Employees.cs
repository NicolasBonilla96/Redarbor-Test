using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Redarbor.Api.Extensions;
using Redarbor.Application.Common.Results;
using Redarbor.Application.Features.Employees.Command.Create;
using Redarbor.Application.Features.Employees.Command.Delete;
using Redarbor.Application.Features.Employees.Command.Update;
using Redarbor.Application.Features.Employees.Dtos;
using Redarbor.Application.Features.Employees.Queries.GetAll;
using Redarbor.Application.Features.Employees.Queries.GetById;

namespace Redarbor.Api.Endpoints;

public class Employees : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("api/v1/employee")
                       .WithTags(nameof(Employees))
                       .RequireAuthorization()
                       .WithDescription("Gestión de empleados.");

        group.MapPost(string.Empty, Create)
            .Produces<CreateEmployeeResponse>(StatusCodes.Status200OK)
            .Produces<ProblemDetails>(StatusCodes.Status400BadRequest)
            .WithDescription("Creación de empleados y usuarios.");

        group.MapPut(string.Empty, Update)
            .Produces(StatusCodes.Status204NoContent)
            .Produces<ProblemDetails>(StatusCodes.Status400BadRequest)
            .WithDescription("Actualización de empleados y usuarios.");

        group.MapDelete("/{employeeId:guid}", Delete)
            .Produces(StatusCodes.Status204NoContent)
            .Produces<ProblemDetails>(StatusCodes.Status400BadRequest)
            .WithDescription("Eliminación de empleados y usuarios.");

        group.MapGet("/{employeeId:guid}", GetById)
            .Produces<EmployeeDto>(StatusCodes.Status200OK)
            .Produces<ProblemDetails>(StatusCodes.Status400BadRequest)
            .WithDescription("Obtiene la información de un usuario por id.");

        group.MapGet(string.Empty, GetAll)
            .Produces<IEnumerable<EmployeeDto>>(StatusCodes.Status200OK)
            .Produces<ProblemDetails>(StatusCodes.Status400BadRequest)
            .WithDescription("Obtiene la información de un usuario por id.");
    }

    public async Task<IResult> Create([FromBody] CreateEmployeeRequest request, ISender sender, HttpContext context)
        => (await Result<CreateEmployeeCommand>.Craete(new CreateEmployeeCommand(request))
                .Bind(request => sender.Send(request)))
                .ToProblemDetails(context);

    public async Task<IResult> Update([FromBody] CreateEmployeeRequest request, ISender sender, HttpContext context)
        => (await Result<UpdateEmployeeCommand>.Craete(new UpdateEmployeeCommand(request))
                .Bind(request => sender.Send(request)))
                .ToProblemDetails(context);

    public async Task<IResult> Delete([FromRoute] Guid employeeId, ISender sender, HttpContext context)
    => (await Result<DeleteEmployeeCommand>.Craete(new DeleteEmployeeCommand(employeeId))
            .Bind(request => sender.Send(request)))
            .ToProblemDetails(context);

    public async Task<IResult> GetById([FromRoute] Guid employeeId, ISender sender, HttpContext context)
    => (await Result<GetEmployeeByIdQuery>.Craete(new GetEmployeeByIdQuery(employeeId))
            .Bind(request => sender.Send(request)))
            .ToProblemDetails(context);

    public async Task<IResult> GetAll(ISender sender, HttpContext context)
    => (await Result<GetAllEmployeeQuery>.Craete(new GetAllEmployeeQuery())
            .Bind(request => sender.Send(request)))
            .ToProblemDetails(context);
}
