using ItnoaWorq.Application.Abstraction.Interfaces;
using ItnoaWorq.Application.Common.DTOs;
using ItnoaWorq.Application.Features.Posts.Queries;
using ItnoaWorq.Domain.Entities;
using MediatR;

namespace ItnoaWorq.Application.Features.Posts.Handlers;

public class GetTrendingPostsHandler : IRequestHandler<GetTrendingPostsQuery, List<PostDto>>
{
    private readonly IUnitOfWork _uow;

    public GetTrendingPostsHandler(IUnitOfWork uow) => _uow = uow;

    public async Task<List<PostDto>> Handle(GetTrendingPostsQuery request, CancellationToken cancellationToken)
    {
        var posts = await _uow.Repository<Post>().GetAllAsync(cancellationToken);

        // last 7 days
        var candidates = posts.Where(p => p.CreatedAt >= DateTime.UtcNow.AddDays(-7)).ToList();

        var scored = new List<(Post Post, int Score)>();

        foreach (var post in candidates)
        {
            var reactionCount = (await _uow.Repository<PostReaction>()
                .FindAsync(r => r.PostId == post.Id, cancellationToken)).Count();

            var commentCount = (await _uow.Repository<PostComment>()
                .FindAsync(c => c.PostId == post.Id, cancellationToken)).Count();

            var score = reactionCount * 2 + commentCount; // reactions weighted higher
            scored.Add((post, score));
        }

        var trending = scored
            .OrderByDescending(s => s.Score)
            .Skip((request.Page - 1) * request.PageSize)
            .Take(request.PageSize)
            .Select(s => s.Post)
            .ToList();

        var result = new List<PostDto>();
        foreach (var post in trending)
        {
            var comments = await _uow.Repository<PostComment>()
                .FindAsync(c => c.PostId == post.Id, cancellationToken);

            var reactions = await _uow.Repository<PostReaction>()
                .FindAsync(r => r.PostId == post.Id, cancellationToken);

            result.Add(new PostDto
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
            });
        }

        return result;
    }
}
