using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Redarbor.Api.Extensions;
using Redarbor.Application.Common.Results;
using Redarbor.Application.Features.Roles.Commands.Create;
using Redarbor.Application.Features.Roles.Dtos;
using Redarbor.Application.Features.Roles.Queries.GetAll;
using Redarbor.Application.Features.Roles.Queries.GetById;

namespace Redarbor.Api.Endpoints;

public class Roles : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("api/v1/role")
                       .WithTags(nameof(Roles))
                       .WithDescription("Gestión de roles");

        group
            .MapPost(string.Empty, Create)
            .Produces<Guid>(StatusCodes.Status200OK)
            .Produces<ProblemDetails>(StatusCodes.Status400BadRequest)
            .WithDescription("Creación de roles");

        group.MapGet("/{roleId:guid}", GetById)
            .Produces<RoleDto>(StatusCodes.Status200OK)
            .Produces<ProblemDetails>(StatusCodes.Status400BadRequest)
            .WithDescription("Obtiene un role según el id.");

        group.MapGet(string.Empty, GetAll)
            .Produces<IEnumerable<RoleDto>>(StatusCodes.Status200OK)
            .Produces<ProblemDetails>(StatusCodes.Status400BadRequest)
            .WithDescription("Obtiene la lista de roles creados.");
    }

    public async Task<IResult> Create([FromBody] CreateRoleRequest request, ISender sender, HttpContext context)
        => (await Result<CreateRoleCommand>.Craete(new CreateRoleCommand(request))
                .Bind(command => sender.Send(command)))
                .ToProblemDetails(context);

    public async Task<IResult> GetById([FromRoute] Guid roleId, ISender sender, HttpContext context)
        => (await Result<GetRoleByIdQuery>.Craete(new GetRoleByIdQuery(roleId))
                .Bind(query => sender.Send(query)))
                .ToProblemDetails(context);

    public async Task<IResult> GetAll(ISender sender, HttpContext context)
        => (await Result<GetAllRolesQuery>.Craete(new GetAllRolesQuery())
                .Bind(query => sender.Send(query)))
                .ToProblemDetails(context);
}
