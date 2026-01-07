using ItnoaWorq.Application.Common.DTOs;
using MediatR;

namespace ItnoaWorq.Application.Features.Connections.Queries;

public record GetConnectionsQuery(Guid UserId) : IRequest<List<ConnectionDto>>;
