using MediatR;

namespace ItnoaWorq.Application.Features.Jobs.Commands;

public record EditJobCommand(Guid JobId, string Title, string Description, string Location) : IRequest<Unit>;
