using MediatR;

namespace ItnoaWorq.Application.Features.Connections.Commands;

public record AcceptConnectionCommand(Guid RequestId, Guid UserId) : IRequest<Unit>;
