using ItnoaWorq.Application.Common.DTOs;
using ItnoaWorq.Application.Features.Connections.Commands;
using ItnoaWorq.Application.Features.Connections.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ItnoaWorq.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ConnectionsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IHttpContextAccessor _http;

    public ConnectionsController(IMediator mediator, IHttpContextAccessor http)
    {
        _mediator = mediator;
        _http = http;
    }

    private Guid GetUserId() =>
        Guid.Parse(_http.HttpContext!.User.Claims.First(c => c.Type == "uid").Value);

    [HttpPost("{targetUserId}/request")]
    [Authorize]
    public async Task<IActionResult> SendRequest(Guid targetUserId)
    {
        await _mediator.Send(new SendConnectionRequestCommand(GetUserId(), targetUserId));
        return Ok(new { message = "Connection request sent" });
    }

    [HttpPost("{requestId}/accept")]
    [Authorize]
    public async Task<IActionResult> Accept(Guid requestId)
    {
        await _mediator.Send(new AcceptConnectionCommand(requestId, GetUserId()));
        return Ok(new { message = "Connection accepted" });
    }

    [HttpPost("{requestId}/reject")]
    [Authorize]
    public async Task<IActionResult> Reject(Guid requestId)
    {
        await _mediator.Send(new RejectConnectionCommand(requestId, GetUserId()));
        return Ok(new { message = "Connection rejected" });
    }

    [HttpGet("pending")]
    [Authorize]
    public async Task<ActionResult<List<ConnectionRequestDto>>> GetPending()
    {
        return await _mediator.Send(new GetPendingRequestsQuery(GetUserId()));
    }

    [HttpGet]
    [Authorize]
    public async Task<ActionResult<List<ConnectionDto>>> GetAll()
    {
        return await _mediator.Send(new GetConnectionsQuery(GetUserId()));
    }

    [HttpGet("suggestions")]
    [Authorize]
    public async Task<ActionResult<List<ConnectionSuggestionDto>>> GetSuggestions([FromQuery] int limit = 10)
    {
        return await _mediator.Send(new GetConnectionSuggestionsQuery(GetUserId(), limit));
    }
}
