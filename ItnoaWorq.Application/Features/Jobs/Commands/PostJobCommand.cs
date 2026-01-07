using MediatR;

namespace ItnoaWorq.Application.Features.Jobs.Commands;

public record PostJobCommand(Guid TenantId, string Title, string Description, string Location) : IRequest<Guid>;
