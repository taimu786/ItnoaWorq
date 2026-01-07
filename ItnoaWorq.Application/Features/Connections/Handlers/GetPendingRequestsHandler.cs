using ItnoaWorq.Application.Abstraction.Interfaces;
using ItnoaWorq.Application.Common.DTOs;
using ItnoaWorq.Application.Features.Connections.Queries;
using ItnoaWorq.Domain.Entities;
using ItnoaWorq.Domain.Enums;
using MediatR;

namespace ItnoaWorq.Application.Features.Connections.Handlers;

public class GetPendingRequestsHandler : IRequestHandler<GetPendingRequestsQuery, List<ConnectionRequestDto>>
{
    private readonly IUnitOfWork _uow;
    public GetPendingRequestsHandler(IUnitOfWork uow) => _uow = uow;

    public async Task<List<ConnectionRequestDto>> Handle(GetPendingRequestsQuery request, CancellationToken cancellationToken)
    {
        var pending = await _uow.Repository<Connection>()
            .FindAsync(c => c.ToUserId == request.UserId && c.Status == ConnectionStatus.Pending, cancellationToken);

        return pending.Select(c => new ConnectionRequestDto
        {
            RequestId = c.Id,
            FromUserId = c.FromUserId,
            RequestedAt = c.RequestedAt
        }).ToList();
    }
}
