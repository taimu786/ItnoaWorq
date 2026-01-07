using ItnoaWorq.Application.Common.DTOs;
using MediatR;

namespace ItnoaWorq.Application.Features.Profiles.Commands;

public record UpdateMySkillsCommand(Guid UserId, List<SkillDto> Skills) : IRequest<Unit>;
