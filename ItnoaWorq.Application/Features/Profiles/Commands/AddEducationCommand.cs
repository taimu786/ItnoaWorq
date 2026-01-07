using ItnoaWorq.Application.Common.DTOs;
using MediatR;

namespace ItnoaWorq.Application.Features.Profiles.Commands;

public record ApplyForJobCommand(Guid UserId, EducationDto Education) : IRequest<Unit>;
