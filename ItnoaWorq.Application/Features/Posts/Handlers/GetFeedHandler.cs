using ItnoaWorq.Application.Abstraction.Interfaces;
using ItnoaWorq.Application.Common.DTOs;
using ItnoaWorq.Application.Features.Posts.Queries;
using ItnoaWorq.Domain.Entities;
using MediatR;

namespace ItnoaWorq.Application.Features.Posts.Handlers;

public class GetFeedHandler : IRequestHandler<GetFeedQuery, List<PostDto>>
{
    private readonly IUnitOfWork _uow;
    private readonly Random _random = new();

    public GetFeedHandler(IUnitOfWork uow) => _uow = uow;

    public async Task<List<PostDto>> Handle(GetFeedQuery request, CancellationToken cancellationToken)
    {
        // 1️⃣ Get connections
        var connections = await _uow.Repository<Connection>()
            .FindAsync(c =>
                (c.ToUserId == request.UserId || c.FromUserId == request.UserId),
                cancellationToken);

        var connectedUserIds = connections
            .Select(c => c.ToUserId == request.UserId ? c.FromUserId : c.ToUserId)
            .Distinct()
            .ToList();

        // 2️⃣ Fetch connection posts
        var connectionPosts = connectedUserIds.Any()
            ? await _uow.Repository<Post>().FindAsync(
                p => connectedUserIds.Contains(p.AuthorUserId), cancellationToken)
            : new List<Post>();

        // 3️⃣ Fetch global posts
        var globalPosts = await _uow.Repository<Post>().GetAllAsync(cancellationToken);

        // 4️⃣ Random extra posts
        var randomExtras = globalPosts
            .Where(p => !connectedUserIds.Contains(p.AuthorUserId))
            .OrderBy(_ => _random.Next())
            .Take(Math.Max(2, request.PageSize / 10)) // ~10%
            .ToList();

        // 5️⃣ Trending posts (by reactions + comments count in last 7 days)
        var trendingCandidates = globalPosts
            .Where(p => p.CreatedAt >= DateTime.UtcNow.AddDays(-7))
            .ToList();

        var trendingPosts = trendingCandidates
            .OrderByDescending(p =>
                globalPosts.Count(g => g.Id == p.Id) + // base weight
                _uow.Repository<PostReaction>().FindAsync(r => r.PostId == p.Id, cancellationToken).Result.Count() +
                _uow.Repository<PostComment>().FindAsync(c => c.PostId == p.Id, cancellationToken).Result.Count())
            .Take(Math.Max(2, request.PageSize / 10)) // ~10%
            .ToList();

        // 6️⃣ Merge posts
        var merged = connectionPosts
            .Concat(randomExtras)
            .Concat(trendingPosts)
            .DistinctBy(p => p.Id)
            .OrderByDescending(p => p.CreatedAt)
            .Skip((request.Page - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToList();

        // 7️⃣ Build DTOs
        var result = new List<PostDto>();

        foreach (var post in merged)
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
