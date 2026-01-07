using ItnoaWorq.Application.Abstraction.Interfaces;
using ItnoaWorq.Application.Common.DTOs;
using ItnoaWorq.Application.Features.Connections.Queries;
using ItnoaWorq.Domain.Entities;
using ItnoaWorq.Domain.Enums;
using MediatR;

namespace ItnoaWorq.Application.Features.Connections.Handlers;

public class GetConnectionsHandler : IRequestHandler<GetConnectionsQuery, List<ConnectionDto>>
{
    private readonly IUnitOfWork _uow;
    public GetConnectionsHandler(IUnitOfWork uow) => _uow = uow;

    public async Task<List<ConnectionDto>> Handle(GetConnectionsQuery request, CancellationToken cancellationToken)
    {
        var accepted = await _uow.Repository<Connection>()
            .FindAsync(c =>
                (c.FromUserId == request.UserId || c.ToUserId == request.UserId) &&
                c.Status == ConnectionStatus.Accepted,
                cancellationToken);

        return accepted.Select(c => new ConnectionDto
        {
            ConnectionId = c.Id,
            UserId = request.UserId,
            ConnectedUserId = c.FromUserId == request.UserId ? c.ToUserId : c.FromUserId,
            ConnectedAt = c.AcceptedAt ?? c.RequestedAt
        }).ToList();
    }
}
