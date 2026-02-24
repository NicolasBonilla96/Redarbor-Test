using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Redarbor.Application.Common.Results;
using Redarbor.Application.Core.Abstractions.Database;
using Redarbor.Application.Features.Companies.Dtos;
using Redarbor.Domain.Entities.Types;
using Redarbor.Domain.Interfaces;
using System.Net;

namespace Redarbor.Application.Features.Companies.Commands.Create;

public sealed class CreateCompanyCommandHandler(
        IServiceProvider provider
    ) : IRequestHandler<CreateCompanyCommand, Result<CreateCompanyResponse>>
{
    private readonly ICompanyRepository _companyRepo = provider.GetRequiredService<ICompanyRepository>();
    private readonly IUnitOfWork _unitOfWork = provider.GetRequiredService<IUnitOfWork>();

    public async Task<Result<CreateCompanyResponse>> Handle(CreateCompanyCommand request, CancellationToken cancellationToken)
    {
        if(await _companyRepo.AnyAsync(d => d.Name.Equals(request.Name), cancellationToken))
            return Result<CreateCompanyResponse>.Failure($"Ya existe una compañía creada bajo el nombre '{request.Name}'", (int)HttpStatusCode.Conflict);

        var newCompany = Company.CreateCompany(request.Name);
        _companyRepo.Add(newCompany);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<CreateCompanyResponse>.Success(new CreateCompanyResponse { Id = newCompany.Id, Name = newCompany.Name });
    }
}
