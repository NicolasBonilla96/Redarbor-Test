using MediatR;
using Redarbor.Application.Common.Results;
using Redarbor.Application.Features.Master.Dtos;

namespace Redarbor.Application.Features.Companies.Queries.GetById;

public sealed record GetCompanyByIdQuery(
        int CompanyId
    ) : IRequest<Result<MasterDto>>;