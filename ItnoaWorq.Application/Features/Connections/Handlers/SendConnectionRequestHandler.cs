using ItnoaWorq.Application.Abstraction.Interfaces;
using ItnoaWorq.Application.Features.Connections.Commands;
using ItnoaWorq.Domain.Entities;
using ItnoaWorq.Domain.Enums;
using MediatR;

namespace ItnoaWorq.Application.Features.Connections.Handlers;

public class SendConnectionRequestHandler : IRequestHandler<SendConnectionRequestCommand, Unit>
{
    private readonly IUnitOfWork _uow;

    public SendConnectionRequestHandler(IUnitOfWork uow) => _uow = uow;

    public async Task<Unit> Handle(SendConnectionRequestCommand request, CancellationToken cancellationToken)
    {
        var existing = (await _uow.Repository<Connection>()
            .FindAsync(c =>
                (c.FromUserId == request.FromUserId && c.ToUserId == request.ToUserId) ||
                (c.FromUserId == request.ToUserId && c.ToUserId == request.FromUserId),
                cancellationToken))
            .FirstOrDefault();

        if (existing != null) throw new Exception("Connection already exists or pending.");

        var conn = new Connection
        {
            FromUserId = request.FromUserId,
            ToUserId = request.ToUserId,
            Status = ConnectionStatus.Pending,
            RequestedAt = DateTime.UtcNow
        };

        await _uow.Repository<Connection>().AddAsync(conn, cancellationToken);
        await _uow.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
