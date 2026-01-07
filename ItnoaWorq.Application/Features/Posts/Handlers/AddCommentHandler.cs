using ItnoaWorq.Application.Abstraction.Interfaces;
using ItnoaWorq.Application.Features.Posts.Commands;
using ItnoaWorq.Domain.Entities;
using MediatR;

namespace ItnoaWorq.Application.Features.Posts.Handlers;

public class AddCommentHandler : IRequestHandler<AddCommentCommand, Unit>
{
    private readonly IUnitOfWork _uow;

    public AddCommentHandler(IUnitOfWork uow) => _uow = uow;

    public async Task<Unit> Handle(AddCommentCommand request, CancellationToken cancellationToken)
    {
        var post = await _uow.Repository<Post>().GetByIdAsync(request.PostId, cancellationToken);
        if (post == null) throw new Exception("Post not found");

        var comment = new PostComment
        {
            PostId = request.PostId,
            UserId = request.UserId,
            Body = request.Content,
            CreatedAt = DateTime.UtcNow
        };

        await _uow.Repository<PostComment>().AddAsync(comment, cancellationToken);
        await _uow.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
