using ItnoaWorq.Application.Common.DTOs;
using MediatR;

namespace ItnoaWorq.Application.Features.Posts.Queries;

public record GetPostByIdQuery(Guid PostId) : IRequest<PostDto?>;
