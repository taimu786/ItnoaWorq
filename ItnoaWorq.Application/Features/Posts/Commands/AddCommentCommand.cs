using MediatR;

namespace ItnoaWorq.Application.Features.Posts.Commands;

public record AddCommentCommand(Guid UserId, Guid PostId, string Content) : IRequest<Unit>;
