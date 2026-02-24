using MediatR;
using Redarbor.Application.Common.Results;
using Redarbor.Application.Core.Abstractions.Interfaces;
using Redarbor.Application.Features.Master.Dtos;
using System.Net;

namespace Redarbor.Application.Features.Companies.Queries.GetAll;

public sealed class GetAllCompaniesQueryHandler(
        ICompanyQueryRepository query
    ) : IRequestHandler<GetAllCompaniesQuery, Result<IEnumerable<MasterDto>>>
{
    private readonly ICompanyQueryRepository _query = query;

    public async Task<Result<IEnumerable<MasterDto>>> Handle(GetAllCompaniesQuery request, CancellationToken cancellationToken)
    {
        var companies = await _query.GetAllAsync();
        return companies.Any()
            ? Result<IEnumerable<MasterDto>>.Success(companies)
            : Result<IEnumerable<MasterDto>>.Failure("No se encontró información de compañías.", (int)HttpStatusCode.NotFound);
    }
}
