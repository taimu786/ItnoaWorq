using ItnoaWorq.Application.Abstraction.Interfaces;
using ItnoaWorq.Application.Features.Posts.Commands;
using ItnoaWorq.Domain.Entities;
using MediatR;

namespace ItnoaWorq.Application.Features.Posts.Handlers;

public class AddReactionHandler : IRequestHandler<AddReactionCommand, Unit>
{
    private readonly IUnitOfWork _uow;

    public AddReactionHandler(IUnitOfWork uow) => _uow = uow;

    public async Task<Unit> Handle(AddReactionCommand request, CancellationToken cancellationToken)
    {
        var post = await _uow.Repository<Post>().GetByIdAsync(request.PostId, cancellationToken);
        if (post == null) throw new Exception("Post not found");

        var existing = (await _uow.Repository<PostReaction>()
            .FindAsync(r => r.PostId == request.PostId && r.UserId == request.UserId, cancellationToken))
            .FirstOrDefault();

        if (existing != null)
        {
            existing.Type = request.Type;
            _uow.Repository<PostReaction>().Update(existing);
        }
        else
        {
            var reaction = new PostReaction
            {
                PostId = request.PostId,
                UserId = request.UserId,
                Type = request.Type
            };
            await _uow.Repository<PostReaction>().AddAsync(reaction, cancellationToken);
        }

        await _uow.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}
