using ItnoaWorq.Application.Common.DTOs;
using MediatR;

namespace ItnoaWorq.Application.Features.Profiles.Commands;

public record AddEducationCommand(Guid UserId, EducationDto Education) : IRequest<Unit>;
