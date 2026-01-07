using ItnoaWorq.Domain.Enums;
using MediatR;

namespace ItnoaWorq.Application.Features.Posts.Commands;

public record AddReactionCommand(Guid UserId, Guid PostId, ReactionType Type) : IRequest<Unit>;
