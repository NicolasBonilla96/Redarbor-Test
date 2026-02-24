using MediatR;
using Redarbor.Application.Common.Results;
using Redarbor.Application.Features.Master.Dtos;

namespace Redarbor.Application.Features.States.Queries.GetAll;

public sealed record GetAllStatesQuery : IRequest<Result<IEnumerable<MasterDto>>>;