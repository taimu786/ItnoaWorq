using ItnoaWorq.Application.Abstraction.Interfaces;
using ItnoaWorq.Application.Common.DTOs;
using ItnoaWorq.Application.Features.Posts.Queries;
using ItnoaWorq.Domain.Entities;
using MediatR;

namespace ItnoaWorq.Application.Features.Posts.Handlers;

public class GetPostByIdHandler : IRequestHandler<GetPostByIdQuery, PostDto?>
{
    private readonly IUnitOfWork _uow;

    public GetPostByIdHandler(IUnitOfWork uow) => _uow = uow;

    public async Task<PostDto?> Handle(GetPostByIdQuery request, CancellationToken cancellationToken)
    {
        var post = await _uow.Repository<Post>().GetByIdAsync(request.PostId, cancellationToken);
        if (post == null) return null;

        var comments = await _uow.Repository<PostComment>()
            .FindAsync(c => c.PostId == post.Id, cancellationToken);

        var reactions = await _uow.Repository<PostReaction>()
            .FindAsync(r => r.PostId == post.Id, cancellationToken);

        return new PostDto
        {
            Id = post.Id,
            AuthorUserId = post.AuthorUserId,
            Content = post.Body,
            CreatedAt = post.CreatedAt,
            Comments = comments.Select(c => new CommentDto
            {
                Id = c.Id,
                PostId = c.PostId,
                AuthorUserId = c.UserId,
                Content = c.Body,
                CreatedAt = c.CreatedAt
            }).ToList(),
            Reactions = reactions.Select(r => new ReactionDto
            {
                Id = r.Id,
                PostId = r.PostId,
                UserId = r.UserId,
                Type = r.Type
            }).ToList()
        };
    }
}
