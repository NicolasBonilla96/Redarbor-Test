using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Redarbor.Api.Extensions;
using Redarbor.Application.Common.Results;
using Redarbor.Application.Features.Users.Commands.Create;
using Redarbor.Application.Features.Users.Dtos;
using Redarbor.Application.Features.Users.Queries.GetAll;
using Redarbor.Application.Features.Users.Queries.GetById;

namespace Redarbor.Api.Endpoints;

public class Users : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("api/v1/user")
                       .WithTags(nameof(Users))
                       .RequireAuthorization()
                       .WithDescription("Gestión de usuarios.");

        group.MapPost(string.Empty, Create)
            .Produces<Guid>(StatusCodes.Status200OK)
            .Produces<ProblemDetails>(StatusCodes.Status400BadRequest)
            .WithDescription("Creación de usuarios.");

        group.MapGet("/{userId:guid}", GetById)
            .Produces<UserDto>(StatusCodes.Status200OK)
            .Produces<ProblemDetails>(StatusCodes.Status400BadRequest)
            .WithDescription("Obtiene un usuario según el id.");

        group.MapGet(string.Empty, GetAll)
            .Produces<UserDto>(StatusCodes.Status200OK)
            .Produces<ProblemDetails>(StatusCodes.Status400BadRequest)
            .WithDescription("Obtiene la lista de usuarios creados.");
    }

    public async Task<IResult> Create([FromBody] CreateUserRequest request, ISender sender, HttpContext context)
        => (await Result<CreateUserCommand>.Craete(new CreateUserCommand(request))
                .Bind(command => sender.Send(command)))
                .ToProblemDetails(context);

    public async Task<IResult> GetById([FromRoute] Guid userId, ISender sender, HttpContext context)
        => (await Result<GetUserQuery>.Craete(new GetUserQuery(userId))
                .Bind(query => sender.Send(query)))
                .ToProblemDetails(context);

    public async Task<IResult> GetAll(ISender sender, HttpContext context)
        => (await Result<GetAllUserQuery>.Craete(new GetAllUserQuery())
                .Bind(query => sender.Send(query)))
                .ToProblemDetails(context);
}
