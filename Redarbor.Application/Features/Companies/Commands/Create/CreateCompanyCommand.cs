using MediatR;
using Redarbor.Application.Common.Results;
using Redarbor.Application.Features.Companies.Dtos;
using Redarbor.Domain.Entities.Types;

namespace Redarbor.Application.Features.Companies.Commands.Create;

public sealed record CreateCompanyCommand(
        string Name
    ) : IRequest<Result<CreateCompanyResponse>>;