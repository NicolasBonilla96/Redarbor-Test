using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Redarbor.Api.Extensions;
using Redarbor.Application.Common.Results;
using Redarbor.Application.Features.Auth.Dtos;
using Redarbor.Application.Features.Auth.Login;
using Redarbor.Application.Features.Auth.Refresh;

namespace Redarbor.Api.Endpoints;

public class Authentication : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("api/v1/auth")
                       .WithTags(nameof(Authentication))
                       .WithDescription("Servicios de autenticación.");

        group.MapPost("/login", Login)
            .Produces<LoginResponse>(StatusCodes.Status200OK)
            .Produces<ProblemDetails>(StatusCodes.Status400BadRequest)
            .WithDescription("Generación del token de autenticación.");

        group.MapPost("/refresh", Refresh)
            .Produces<LoginResponse>(StatusCodes.Status200OK)
            .Produces<ProblemDetails>(StatusCodes.Status400BadRequest)
            .WithDescription("Generación del refresh token.");
    }

    public async Task<IResult> Login([FromBody] LoginRequest request, ISender sender, HttpContext context)
        => (await Result<LoginCommand>.Craete(new LoginCommand(request))
                .Bind(command => sender.Send(command)))
                .ToProblemDetails(context);

    public async Task<IResult> Refresh([FromBody] RefreshRequest request, ISender sender, HttpContext context)
        => (await Result<RefreshCommand>.Craete(new RefreshCommand(request.AccessToken, request.RefreshToken))
                .Bind(command => sender.Send(command)))
                .ToProblemDetails(context);
}
