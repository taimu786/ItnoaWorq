using MediatR;

namespace ItnoaWorq.Application.Features.Jobs.Commands;

public record DeleteJobCommand(Guid JobId) : IRequest<Unit>;
