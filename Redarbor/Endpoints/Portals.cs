using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Redarbor.Api.Extensions;
using Redarbor.Application.Common.Results;
using Redarbor.Application.Features.Master.Dtos;
using Redarbor.Application.Features.Portals.Commands.Create;
using Redarbor.Application.Features.Portals.Dtos;
using Redarbor.Application.Features.Portals.Queries.GetAll;
using Redarbor.Application.Features.Portals.Queries.GetById;

namespace Redarbor.Api.Endpoints;

public class Portals : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("api/v1/portal")
                       .WithTags(nameof(Portals))
                       .RequireAuthorization()
                       .WithDescription("Gestión de portales.");

        group.MapPost(string.Empty, Create)
            .Produces<CreatePortalResponse>(StatusCodes.Status200OK)
            .Produces<ProblemDetails>(StatusCodes.Status400BadRequest)
            .WithDescription("Creación de portales.");

        group.MapGet("/{portalId:int}", GetById)
            .Produces<MasterDto>(StatusCodes.Status200OK)
            .Produces<ProblemDetails>(StatusCodes.Status400BadRequest)
            .WithDescription("Obtiene un portal según el id.");

        group.MapGet(string.Empty, GetAll)
            .Produces<IEnumerable<MasterDto>>(StatusCodes.Status200OK)
            .Produces<ProblemDetails>(StatusCodes.Status400BadRequest)
            .WithDescription("Obtiene la lista de portales.");
    }

    public async Task<IResult> Create([FromBody] CreatePortalRequest request, ISender sender, HttpContext context)
        => (await Result<CreatePortalCommand>.Craete(new CreatePortalCommand(request.Name))
                .Bind(request => sender.Send(request)))
                .ToProblemDetails(context);

    public async Task<IResult> GetById([FromRoute] int portalId, ISender sender, HttpContext context)
        => (await Result<GePortalByIdQuery>.Craete(new GePortalByIdQuery(portalId))
            .Bind(query => sender.Send(query)))
            .ToProblemDetails(context);

    public async Task<IResult> GetAll(ISender sender, HttpContext context)
        => (await Result<GetAllPortalsQuery>.Craete(new GetAllPortalsQuery())
            .Bind(query => sender.Send(query)))
            .ToProblemDetails(context);
}
