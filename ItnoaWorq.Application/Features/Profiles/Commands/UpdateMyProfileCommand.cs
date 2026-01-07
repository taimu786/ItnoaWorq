using ItnoaWorq.Application.Common.DTOs;
using MediatR;

namespace ItnoaWorq.Application.Features.Profiles.Commands;

public record UpdateMyProfileCommand(Guid UserId, ProfileDto Profile) : IRequest<Unit>;
