using MediatR;

namespace ItnoaWorq.Application.Features.Connections.Commands;

public record RejectConnectionCommand(Guid RequestId, Guid UserId) : IRequest<Unit>;
