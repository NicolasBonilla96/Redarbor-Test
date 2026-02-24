using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Redarbor.Api.Extensions;
using Redarbor.Application.Common.Results;
using Redarbor.Application.Features.Master.Dtos;
using Redarbor.Application.Features.States.Commands;
using Redarbor.Application.Features.States.Dtos;
using Redarbor.Application.Features.States.Queries.GetAll;
using Redarbor.Application.Features.States.Queries.GetById;

namespace Redarbor.Api.Endpoints;

public class States : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("api/v1/state")
                       .WithTags(nameof(States))
                       .RequireAuthorization()
                       .WithDescription("Gestión de estados.");

        group.MapPost(string.Empty, Create)
            .Produces<CreateStateResponse>(StatusCodes.Status200OK)
            .Produces<ProblemDetails>(StatusCodes.Status400BadRequest)
            .WithDescription("Creación de estados.");

        group.MapGet("/{stateId:int}", GetById)
            .Produces<MasterDto>(StatusCodes.Status200OK)
            .Produces<ProblemDetails>(StatusCodes.Status400BadRequest)
            .WithDescription("Obtiene un estado según el id.");

        group.MapGet(string.Empty, GetAll)
            .Produces<IEnumerable<MasterDto>>(StatusCodes.Status200OK)
            .Produces<ProblemDetails>(StatusCodes.Status400BadRequest)
            .WithDescription("Obtiene la lista de estados.");
    }

    public async Task<IResult> Create([FromBody] CreateStateRequest request, ISender sender, HttpContext context)
        => (await Result<CreateStateCommand>.Craete(new CreateStateCommand(request.Name))
                .Bind(request => sender.Send(request)))
                .ToProblemDetails(context);

    public async Task<IResult> GetById([FromRoute] int stateId, ISender sender, HttpContext context)
        => (await Result<GetStateByIdQuery>.Craete(new GetStateByIdQuery(stateId))
                .Bind(query => sender.Send(query)))
                .ToProblemDetails(context);

    public async Task<IResult> GetAll(ISender sender, HttpContext context)
        => (await Result<GetAllStatesQuery>.Craete(new GetAllStatesQuery())
                .Bind(query => sender.Send(query)))
                .ToProblemDetails(context);
}
