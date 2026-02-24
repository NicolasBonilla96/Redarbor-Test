using MediatR;
using Redarbor.Application.Common.Results;
using Redarbor.Application.Core.Abstractions.Interfaces;
using Redarbor.Application.Features.Master.Dtos;
using System.Net;

namespace Redarbor.Application.Features.Companies.Queries.GetById;

public sealed class GetCompanyByIdQueryHandler(
        ICompanyQueryRepository query
    ) : IRequestHandler<GetCompanyByIdQuery, Result<MasterDto>>
{
    private readonly ICompanyQueryRepository _query = query;

    public async Task<Result<MasterDto>> Handle(GetCompanyByIdQuery request, CancellationToken cancellationToken)
    {
        var company = await _query.FindByIdAsync(request.CompanyId);
        return company is not null
            ? Result<MasterDto>.Success(company)
            : Result<MasterDto>.Failure($"No se encontró ninguna compañía bajo el id '{request.CompanyId}'", (int)HttpStatusCode.NotFound);
    }
}
