using MediatR;

namespace ItnoaWorq.Application.Features.Posts.Commands;

public record CreatePostCommand(Guid AuthorUserId, string Content) : IRequest<Guid>;
