using MediatR;

namespace ItnoaWorq.Application.Features.Connections.Commands;

public record SendConnectionRequestCommand(Guid FromUserId, Guid ToUserId) : IRequest<Unit>;
