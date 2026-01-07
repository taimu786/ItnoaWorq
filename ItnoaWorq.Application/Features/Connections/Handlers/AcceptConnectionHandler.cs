using ItnoaWorq.Application.Abstraction.Interfaces;
using ItnoaWorq.Application.Features.Connections.Commands;
using ItnoaWorq.Domain.Entities;
using ItnoaWorq.Domain.Enums;
using MediatR;

namespace ItnoaWorq.Application.Features.Connections.Handlers;

public class AcceptConnectionHandler : IRequestHandler<AcceptConnectionCommand, Unit>
{
    private readonly IUnitOfWork _uow;

    public AcceptConnectionHandler(IUnitOfWork uow) => _uow = uow;

    public async Task<Unit> Handle(AcceptConnectionCommand request, CancellationToken cancellationToken)
    {
        var conn = await _uow.Repository<Connection>().GetByIdAsync(request.RequestId, cancellationToken);
        if (conn == null) throw new Exception("Connection request not found");

        if (conn.ToUserId != request.UserId)
            throw new Exception("You are not authorized to accept this request");

        conn.Status = ConnectionStatus.Accepted;
        conn.AcceptedAt = DateTime.UtcNow;

        _uow.Repository<Connection>().Update(conn);
        await _uow.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
