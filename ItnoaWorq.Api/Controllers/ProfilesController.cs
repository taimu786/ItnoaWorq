using ItnoaWorq.Application.Common.DTOs;
using ItnoaWorq.Application.Features.Profiles.Commands;
using ItnoaWorq.Application.Features.Profiles.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ItnoaWorq.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ProfilesController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IHttpContextAccessor _httpContext;

    public ProfilesController(IMediator mediator, IHttpContextAccessor httpContext)
    {
        _mediator = mediator;
        _httpContext = httpContext;
    }

    private Guid GetUserId()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        return Guid.Parse(userId!);
    }

    [HttpGet("me")]
    public async Task<ActionResult<ProfileDto>> GetMyProfile() =>
        await _mediator.Send(new GetMyProfileQuery(GetUserId()));

    [HttpPut("me")]
    public async Task<IActionResult> UpdateMyProfile([FromBody] ProfileDto dto)
    {
        await _mediator.Send(new UpdateMyProfileCommand(GetUserId(), dto));
        return NoContent();
    }

    [HttpPut("me/skills")]
    public async Task<IActionResult> UpdateMySkills([FromBody] List<SkillDto> dto)
    {
        await _mediator.Send(new UpdateMySkillsCommand(GetUserId(), dto));
        return NoContent();
    }

    [HttpPost("me/education")]
    public async Task<IActionResult> AddEducation([FromBody] EducationDto dto)
    {
        await _mediator.Send(new AddEducationCommand(GetUserId(), dto));
        return NoContent();
    }

    [HttpPost("me/experience")]
    public async Task<IActionResult> AddExperience([FromBody] ExperienceDto dto)
    {
        await _mediator.Send(new AddExperienceCommand(GetUserId(), dto));
        return NoContent();
    }
}
