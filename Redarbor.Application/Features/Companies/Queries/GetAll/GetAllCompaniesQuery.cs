using MediatR;
using Redarbor.Application.Common.Results;
using Redarbor.Application.Features.Master.Dtos;

namespace Redarbor.Application.Features.Companies.Queries.GetAll;

public sealed record GetAllCompaniesQuery : IRequest<Result<IEnumerable<MasterDto>>>;