using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Redarbor.Api.Extensions;
using Redarbor.Application.Common.Results;
using Redarbor.Application.Features.Companies.Commands.Create;
using Redarbor.Application.Features.Companies.Dtos;
using Redarbor.Application.Features.Companies.Queries.GetAll;
using Redarbor.Application.Features.Companies.Queries.GetById;
using Redarbor.Application.Features.Master.Dtos;

namespace Redarbor.Api.Endpoints;

public class Companies : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("api/v1/company")
                       .WithTags(nameof(Companies))
                       .RequireAuthorization()
                       .WithDescription("Gestión de compañías.");

        group.MapPost(string.Empty, Create)
            .Produces<CreateCompanyResponse>(StatusCodes.Status200OK)
            .Produces<ProblemDetails>(StatusCodes.Status400BadRequest)
            .WithDescription("Creación de compañías.");

        group.MapGet("/{companyId:int}", GetById)
            .Produces<MasterDto>(StatusCodes.Status200OK)
            .Produces<ProblemDetails>(StatusCodes.Status400BadRequest)
            .WithDescription("Obtiene una compañía según el id.");

        group.MapGet(string.Empty, GetAll)
            .Produces<IEnumerable<MasterDto>>(StatusCodes.Status200OK)
            .Produces<ProblemDetails>(StatusCodes.Status400BadRequest)
            .WithDescription("Obtiene la lista de compañías.");
    }

    public async Task<IResult> Create([FromBody] CreateCompanyRequest request, ISender sender, HttpContext context)
        => (await Result<CreateCompanyCommand>.Craete(new CreateCompanyCommand(request.Name))
                .Bind(request => sender.Send(request)))
                .ToProblemDetails(context);

    public async Task<IResult> GetById([FromRoute] int companyId, ISender sender, HttpContext context)
        => (await Result<GetCompanyByIdQuery>.Craete(new GetCompanyByIdQuery(companyId))
                .Bind(query => sender.Send(query)))
                .ToProblemDetails(context);

    public async Task<IResult> GetAll(ISender sender, HttpContext context)
        => (await Result<GetAllCompaniesQuery>.Craete(new GetAllCompaniesQuery())
                .Bind(query => sender.Send(query)))
                .ToProblemDetails(context);
}
