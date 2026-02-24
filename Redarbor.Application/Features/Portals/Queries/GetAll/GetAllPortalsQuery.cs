using MediatR;
using Redarbor.Application.Common.Results;
using Redarbor.Application.Features.Master.Dtos;

namespace Redarbor.Application.Features.Portals.Queries.GetAll;

public sealed record GetAllPortalsQuery : IRequest<Result<IEnumerable<MasterDto>>>;