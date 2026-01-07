using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ItnoaWorq.Application.Common.DTOs;
using ItnoaWorq.Application.Features.Posts.Commands;
using ItnoaWorq.Application.Features.Posts.Queries;

namespace ItnoaWorq.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PostsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IHttpContextAccessor _http;

    public PostsController(IMediator mediator, IHttpContextAccessor http)
    {
        _mediator = mediator;
        _http = http;
    }

    private Guid GetUserId() =>
        Guid.Parse(_http.HttpContext!.User.Claims.First(c => c.Type == "uid").Value);

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Create([FromBody] string content)
    {
        var postId = await _mediator.Send(new CreatePostCommand(GetUserId(), content));
        return Ok(new { id = postId });
    }

    [HttpGet("feed")]
    [Authorize]
    public async Task<ActionResult<List<PostDto>>> GetFeed([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        return await _mediator.Send(new GetFeedQuery(GetUserId(), page, pageSize));
    }

    [HttpGet("{id}")]
    [AllowAnonymous]
    public async Task<ActionResult<PostDto?>> GetById(Guid id)
    {
        return await _mediator.Send(new GetPostByIdQuery(id));
    }

    [HttpPost("{id}/comment")]
    [Authorize]
    public async Task<IActionResult> AddComment(Guid id, [FromBody] string content)
    {
        await _mediator.Send(new AddCommentCommand(GetUserId(), id, content));
        return Ok(new { message = "Comment added" });
    }

    [HttpPost("{id}/reaction")]
    [Authorize]
    public async Task<IActionResult> AddReaction(Guid id, [FromBody] int type)
    {
        await _mediator.Send(new AddReactionCommand(GetUserId(), id, (Domain.Enums.ReactionType)type));
        return Ok(new { message = "Reaction updated" });
    }

    [HttpGet("trending")]
    [AllowAnonymous]
    public async Task<ActionResult<List<PostDto>>> GetTrending([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        return await _mediator.Send(new GetTrendingPostsQuery(page, pageSize));
    }
}
