using ItnoaWorq.Application.Common.DTOs;
using MediatR;

namespace ItnoaWorq.Application.Features.Profiles.Queries;

public record GetJobByIdQuery(Guid UserId) : IRequest<ProfileDto>;
