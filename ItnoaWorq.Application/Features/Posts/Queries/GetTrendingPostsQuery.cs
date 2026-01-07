using ItnoaWorq.Application.Common.DTOs;
using MediatR;

namespace ItnoaWorq.Application.Features.Posts.Queries;

public record GetTrendingPostsQuery(int Page = 1, int PageSize = 10)
    : IRequest<List<PostDto>>;
