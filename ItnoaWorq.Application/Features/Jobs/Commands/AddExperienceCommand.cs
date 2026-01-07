using MediatR;

namespace ItnoaWorq.Application.Features.Jobs.Commands;

public record ApplyForJobCommand(Guid UserId, Guid JobId) : IRequest<Unit>;
