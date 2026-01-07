using ItnoaWorq.Application.Common.DTOs;
using MediatR;

namespace ItnoaWorq.Application.Features.Profiles.Commands;

public record AddExperienceCommand(Guid UserId, ExperienceDto Experience) : IRequest<Unit>;
