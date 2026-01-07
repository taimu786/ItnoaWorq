using ItnoaWorq.Application.Abstraction.Interfaces;
using ItnoaWorq.Application.Features.Connections.Commands;
using ItnoaWorq.Domain.Entities;
using ItnoaWorq.Domain.Enums;
using MediatR;

namespace ItnoaWorq.Application.Features.Connections.Handlers;

public class RejectConnectionHandler : IRequestHandler<RejectConnectionCommand, Unit>
{
    private readonly IUnitOfWork _uow;

    public RejectConnectionHandler(IUnitOfWork uow) => _uow = uow;

    public async Task<Unit> Handle(RejectConnectionCommand request, CancellationToken cancellationToken)
    {
        var conn = await _uow.Repository<Connection>().GetByIdAsync(request.RequestId, cancellationToken);
        if (conn == null) throw new Exception("Connection request not found");

        if (conn.ToUserId != request.UserId)
            throw new Exception("You are not authorized to reject this request");

        conn.Status = ConnectionStatus.Rejected;

        _uow.Repository<Connection>().Update(conn);
        await _uow.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
