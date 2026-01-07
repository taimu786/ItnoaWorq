using ItnoaWorq.Application.Abstraction.Interfaces;
using ItnoaWorq.Application.Features.Posts.Commands;
using ItnoaWorq.Domain.Entities;
using MediatR;

namespace ItnoaWorq.Application.Features.Posts.Handlers;

public class CreatePostHandler : IRequestHandler<CreatePostCommand, Guid>
{
    private readonly IUnitOfWork _uow;

    public CreatePostHandler(IUnitOfWork uow) => _uow = uow;

    public async Task<Guid> Handle(CreatePostCommand request, CancellationToken cancellationToken)
    {
        var post = new Post
        {
            AuthorUserId = request.AuthorUserId,
            Body = request.Content,
            CreatedAt = DateTime.UtcNow
        };

        await _uow.Repository<Post>().AddAsync(post, cancellationToken);
        await _uow.SaveChangesAsync(cancellationToken);

        return post.Id;
    }
}
