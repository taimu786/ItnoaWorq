using ItnoaWorq.Domain.Enums;
using MediatR;

namespace ItnoaWorq.Application.Features.Jobs.Commands;

public record UpdateApplicationStatusCommand(Guid ApplicationId, ApplicationStatus Status) : IRequest<Unit>;
