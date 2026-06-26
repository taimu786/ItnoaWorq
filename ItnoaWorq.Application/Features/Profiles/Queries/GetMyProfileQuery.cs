using ItnoaWorq.Application.Common.DTOs;
using MediatR;

namespace ItnoaWorq.Application.Features.Profiles.Queries;

public record GetMyProfileQuery(Guid UserId) : IRequest<ProfileDto>;
