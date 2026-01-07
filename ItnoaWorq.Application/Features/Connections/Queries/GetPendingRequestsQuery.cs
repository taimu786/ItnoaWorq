using ItnoaWorq.Application.Common.DTOs;
using MediatR;

namespace ItnoaWorq.Application.Features.Connections.Queries;

public record GetPendingRequestsQuery(Guid UserId) : IRequest<List<ConnectionRequestDto>>;
